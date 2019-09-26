using System;

namespace eroller.logic.data
{
    public class CheckoutResult : OkResult
    {
        public TimeSpan Duration { get; }

        public CheckoutResult(string id, TimeSpan duration) {
            Id = id;
            Duration = duration;
        }
    }
}