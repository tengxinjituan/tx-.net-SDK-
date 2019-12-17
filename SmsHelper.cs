using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TxSms.SDK
{
    public class SmsHelper
    {
        private readonly static HttpClient _hc = new HttpClient();

        public static string publicKey = @"";
        public static string rootUrl;

        public static string QueryBalance(string account, string password, string uid)
        {
            string ret;
            string data = "{\"account\":\"" + account + "\", \"password\":\"" + password + "\",\"uid\":\"" + uid + "\"}";
            ret = HttpSender.Post(rootUrl + "/msg/balance/json", data, "application/json").Result;
            return ret;
        }

        public static string QueryBalanceWithRsa(string account, string password, string uid)
        {
            string ret;
            string data = "{\"account\":\"" + account + "\", \"password\":\"" + password + "\",\"uid\":\"" + uid + "\"}";

            var key = GetAesKey();
            var resultData = AESUtil.Encrypt(data, key);
            var resultKey =  RsaPkcs1Util.RsaEncrypt(publicKey, key);

            ret = HttpSender.Post(rootUrl + "/msg/balance/json/rsa", "{\"key\":\"" + resultKey + "\", \"data\":\"" + resultData + "\", \"account\":\"" + account + "\"}", "application/json").Result;
            
            return ret;
        }

        public static string MsgSend(string account, string password, string uid, string msg, string phone, string send_time = "", bool report = false, string extend = "", string format = "json", string useragent = "http")
        {
            string ret = string.Empty;
            string data = "{" +
                                "\"account\":\"" + account + "\"," +
                                "\"password\":\"" + password + "\"," +
							    "\"uid\":\"" + uid + "\"," +
							    "\"msg\":\"" + msg + "\"," +
							    "\"phone\":\"" + phone + "\"," +
							    "\"sendtime\":\"" + send_time + "\"," +
							    "\"report\":\"" + (report ? "true" : "false") + "\"," +
							    "\"extend\":\"" + extend + "\"," +
							    "\"format\":\"" + format + "\"," +
                                "\"useragent\":\"" + useragent + "\"" +
                            "}";
            ret = HttpSender.Post(rootUrl + "/msg/send/json", data, "application/json").Result;
            return ret;
        }

        public static string MsgSendWithRsa(string account, string password, string uid, string msg, string phone, string send_time = "", bool report = false, string extend = "", string format = "json", string useragent = "http")
        {
            string ret;
            string data = "{" +
							    "\"account\":\"" + account + "\"," +
                                "\"password\":\"" + password + "\"," +
                                "\"uid\":\"" + uid + "\"," +
                                "\"msg\":\"" + msg + "\"," +
                                "\"phone\":\"" + phone + "\"," +
                                "\"sendtime\":\"" + send_time + "\"," +
                                "\"report\":\"" + (report ? "true" : "false") + "\"," +
                                "\"extend\":\"" + extend + "\"," +
                                "\"format\":\"" + format + "\"," +
                                "\"useragent\":\"" + useragent + "\"" +
                            "}";
            var key = GetAesKey();
            var resultData = AESUtil.Encrypt(data, key);
            var resultKey = RsaPkcs1Util.RsaEncrypt(publicKey, key);

            ret = HttpSender.Post(rootUrl + "/msg/send/json/rsa", "{\"key\":\"" + resultKey + "\", \"data\":\"" + resultData + "\", \"account\":\"" + account + "\"}", "application/json").Result;
            return ret;
        }

        public static string MsgVariable(string account, string password, string uid, string msg, string @params, string send_time = "", bool report = false, string extend = "", string format = "json", string useragent = "http")
        {
            string ret;
            string data = "{" +
							    "\"account\":\"" + account + "\"," +
                                "\"password\":\"" + password + "\"," +
                                "\"uid\":\"" + uid + "\"," +
                                "\"msg\":\"" + msg + "\"," +
                                "\"params\":\"" + @params + "\"," +
                                "\"sendtime\":\"" + send_time + "\"," +
                                "\"report\":\"" + (report ? "true" : "false") + "\"," +
                                "\"extend\":\"" + extend + "\"," +
                                "\"format\":\"" + format + "\"," +
                                "\"useragent\":\"" + useragent + "\"" +
                            "}";
            ret = HttpSender.Post(rootUrl + "/msg/variable/json", data, "application/json").Result;
            
            return ret;
        }

        public static string MsgVariableWithRsa(string account, string password, string uid, string msg, string @params, string send_time = "", bool report = false, string extend = "", string format = "json", string useragent = "http")
        {
            string ret;
            string data = "{" +
							    "\"account\":\"" + account + "\"," +
							    "\"password\":\"" + password + "\"," +
							    "\"uid\":\"" + uid + "\"," +
							    "\"msg\":\"" + msg + "\"," +
							    "\"params\":\"" + @params + "\"," +
							    "\"sendtime\":\"" + send_time + "\"," +
							    "\"report\":\"" + (report ? "true" : "false") + "\"," +
							    "\"extend\":\"" + extend + "\"," +
							    "\"format\":\"" + format + "\"," +
                                "\"useragent\":\"" + useragent + "\""+
                            "}";
            var key = GetAesKey();
            var resultData = AESUtil.Encrypt(data, key);
            var resultKey = RsaPkcs1Util.RsaEncrypt(publicKey, key);

            ret = HttpSender.Post(rootUrl + "/msg/variable/json/rsa", "{\"key\":\"" + resultKey + "\", \"data\":\"" + resultData + "\", \"account\":\"" + account + "\"}", "application/json").Result;
            return ret;
        }

        public static string MsgPackage(string account, string password, string uid, List<string> msgs, string send_time = "", bool report = false, string extend = "", string format = "json", string useragent = "http")
        {
            string ret;
            string data = "account=" + account + "&password=" + password + "&sendtime=" + send_time + "&report=" + (report ? "true" : "false") + "&extend=" + extend + "&uid=" + uid + "&format=" + format + "&useragent=" + useragent;
            foreach (var msg in msgs)
            {
                data += "&msg=" + msg;
            }
            
            ret = HttpSender.Post(rootUrl + "/msg/sendpackage/json", data, "application/x-www-form-urlencoded").Result;
            return ret;
        }

        public static string MsgPackageWithRsa(string account, string password, string uid, List<string> msgs, string send_time = "", bool report = false, string extend = "", string format = "json", string useragent = "http")
        {
            string ret;
            string data = "account=" + account + "&password=" + password;
            foreach (var msg in msgs)
            {
                data += "&msg=" + msg;
            }
            data += "&sendtime=" + send_time + "&report=" + (report ? "true" : "false") + "&extend=" + extend + "&uid=" + uid + "&format=" + format + "&useragent=" + useragent;
            var key = GetAesKey();
            var resultData = AESUtil.Encrypt(data, key);
            var resultKey = RsaPkcs1Util.RsaEncrypt(publicKey, key);
            ret = HttpSender.Post(rootUrl + "/msg/sendpackage/json/rsa", "key=" + System.Web.HttpUtility.UrlEncode(resultKey) + "&data=" + System.Web.HttpUtility.UrlEncode(resultData) + "&account=" + account, "application/x-www-form-urlencoded").Result;
            return ret;
        }

        public static string PullMo(string account, string password, int count = 20, string format = "json")
        {
            string ret;
            count = count < 20 ? 20 : count;
            count = count > 100 ? 100 : count;
            
            string data = "{" +
							    "\"account\":\"" + account + "\"," +
                                "\"password\":\"" + password + "\"," +
                                "\"format\":\"" + format + "\"," +
                                "\"count\":\"" + count + "\"" +
                            "}";

            ret = HttpSender.Post(rootUrl + "/msg/pull/mo", data, "application/json").Result;
            return ret;
        }

        public static string PullReport(string account, string password, int count = 20, string format = "json")
        {
            string ret;
            count = count < 20 ? 20 : count;
            count = count > 100 ? 100 : count;
            string data = "{" +
							    "\"account\":\"" + account + "\"," +
							    "\"password\":\"" + password + "\"," +
							    "\"format\":\"" + format + "\"," +
                                "\"count\":\"" + count + "\"" +
                            "}";

            ret = HttpSender.Post(rootUrl + "/msg/pull/report", data, "application/json").Result;
            
            return ret;
        }

        readonly static string rs = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static string GetAesKey()
        {
            var ret = string.Empty;
            for (int i = 0; i < 16; i++)
            {
                Random rd = new Random(i);
                ret += rs[rd.Next(0, rs.Length - 1)];
            }
            return ret;
        }
    }
}
