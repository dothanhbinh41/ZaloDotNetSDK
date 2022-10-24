namespace ZaloCSharpSDK
{
    public class ZaloOaInfo
    {
        public long OAId { get; set; }
        public string SecretKey { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Cover { get; set; }
        public string Description { get; set; }

        public ZaloOaInfo(long oaId, string secretKey)
        {
            OAId = oaId;
            SecretKey = secretKey;
        }
    }
}
