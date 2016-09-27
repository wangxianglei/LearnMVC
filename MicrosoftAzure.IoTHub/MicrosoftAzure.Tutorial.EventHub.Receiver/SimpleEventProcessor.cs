using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using System.Diagnostics;

namespace MicrosoftAzure.Tutorial.EventHub.Receiver
{
    class SimpleEventProcessor : IEventProcessor
    {
        Stopwatch _checkpointStopwatch;

        async Task IEventProcessor.CloseAsync(PartitionContext context, CloseReason reason)
        {
            Console.WriteLine("{0}> Processor Shut Down. Partition {1}, Reason: {2}", DateTime.Now, context.Lease.PartitionId, reason);

            if (reason == CloseReason.Shutdown)
            {
                await context.CheckpointAsync();
            }
        }

        Task IEventProcessor.OpenAsync(PartitionContext context)
        {
            Console.WriteLine("{0}> SimpleEventProcessor initilized . Partition {1}, Offset: {2}", DateTime.Now, context.Lease.PartitionId, context.Lease.Offset);
            this._checkpointStopwatch = new Stopwatch();
            this._checkpointStopwatch.Start();

            return Task.FromResult<object>(null);
        }

        async Task IEventProcessor.ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> messages)
        {
            foreach (EventData eventData in messages)
            {
                string eventDataString = Encoding.UTF8.GetString(eventData.GetBytes());
                Console.WriteLine("{0}> Message Received. Partition: {1}, Data: {2}", DateTime.Now, context.Lease.PartitionId, eventDataString);
            }

            if (this._checkpointStopwatch.Elapsed > TimeSpan.FromMinutes(5))
            {
                await context.CheckpointAsync();
                this._checkpointStopwatch.Restart();
            }
        }
    }
}
