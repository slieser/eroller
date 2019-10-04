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
        private readonly Func<DateTime> _now;

        public Interactors()
            : this(Guid.NewGuid().ToString, RandomStringId.New, () => DateTime.Now) {
        }

        internal Interactors(Func<string> newId, Func<string> newCode, Func<DateTime> now) {
            _newCode = newCode;
            _registrationProvider = new RegistrationProvider(newId);
            _now = now;
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
                    _rollerProvider.Checkin(rollerId, customer.Id, _now());
                    _registrationProvider.Checkin(customer.Id, rollerId);
                    return new OkResult {Id = id};
                }
            }
            return new ErrorCantCheckin();
        }

        public Result Checkout(string id, string rollerId) {
            var customer = _registrationProvider.Get(id);
            if (CanCheckout(customer)) {
                var roller = _rollerProvider.Get(rollerId);
                if (CanCheckout(roller)) {
                    var duration = _rollerProvider.Checkout(rollerId, customer.Id, _now());
                    _registrationProvider.Checkout(customer.Id, rollerId);
                    return new CheckoutResult(id, duration);
                }
            }
            return new ErrorCantCheckout();
        }

        private bool CanCheckin(Customer customer) {
            return customer != null && customer.Status == Status.Approved && customer.CheckedInRollerId == null;
        }

        private bool CanCheckin(Roller roller) {
            return roller != null && roller.CustomerId == null;
        }

        private bool CanCheckout(Customer customer) {
            return customer?.CheckedInRollerId != null;
        }

        private bool CanCheckout(Roller roller) {
            return roller?.CustomerId != null;
        }

        private static bool CanApprove(string code, Customer customer) {
            return customer != null && customer.Status == Status.Registered && customer.Code == code;
        }
    }
}
