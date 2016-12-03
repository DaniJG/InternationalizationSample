using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace InternationalizationSample.CultureProviders
{
    public class LocalizationFilterPipeline
    {
        public void Configure(IApplicationBuilder applicationBuilder)
        {
            var supportedCultures = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("en-US"),
                new CultureInfo("es"),
                new CultureInfo("es-ES")
            };
            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };
            options.RequestCultureProviders.Insert(0, new RouteDataRequestCultureProvider() { Options = options });
            applicationBuilder.UseRequestLocalization(options);
        }
    }
}
