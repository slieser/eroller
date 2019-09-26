using System;
using eroller.logic.data;
using eroller.logic.provider;

namespace eroller.logic
{
    public class Interactors
    {
        private readonly Func<string> _newCode;
        private readonly RegistrationProvider _registrationProvider;
        private readonly SmsProvider _smsProvider = new SmsProvider();
        private readonly RollerProvider _rollerProvider = new RollerProvider();

        public Interactors()
            : this(Guid.NewGuid().ToString, RandomStringId.New) {
        }

        internal Interactors(Func<string> newId, Func<string> newCode) {
            _newCode = newCode;
            _registrationProvider = new RegistrationProvider(newId);
        }

        public Result Register(string name, string phone) {
            var code = _newCode();
            var id = _registrationProvider.Add(name, phone, code);
            _smsProvider.SendCode(phone, code);
            return new OkResult {Id = id};
        }

        public Result Approve(string code, string id) {
            var customer = _registrationProvider.Get(id);
            if (CanApprove(code, customer)) {
                _registrationProvider.Approved(id);
                return new OkResult {Id = id};
            }
            return new ErrorCantApprove();
        }

        public Result Checkin(string id, string rollerId) {
            var customer = _registrationProvider.Get(id);
            if (CanCheckin(customer)) {
                var roller = _rollerProvider.Get(rollerId);
                if (CanCheckin(roller)) {
                    _rollerProvider.Checkin(rollerId, customer.Id);
                    return new OkResult {Id = id};
                }
            }
            return new ErrorCantCheckin();
        }

        private bool CanCheckin(Roller roller) {
            return roller.CustomerId == null;
        }

        private static bool CanApprove(string code, Customer customer) {
            return customer != null && customer.Status == Status.Registered && customer.Code == code;
        }

        private bool CanCheckin(Customer customer) {
            return customer != null && customer.Status == Status.Approved && customer.CheckedInRollerId == null;
        }
    }
}
