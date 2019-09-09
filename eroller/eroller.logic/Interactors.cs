using System;
using eroller.logic.data;
using eroller.logic.provider;

namespace eroller.logic
{
    public class Interactors
    {
        private readonly RegistrationProvider _registrationProvider;

        public Interactors()
            : this(Guid.NewGuid().ToString, RandomStringId.New) {
        }

        internal Interactors(Func<string> newId, Func<string> newCode) {
            _registrationProvider = new RegistrationProvider(newId, newCode);
        }

        public Result Register(string name, string phone) {
            var id = _registrationProvider.Add(name, phone);
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