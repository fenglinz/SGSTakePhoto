namespace SGSTakePhoto.Infrastructure
{
    /// <summary>
    /// 用户加密请求的Token验证
    /// </summary>
    public class EncryptToken
    {
        public string Mac { get; set; }
        public string Timestamp { get; set; }
        public string Token { get; set; }

        /// <summary>
        /// 返回Token信息
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("mac={0}&timestamp={1}&token={2}", Mac, Timestamp, Token);
        }
    }
}
