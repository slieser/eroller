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
            _sut = new Interactors(() => "1234", () => "A1B2C3");
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
        public void Approve_with_correct_code_returns_the_id() {
            _sut.Register("name", "phone");
            var result = _sut.Approve("A1B2C3", "1234");
            Assert.That(result, Is.TypeOf<OkResult>());
            Assert.That(((OkResult)result).Id, Is.EqualTo("1234"));
        }
    }
}