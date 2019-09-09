using System;
using System.Collections.Generic;

namespace eroller.logic.provider
{
    public class RegistrationProvider
    {
        private readonly Func<string> _newId;
        private readonly Func<string> _newCode;
        private readonly List<Customer> _customers = new List<Customer>();

        public RegistrationProvider(Func<string> newId, Func<string> newCode) {
            _newId = newId;
            _newCode = newCode;
        }

        public string Add(string name, string phone) {
            var id = _newId();
            var code = _newCode();
            var newCustomer = new Customer {
                Id = id,
                Name = name,
                Phone = phone,
                Status = Status.Registered,
                Code = code
            };
            _customers.Add(newCustomer);
            return id;
        }

        public Customer Get(string id) {
            return _customers.Find(customer => customer.Id == id);
        }

        public void Approved(string id) {
            var customer = Get(id);
            customer.Status = Status.Approved;
        }
    }
}