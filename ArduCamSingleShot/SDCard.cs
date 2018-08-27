using Microsoft.SPOT;
using Microsoft.SPOT.IO;
using System.IO;

namespace ArduCamSingleShot
{
    public static class SDCard
    {
        public static void OutputSDInfo()
        {
            var vInfo = new VolumeInfo("SD");

            if (vInfo != null)
            {
                Debug.Print("Is Formatted: " + vInfo.IsFormatted.ToString());
                Debug.Print("Total Free Space: " + vInfo.TotalFreeSpace.ToString());
                Debug.Print("Total Size: " + vInfo.TotalSize.ToString());
                Debug.Print("File System: " + vInfo.FileSystem);

                if (vInfo.TotalFreeSpace == 0) {
                    var message = "sdcard has 0 free space! Ensure sdcard is only upto 2 GB!";
                    Debug.Print(message);
                         throw new IOException(message);
                }

            }
            else
            {
                Debug.Print("There doesn't appear to be an SD card in the device.");
            }
        }

        public static bool SDExists()
        {
            return (new VolumeInfo("SD") != null);
        }
    }
}
