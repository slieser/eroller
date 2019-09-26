using System;
using System.Collections.Generic;
using System.Linq;

namespace eroller.logic.provider
{
    public class RollerProvider
    {
        private readonly List<Roller> _roller = new List<Roller>();

        public RollerProvider() {
            _roller.Add(new Roller { Id = "abcd"});
            _roller.Add(new Roller { Id = "efgh"});
        }

        public Roller Get(string rollerId) {
            return _roller.FirstOrDefault(r => r.Id == rollerId);
        }

        public void Checkin(string rollerId, string customerId, DateTime checkinTime) {
            var roller = Get(rollerId);
            roller.CustomerId = customerId;
            roller.CheckinTime = checkinTime;
        }

        public TimeSpan Checkout(string rollerId, string customerId, DateTime checkoutTime) {
            var roller = Get(rollerId);
            if (roller.CustomerId != customerId) {
                throw new ArgumentOutOfRangeException("roller can't be checked out from this customer id'");
            }
            roller.CustomerId = null;
            var duration = checkoutTime - roller.CheckinTime;
            return duration;
        }
    }
}