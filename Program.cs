using System;
using System.Collections.Generic;

namespace TxSmsSdk.Net
{
    class Program
    {
        static void Main(string[] args)
        {
            TxSms.SDK.SmsHelper.rootUrl = "http://10.10.50.3:8082";
            TxSms.SDK.SmsHelper.publicKey = @"<RSAKeyValue>
  <Modulus>t2IPmZmb/K6ULtN4ZAQRJ3LQXAJVOTAH00hTrOUU3zN41roN1dZ18q8JG6+wJYUlyYyLCPo+fvK8pOOx1es9QVtwCcsnQExsn9wM2g/gKrGtwK3/6h3mSXZhV3q4qRpCtFA2fOP6M4YWMfiWr4DTYBGwBfhur0ovitT58xYIvlk=</Modulus>
  <Exponent>AQAB</Exponent>
</RSAKeyValue>";
            var account = "meeleem";
            var password = "qq123456";
            var uid = "1000105";

            string spd = "--------------------------------------------";

            {
                Console.WriteLine("余额查询");
                var body = TxSms.SDK.SmsHelper.QueryBalance(account, password, uid);
                Console.WriteLine("结果: " + body);
                Console.WriteLine(spd);
            }
            {
                Console.WriteLine("余额查询(加密)");
                var body = TxSms.SDK.SmsHelper.QueryBalanceWithRsa(account, password, uid);
                Console.WriteLine("结果: " + body);
                Console.WriteLine(spd);
            }
            {
                Console.WriteLine("短信发送(加密)");
                var body = TxSms.SDK.SmsHelper.MsgSendWithRsa(account, password, uid, "【内测】您的验证码为：123456", "13000000000,13900000000");
                Console.WriteLine("结果: " + body);
                Console.WriteLine(spd);
            }
            {
                Console.WriteLine("变量短信(加密)");
                var body = TxSms.SDK.SmsHelper.MsgVariableWithRsa(account, password, uid, "【内测】您的验证码为：{$var}", "13000000000,123456;13900000000,656565");
                Console.WriteLine("结果: " + body);
                Console.WriteLine(spd);
            }
            {
                /*	cout << "短信包发送" << endl;
                    list<string> msgs;
                    msgs.push_back("132769909863|test1");
                    msgs.push_back("132769909864|test2");
                    msgs.push_back("132769909865|test3");

                    var body = TxSms.SDK.SmsHelper.MsgPackage(account, password, uid, msgs);
                    std::cout << "发送结果: " << body << endl << spd << endl;*/
            }
            {
                Console.WriteLine("短信包发送(加密)");
                List<string> msgs = new List<string>();
                msgs.Add("13276990986|中文1");
                msgs.Add("13276990986|中文2");
                msgs.Add("13276990986|中文3");
                var body = TxSms.SDK.SmsHelper.MsgPackageWithRsa(account, password, uid, msgs, "201908190909");
                Console.WriteLine("结果: " + body);
                Console.WriteLine(spd);
            }
            {
                Console.WriteLine("拉取上行");
                var body = TxSms.SDK.SmsHelper.PullMo(account, password);
                Console.WriteLine("结果: " + body);
                Console.WriteLine(spd);
            }
            {
                Console.WriteLine("拉取报告");
                var body = TxSms.SDK.SmsHelper.PullReport(account, password);
                Console.WriteLine("结果: " + body);
                Console.WriteLine(spd);
            }

            Console.ReadLine();
        }
    }
}
