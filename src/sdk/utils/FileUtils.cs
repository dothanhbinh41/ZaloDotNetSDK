using System;
using System.IO;
using System.Net;

namespace ZaloDotNetSDK
{
    public class FileUtils
    {
        public static byte[] LoadFile(string path)
        {
            if (path.Contains("http")) {
                return new WebClient().DownloadData(path);
            }
            return File.ReadAllBytes(path);
        }
    }
}
