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
            if (customer != null && customer.Status == Status.Registered && customer.Code == code) {
                _registrationProvider.Approved(id);
                return new OkResult {Id = id};
            }
            return new ErrorCantApprove();
        }
    }
}
