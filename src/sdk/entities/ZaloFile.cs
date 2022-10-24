using System.Net.Http;
using System.IO;
using System.Net.Http.Headers;

namespace ZaloDotNetSDK
{
    public class ZaloFile
    {
        public string Name => name;
        public ByteArrayContent Data => data;

        readonly string name;
        readonly ByteArrayContent data; 
        public ZaloFile(string path)
        {
            data = new ByteArrayContent(FileUtils.LoadFile(path));
            name = Path.GetFileName(path);
        }

   
        public void SetMediaTypeHeader(string type)
        {
            data.Headers.ContentType = new MediaTypeHeaderValue(type);
        }
    }
}