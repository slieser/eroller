using System;

namespace eroller.logic.provider
{
    public class SmsProvider
    {
        public void SendCode(string phone, string code) {
            Console.WriteLine($"Send code '{code}' to phone '{phone}'.");
        }
    }
}