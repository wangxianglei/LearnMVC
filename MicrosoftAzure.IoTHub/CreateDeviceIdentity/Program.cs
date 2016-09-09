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
        static string connectionString = "HostName=azure-iot-hub-tutorial.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=SV1bzKTvHh0MhLx/KTPhpC74VpLm7diGh42buCsdFME=";

        static void Main(string[] args)
        {
            registryManager = RegistryManager.CreateFromConnectionString(connectionString);

            AddDeviceAsync().Wait();
            Console.ReadLine();
        }

        private static async Task AddDeviceAsync()
        {
            string deviceId = "device-s-1";
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
