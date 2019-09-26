using System;
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
                with.Module(new RegisterModule(
                    new Interactors(() => "1234", () => "A1B2C3", () => new DateTime())));
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
            Assert.That(result.Body.AsString(), Is.EqualTo("{\"id\":\"1234\"}"));
        }
        
        [Test]
        public async Task Approve_with_correct_code() {
            await _browser.Get("/register", with => {
                with.HttpRequest();
                with.Query("name", "Stefan Lieser");
                with.Query("phone", "123456578");
            });
            var result = await _browser.Get("/approve/1234", with => {
                with.HttpRequest();
                with.Query("code", "A1B2C3");
            });
        
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.Body.AsString(), Is.EqualTo("{\"id\":\"1234\"}"));
        }
    }
}