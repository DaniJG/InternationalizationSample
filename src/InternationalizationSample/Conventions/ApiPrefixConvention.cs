using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternationalizationSample.Conventions
{
    //Thanks to Filip W for his blog posts: 
    //  http://www.strathweb.com/2016/06/global-route-prefix-with-asp-net-core-mvc-revisited/
    //  http://www.strathweb.com/2015/10/global-route-prefixes-with-attribute-routing-in-asp-net-5-and-mvc-6/

    public class ApiPrefixConvention : IApplicationModelConvention
    {
        private readonly AttributeRouteModel apiPrefix;
        private readonly AttributeRouteModel apiCulturePrefix;
        private readonly AttributeRouteModel apiRouteWithController;
        private readonly AttributeRouteModel apiCultureRouteWithController;

        public ApiPrefixConvention()
        {
            //These are meant to be combined with existing route attributes
            apiPrefix = new AttributeRouteModel(
                new RouteAttribute("api/"));
            apiCulturePrefix = new AttributeRouteModel(
                new RouteAttribute("api/{culture:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/"));

            //These are meant to be added as routes for api controllers that do not specify any route attribute
            apiRouteWithController = new AttributeRouteModel(
                new RouteAttribute("api/[controller]"));
            apiCultureRouteWithController = new AttributeRouteModel(
                new RouteAttribute("api/{culture:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/[controller]"));
        }

        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers.Where(c => c.ControllerName.EndsWith("Api")))
            {
                ApplyControllerConvention(controller);
            }
        }

        private void ApplyControllerConvention(ControllerModel controller)
        {
            //Remove the "Api" suffix from the controller name 
            //(The "Controller" suffix is already removed by default conventions)
            controller.ControllerName = controller.ControllerName.Substring(0, controller.ControllerName.Length - 3);

            //Either update existing route attributes or add new ones
            if (controller.Selectors.Any(x => x.AttributeRouteModel != null))
            {
                AddPrefixesToExistingRoutes(controller);
            }
            else
            {
                AddNewRoutes(controller);
            }
        }

        private void AddPrefixesToExistingRoutes(ControllerModel controller)
        {
            foreach (var selectorModel in controller.Selectors.Where(x => x.AttributeRouteModel != null).ToList())
            {
                var originalAttributeRoute = selectorModel.AttributeRouteModel;
                //Merge controller selector with the api prefix
                selectorModel.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(apiPrefix,
                    originalAttributeRoute);

                //Add another selector with the culture api prefix
                var cultureSelector = new SelectorModel(selectorModel);
                cultureSelector.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(apiCulturePrefix,
                    originalAttributeRoute);
                controller.Selectors.Add(cultureSelector);
            }
        }

        private void AddNewRoutes(ControllerModel controller)
        {
            //The controller has no route attributes, lets add a default api convention 
            var defaultSelector = controller.Selectors.First(s => s.AttributeRouteModel == null);
            defaultSelector.AttributeRouteModel = apiRouteWithController;
            //Lets add another selector for the api with culture convention
            controller.Selectors.Add(new SelectorModel { AttributeRouteModel = apiCultureRouteWithController });
        }
    }
}
