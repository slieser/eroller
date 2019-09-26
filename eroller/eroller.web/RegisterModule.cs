using eroller.logic;
using eroller.logic.data;
using Nancy;

namespace eroller.web
{
    public class RegisterModule : NancyModule
    {    
        public RegisterModule(Interactors interactors) {
            Get("/register", context => {
                var name = Request.Query["name"];
                var phone = Request.Query["phone"];
                var result = interactors.Register(name, phone);
                var json = FormatterExtensions.AsJson<Result>(Response, result);
                return json;
            });

            Get("/approve/{id}", context => {
                var id = context.id;
                var code = Request.Query["code"];
                var result = interactors.Approve(code, id);
                var json = FormatterExtensions.AsJson<Result>(Response, result);
                return json;
            });
        }
    }
}