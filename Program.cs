using System;
using System.IO;
using WIA;

class Program
{
    static void Main()
    {
        try
        {
            var manager = new DeviceManager();

            DeviceInfo scanner = null;
            foreach (DeviceInfo info in manager.DeviceInfos)
            {
                if (info.Type == WiaDeviceType.ScannerDeviceType)
                {
                    scanner = info;
                    break;
                }
            }

            if (scanner == null) return;

            Device device = scanner.Connect();
            Item item = device.Items[1];

            ImageFile image = (ImageFile)item.Transfer(FormatID.wiaFormatJPEG);

            // Guardar en memoria sin archivo temporal
            var imageData = (byte[])image.FileData.get_BinaryData();
            string base64 = Convert.ToBase64String(imageData);

            Console.WriteLine(base64); // <-- Solo imprime el Base64
        }
        catch
        {
            // Silenciar errores para mantener salida limpia
            Environment.Exit(1);
        }
    }
}