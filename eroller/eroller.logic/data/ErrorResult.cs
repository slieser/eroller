namespace eroller.logic.data
{
    public class ErrorResult : Result
    {
        public ErrorResult(string message) {
            Message = message;
        }

        public string Message { get; }
    }
}