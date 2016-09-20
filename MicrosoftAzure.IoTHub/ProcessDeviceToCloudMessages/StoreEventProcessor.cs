using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Security.Cryptography;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace MicrosoftAzure.Tutorial.IoT.ProcessDeviceToCloudMessages
{
    class StoreEventProcessor : IEventProcessor
    {
        private const int MAX_BLOCK_SIZE = 4 * 1024 * 1024;
        private const string SERVICEBUS_QUEUENAME = "q-azure-iot-tutorial";
        
        public static string StorageConnectionString;
        public static string ServiceBusConnectionString;

        private CloudBlobClient _blobClient;
        private CloudBlobContainer _blobContainer;
        private QueueClient _queueClient;
        private long _currentBlockInitOffset;
        private MemoryStream _toAppend = new MemoryStream(MAX_BLOCK_SIZE);

        private Stopwatch _stopWatch;
        private TimeSpan MAX_CHECKPOINT_TIME = TimeSpan.FromHours(1);

        public StoreEventProcessor()
        {
            var storeAccount = CloudStorageAccount.Parse(StorageConnectionString);
            _blobClient = storeAccount.CreateCloudBlobClient();
            _blobContainer = _blobClient.GetContainerReference("SERVICEBUS_QUEUENAME");
            _blobContainer.CreateIfNotExists();

            _queueClient = QueueClient.CreateFromConnectionString(ServiceBusConnectionString);
        }

        Task IEventProcessor.CloseAsync(PartitionContext context, CloseReason reason)
        {
            Console.WriteLine("Processor Shutting Down. Partition: {0}, Reason: {1}", context.Lease.PartitionId, reason.ToString());
            return Task.FromResult<object>(null);
        }

        Task IEventProcessor.OpenAsync(PartitionContext context)
        {
            Console.WriteLine("StoreEventProcessor initialized, Partition: {0}, Offset: {1}", context.Lease.PartitionId, context.Lease.Offset);

            if (!long.TryParse(context.Lease.Offset, out _currentBlockInitOffset))
            {
                _currentBlockInitOffset = 0;
            }

            _stopWatch = new Stopwatch();
            _stopWatch.Start();

            return Task.FromResult<object>(null);
        }


        async Task IEventProcessor.ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> messages)
        {
            foreach (EventData eventData in messages)
            {
                byte[] data = eventData.GetBytes();

                if (eventData.Properties.ContainsKey("messageType") && (string)eventData.Properties["messageType"] == "interactive")
                {
                    var messageId = (string)eventData.SystemProperties["message-id"]; ;
                    var queueMessage = new BrokeredMessage(new MemoryStream(data));
                    queueMessage.MessageId = messageId;
                    queueMessage.Properties["messageType"] = (string)eventData.Properties["messageType"];
                    await _queueClient.SendAsync(queueMessage);

                    WriteHighlightedMessage(string.Format("Recived interactive message: {0}", messageId));

                    continue;
                }

                if (_toAppend.Length + data.Length > MAX_BLOCK_SIZE || _stopWatch.Elapsed > MAX_CHECKPOINT_TIME)
                {
                    //
                }
            }
        }


        #region Private Methods
        private async Task AppendAndCheckpoint(PartitionContext context)
        {
            var blockIdString = string.Format("startSeq:{0}", _currentBlockInitOffset.ToString("0000000000000000000000000"));
            var blockId = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(blockIdString));

            _toAppend.Seek(0, SeekOrigin.Begin);
            byte[] md5 = MD5.Create().ComputeHash(_toAppend);
            _toAppend.Seek(0, SeekOrigin.Begin);

            var blobName = string.Format("azure-iot-hub-d2c_{0}", context.Lease.PartitionId);
            var currentBlob = _blobContainer.GetBlockBlobReference(blobName);
            if (await currentBlob.ExistsAsync())
            {
                await currentBlob.PutBlockAsync(blockId, _toAppend, Convert.ToBase64String(md5));
                var blockList = await currentBlob.DownloadBlockListAsync();
                var newBlockList = new List<string>(blockList.Select(b => b.Name));

                if (newBlockList.Count() > 0 && newBlockList.Last() != blockId)
                {
                    newBlockList.Add(blockId);
                    WriteHighlightedMessage(String.Format("Appending block id: {0} to blob: {1}", blockIdString, currentBlob.Name));
                }
                else
                {
                    WriteHighlightedMessage(String.Format("Overwriting block id: {0}", blockIdString));
                }
                await currentBlob.PutBlockListAsync(newBlockList);
            }
            else
            {
                await currentBlob.PutBlockAsync(blockId, _toAppend, Convert.ToBase64String(md5));
                var newBlockList = new List<string>();
                newBlockList.Add(blockId);
                await currentBlob.PutBlockListAsync(newBlockList);

                WriteHighlightedMessage(String.Format("Created new blob {0}", currentBlob.Name));
            }

            _toAppend.Dispose();
            _toAppend = new MemoryStream(MAX_BLOCK_SIZE);

            // checkpoint.
            await context.CheckpointAsync();
            WriteHighlightedMessage(String.Format("Checkpointed partition: {0}", context.Lease.PartitionId));

            _currentBlockInitOffset = long.Parse(context.Lease.Offset);
            _stopWatch.Restart();
        }
       

        private void WriteHighlightedMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        #endregion
    }
}
