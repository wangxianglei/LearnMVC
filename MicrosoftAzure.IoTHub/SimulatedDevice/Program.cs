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
        //static string iotHubUri = "HostName=AzureIoTHubWXL.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=wGptxByn6vS7lvxsvWgsq5TwJKESgxQOv8XEcEOEZ6w=";
        //static string iotHubUri = "HostName=AzureIoTHubWXL.azure-devices.net;SharedAccessKeyName=device;SharedAccessKey=sN2foMpUuwTP1l9z9WWYVmMXNAyn/7Ylk9a9u/xgEQY=";
        static string iotHubUri = "AzureIoTHubWXL.azure-devices.net";
        static string deviceKey = "vjSyJL1+BMKBOXA9oNiJeea3QiEJDBfcGVZKKTKfmGM=";
        static string deviceId = "myFirstDevice";
        static void Main(string[] args)
        {
            Console.WriteLine("Simulated Devices\n");

            deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey(deviceId, deviceKey));
            SendDeviceToCloudMessageAsync();

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
                    deviceId = "myFirstDevice",
                    windSpeed = currentWindSpeed
                };

                var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                var message = new Message(Encoding.ASCII.GetBytes(messageString));

                await deviceClient.SendEventAsync(message);
                Console.WriteLine("{0}> Sending message: {1}", DateTime.Now, messageString);

                Task.Delay(1000).Wait();
            }
        }
    }
}
