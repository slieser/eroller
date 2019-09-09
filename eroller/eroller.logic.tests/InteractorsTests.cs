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
            _sut = new Interactors();
        }
        
        [Test]
        public void Register_returns_an_id() {
            var result = _sut.Register("stefan", "12345");
            Assert.That(result, Is.TypeOf<OkResult>());
            Assert.That(((OkResult)result).Id.Length, Is.GreaterThan(4));
        }

        [Test]
        public void Approve_with_wrong_code_returns_an_error() {
            var result = _sut.Approve("wrong", "");
            Assert.That(result, Is.TypeOf<ErrorCantApprove>());
        }

        [Test]
        public void Approve_with_correct_code_returns_the_id() {
            var result = _sut.Approve("1234", "12345678");
            Assert.That(result, Is.TypeOf<OkResult>());
            Assert.That(((OkResult)result).Id, Is.EqualTo("12345678"));
        }
    }
}