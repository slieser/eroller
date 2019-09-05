using System.Net.WebSockets;

namespace eroller.logic
{
    public class Interactors
    {
        public string Register(string name, string phone) {
            return "1234-5678-abcd-efgh";
        }

        public string Approve(string code, string id) {
            return "ok";
        }
    }
}