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

        public void Checkin(string rollerId, string customerId) {
            var roller = Get(rollerId);
            roller.CustomerId = customerId;
        }
    }
}