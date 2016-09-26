﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InternationalizationSample.CultureProviders
{
    public class UrlRequestCultureProvider: IRequestCultureProvider
    {
        public Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            var url = httpContext.Request.Path;

            //Quick and dirty parsing of language from url path, which looks like "/api/es-ES/hello-world"
            var parts = httpContext.Request.Path.Value.Split('/').Where(p => !String.IsNullOrWhiteSpace(p)).ToList();
            if (parts.Count == 0)
            {
                return Task.FromResult<ProviderCultureResult>(null);
            }

            var cultureSegmentIndex = parts.Contains("api") ? 1 : 0;
            var hasCulture = Regex.IsMatch(parts[cultureSegmentIndex], @"^[a-z]{2}(?:-[A-Z]{2})?$");
            if (!hasCulture)
            {
                return Task.FromResult<ProviderCultureResult>(null);
            }

            var culture = parts[cultureSegmentIndex];
            return Task.FromResult(new ProviderCultureResult(culture));
        }
    }
}
