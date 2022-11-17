using System;
using System.Security.Cryptography;

namespace GameFramework.Resource
{
    /// <summary>
    /// MG CDN助手
    /// </summary>
	public class CDNHelper
    {
        /// <summary>
        /// 补充CDN校验
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetCDNSafeFull(string url)
        {
            long timestamp = ConvertDateTimeToInt(DateTime.Now.AddDays(1));
            string sid = "10008_5107221990";
            string key = "cmam2021";
            string encrpty = EncryptMD5_32(string.Format("{0}{1}{2}{3}", url, timestamp, sid, key));
            return string.Format("{0}?timestamp={1}&sid={2}&encrypt={3}", url, timestamp, sid, encrpty);
        }

        /// <summary>
        /// 将DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>long</returns>
        private static long ConvertDateTimeToInt(DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;  //除10000调整为13位  
            return t;
        }

        /// <summary>
        /// MD5　32位加密
        /// </summary>
        /// <param name="_encryptContent">需要加密的内容</param>
        /// <returns></returns>
        private static string EncryptMD5_32(string _encryptContent)
        {
            string content_Normal = _encryptContent;
            string content_Encrypt = "";
            MD5 md5 = MD5.Create();

            byte[] s = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(content_Normal));

            for (int i = 0; i < s.Length; i++)
            {
                content_Encrypt = content_Encrypt + s[i].ToString("X2");
            }
            return content_Encrypt.ToLower();
        }
    }
}
