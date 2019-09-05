using eroller.logic;
using Nancy;
using Nancy.TinyIoc;

namespace eroller.web
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container) {
            base.ConfigureApplicationContainer(container);
            container.Register<Interactors>().AsSingleton();
        }

    }
}