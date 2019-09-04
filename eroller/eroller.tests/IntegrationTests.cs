using NUnit.Framework;

namespace eroller.tests
{
    public class IntegrationTests
    {
        [Test]
        public void Smoketest() {
            Program.Main(new[]{""});
        }
    }
}