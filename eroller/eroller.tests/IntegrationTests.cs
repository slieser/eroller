using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace eroller.tests
{
    public class IntegrationTests
    {
        [Test]
        public void Smoketest() {
            var task = Task.Run(() => Program.Main(new[] {""}));
            Thread.Sleep(2000);
            Assert.That(task.Exception, Is.Null);
        }
    }
}