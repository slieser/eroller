using System.Threading.Tasks;
using eroller.logic;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;

namespace eroller.web.tests
{
    public class IntegrationTests
    {
        private Browser _browser;

        [SetUp]
        public void Setup() {
            var bootstrapper = new ConfigurableBootstrapper(with => {
                with.Module(new RegisterModule(new Interactors()));
            });
            _browser = new Browser(bootstrapper);
        }
        
        [Test]
        public async Task Subscribe() {
            var result = await _browser.Get("/register", with => {
                with.HttpRequest();
                with.Query("name", "Stefan Lieser");
                with.Query("phone", "123456578");
            });
        
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            StringAssert.StartsWith("{\"id\":", result.Body.AsString());
        }
        
        [Test]
        public async Task Approve_with_correct_code() {
            var result = await _browser.Get("/approve/0815", with => {
                with.HttpRequest();
                with.Query("code", "1234");
            });
        
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            StringAssert.StartsWith("{\"id\":", result.Body.AsString());
        }

    }
}