using eroller.logic;
using eroller.logic.data;
using Nancy;

namespace eroller.web
{
    public class RollerModule : NancyModule
    {
        public RollerModule(Interactors interactors) {
            Get("/checkin/{id}", context => {
                var id = context.id;
                var rollerId = Request.Query["rollerid"];
                var result = interactors.Checkin(id, rollerId);
                var json = FormatterExtensions.AsJson<Result>(Response, result);
                return json;
            });
 
            Get("/checkout/{id}", context => {
                var id = context.id;
                var rollerId = Request.Query["rollerid"];
                var result = interactors.Checkout(id, rollerId);
                var json = FormatterExtensions.AsJson<Result>(Response, result);
                return json;
            });
        }
    }
}