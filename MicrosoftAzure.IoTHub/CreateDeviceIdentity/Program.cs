using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;

namespace CreateDeviceIdentity
{
    class Program
    {
        static RegistryManager registryManager;
        static string connectionString = "HostName=AzureIoTHubWXL.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=wGptxByn6vS7lvxsvWgsq5TwJKESgxQOv8XEcEOEZ6w=";

        static void Main(string[] args)
        {
            registryManager = RegistryManager.CreateFromConnectionString(connectionString);

            AddDeviceAsync().Wait();
            Console.ReadLine();
        }

        private static async Task AddDeviceAsync()
        {
            string deviceId = "myFirstDevice";
            Device device;

            try
            {
                device = await registryManager.AddDeviceAsync(new Device(deviceId));

            }
            catch (DeviceAlreadyExistsException)
            {
                device = await registryManager.GetDeviceAsync(deviceId);
            }

            Console.WriteLine("Generated device key {0}", device.Authentication.SymmetricKey.PrimaryKey);
        }
    }
}
