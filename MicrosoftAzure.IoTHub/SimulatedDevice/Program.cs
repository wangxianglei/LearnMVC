using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;

namespace SimulatedDevice
{
    class Program
    {
        static DeviceClient deviceClient;
        //static string iotHubUri = "HostName=azure-iot-hub-tutorial.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=SV1bzKTvHh0MhLx/KTPhpC74VpLm7diGh42buCsdFME=";
        //static string iotHubUri = "HostName=azure-iot-hub-tutorial.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=SV1bzKTvHh0MhLx/KTPhpC74VpLm7diGh42buCsdFME=";
        static string iotHubUri = "azure-iot-hub-tutorial.azure-devices.net";
        static string deviceKey = "fxnYm8l85jD7Ei0vdcxTZa6qxDEqXOcuHUq5/93cfyc=";
        static string deviceId = "device-s-1";
        static void Main(string[] args)
        {
            Console.WriteLine("Simulated Devices\n");

            deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey(deviceId, deviceKey));
            //SendDeviceToCloudMessageAsync();
            SendDeviceToCloudInteractiveMessageAsync();
            Console.ReadLine();
        }

        private static async void SendDeviceToCloudMessageAsync()
        {
            double avgWindSpeed = 10; // m/s
            Random random = new Random();

            while (true)
            {
                double currentWindSpeed = avgWindSpeed + random.NextDouble() * 4 - 2;

                var telemetryDataPoint = new
                {
                    deviceId = deviceId,
                    windSpeed = currentWindSpeed
                };

                var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                var message = new Message(Encoding.ASCII.GetBytes(messageString));

                await deviceClient.SendEventAsync(message);
                Console.WriteLine("{0}> Sending message: {1}", DateTime.Now, messageString);

                Task.Delay(1000).Wait();
            }
        }

        private static async void SendDeviceToCloudInteractiveMessageAsync()
        {
            while (true)
            {
                var interactiveMessageString = "Alert message !";
                var interactiveMessage = new Message(Encoding.ASCII.GetBytes(interactiveMessageString));
                interactiveMessage.Properties["messageType"] = "interactive";
                interactiveMessage.MessageId = Guid.NewGuid().ToString();

                await deviceClient.SendEventAsync(interactiveMessage);
                Console.WriteLine("{0}> Send interactive message: {1}", DateTime.Now, interactiveMessageString);

                Task.Delay(1000).Wait();
            }
        }
    }
}
