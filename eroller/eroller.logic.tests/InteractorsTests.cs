using System;
using eroller.logic.data;
using NUnit.Framework;

namespace eroller.logic.tests
{
    [TestFixture]
    public class InteractorsTests
    {
        private Interactors _sut;

        [SetUp]
        public void Setup() {
            var firstCall = true;
            _sut = new Interactors(
                () => "1234", 
                () => "A1B2C3",
                () => {
                    if (firstCall) {
                        firstCall = false;
                        return new DateTime(2019, 9, 26, 12, 0, 0);
                    }
                    return new DateTime(2019, 9, 26, 12, 5, 0);
                });
        }
        
        [Test]
        public void Register_returns_an_id() {
            var result = _sut.Register("stefan", "0221/12345");
            Assert.That(result, Is.TypeOf<OkResult>());
            Assert.That(((OkResult)result).Id, Is.EqualTo("1234"));
        }

        [Test]
        public void Approve_with_wrong_code_returns_an_error() {
            var result = _sut.Approve("wrong", "1234");
            Assert.That(result, Is.TypeOf<ErrorCantApprove>());
        }

        [Test]
        public void Approve_with_unknown_id_returns_an_error() {
            var result = _sut.Approve("", "wrong");
            Assert.That(result, Is.TypeOf<ErrorCantApprove>());
        }

        [Test]
        public void Approve_with_correct_code_and_id_returns_the_id() {
            _sut.Register("name", "phone");
            var result = _sut.Approve("A1B2C3", "1234");
            Assert.That(result, Is.TypeOf<OkResult>());
            Assert.That(((OkResult)result).Id, Is.EqualTo("1234"));
        }

        [Test]
        public void Approved_customer_can_checkin_to_free_Roller() {
            _sut.Register("name", "phone");
            _sut.Approve("A1B2C3", "1234");
            var result = _sut.Checkin("1234", "abcd");
            Assert.That(result, Is.TypeOf<OkResult>());
            Assert.That(((OkResult)result).Id, Is.EqualTo("1234"));
        }

        [Test]
        public void Registered_customer_cant_checkin_to_free_Roller() {
            _sut.Register("name", "phone");
            var result = _sut.Checkin("1234", "abcd");
            Assert.That(result, Is.TypeOf<ErrorCantCheckin>());
        }

        [Test]
        public void Approved_customer_cant_checkin_to_checkin_Roller() {
            _sut.Register("name", "phone");
            _sut.Approve("A1B2C3", "1234");
            _sut.Checkin("1234", "abcd");
            var result = _sut.Checkin("1234", "abcd");
            Assert.That(result, Is.TypeOf<ErrorCantCheckin>());
        }

        [Test]
        public void Approved_customer_cant_checkin_to_unknown_Roller() {
            _sut.Register("name", "phone");
            _sut.Approve("A1B2C3", "1234");
            var result = _sut.Checkin("1234", "xxxx");
            Assert.That(result, Is.TypeOf<ErrorCantCheckin>());
        }
        
        [Test]
        public void Unknown_customer_cant_checkin_to_Roller() {
            _sut.Checkin("1234", "abcd");
            var result = _sut.Checkin("1234", "abcd");
            Assert.That(result, Is.TypeOf<ErrorCantCheckin>());
        }
        
        
        [Test]
        public void Approved_customer_can_checkout_from_checked_in_Roller() {
            _sut.Register("name", "phone");
            _sut.Approve("A1B2C3", "1234");
            _sut.Checkin("1234", "abcd");
            var result = _sut.Checkout("1234", "abcd");
            Assert.That(result, Is.TypeOf<CheckoutResult>());
            Assert.That(((CheckoutResult)result).Id, Is.EqualTo("1234"));
            Assert.That(((CheckoutResult)result).Duration, Is.EqualTo(new TimeSpan(0, 0, 5, 0)));
        }
    }
}