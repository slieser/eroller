using eroller.logic;
using Nancy;

namespace eroller.web
{
    public class RegisterModule : NancyModule
    {
        public RegisterModule(Interactors interactors) {
            Get("/register", context => {
                var name = Request.Query["name"];
                var phone = Request.Query["phone"];
                return interactors.Register(name, phone);
            });

            Get("/approve/{id}", context => {
                var id = context.id;
                var code = Request.Query["code"];
                return interactors.Approve(code, id);
            });
        }
    }
}