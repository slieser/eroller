using eroller.logic.data;

namespace eroller.logic
{
    public class Interactors
    {
        public Result Register(string name, string phone) {
            return new OkResult {Id = "1234-5678-abcd-efgh"};
        }

        public Result Approve(string code, string id) {
            if (code == "1234") {
                return new OkResult {Id = id};
            }
            return new ErrorCantApprove();
        }
    }
}