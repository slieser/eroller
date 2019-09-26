using System;
using System.Collections.Generic;

namespace eroller.logic.provider
{
    public class RegistrationProvider
    {
        private readonly Func<string> _newId;
        private readonly List<Customer> _customers = new List<Customer>();

        public RegistrationProvider(Func<string> newId) {
            _newId = newId;
        }

        public string Add(string name, string phone, string code) {
            var id = _newId();
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

        public void Checkin(string customerId, string rollerId) {
            var customer = Get(customerId);
            customer.CheckedInRollerId = rollerId;
        }

        public void Checkout(string customerId, string rollerId) {
            var customer = Get(customerId);
            if (customer.CheckedInRollerId != rollerId) {
                throw new ArgumentOutOfRangeException("customer is not checked in into this roller");
            }
            customer.CheckedInRollerId = null;
        }
    }
}