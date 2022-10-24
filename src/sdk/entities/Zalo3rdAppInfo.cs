namespace ZaloDotNetSDK
{
    public class ZaloAppInfo
    {
        public long AppId { get; set; }
        public string SecretKey { get; set; }
        public string CallbackUrl { get; set; }


        public ZaloAppInfo(long appId, string secretKey, string callbackUrl)
        {
            AppId = appId;
            SecretKey = secretKey;
            CallbackUrl = callbackUrl;
        }
    }
}