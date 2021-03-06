﻿using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Azure.Management.ApiManagement.ArmTemplates.Common;
using Microsoft.Azure.Management.ApiManagement.ArmTemplates.Create;
using Microsoft.Extensions.Caching.Memory;

namespace Microsoft.Azure.Management.ApiManagement.ArmTemplates.Extract
{
    public class EntityExtractor
    {
        public string baseUrl = "https://management.azure.com";
        internal Authentication auth = new Authentication();
        private static readonly IMemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

        public static async Task<string> CallApiManagementAsync(string azToken, string requestUrl)
        {
            if (_cache.TryGetValue(requestUrl, out string cachedResponseBody))
            {
                return cachedResponseBody;
            }

            using (HttpClient httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", azToken);

                HttpResponseMessage response = await httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                _cache.Set(requestUrl, responseBody);

                return responseBody;
            }
        }

        public Template GenerateEmptyTemplate()
        {
            TemplateCreator templateCreator = new TemplateCreator();
            Template armTemplate = templateCreator.CreateEmptyTemplate();
            return armTemplate;
        }

        public Template GenerateEmptyPropertyTemplateWithParameters(Extractor exc)
        {
            Template armTemplate = GenerateEmptyTemplate();
            armTemplate.parameters = new Dictionary<string, TemplateParameterProperties> { { "ApimServiceName", new TemplateParameterProperties() { type = "string" } } };
            if (exc.policyXMLBaseUrl != null && exc.policyXMLSasToken != null)
            {
                TemplateParameterProperties policyTemplateSasTokenParameterProperties = new TemplateParameterProperties()
                {
                    type = "string"
                };
                armTemplate.parameters.Add("PolicyXMLSasToken", policyTemplateSasTokenParameterProperties);
            }
            if (exc.policyXMLBaseUrl != null)
            {
                TemplateParameterProperties policyTemplateBaseUrlParameterProperties = new TemplateParameterProperties()
                {
                    type = "string"
                };
                armTemplate.parameters.Add("PolicyXMLBaseUrl", policyTemplateBaseUrlParameterProperties);
            }
            if (exc.paramNamedValue)
            {
                TemplateParameterProperties namedValueParameterProperties = new TemplateParameterProperties()
                {
                    type = "object"
                };
                armTemplate.parameters.Add("NamedValues", namedValueParameterProperties);
            }
            return armTemplate;
        }

        public Template GenerateEmptyTemplateWithParameters(string policyXMLBaseUrl, string policyXMLSasToken)
        {
            Template armTemplate = GenerateEmptyTemplate();
            armTemplate.parameters = new Dictionary<string, TemplateParameterProperties> { { "ApimServiceName", new TemplateParameterProperties() { type = "string" } } };
            if (policyXMLBaseUrl != null && policyXMLSasToken != null)
            {
                TemplateParameterProperties policyTemplateSasTokenParameterProperties = new TemplateParameterProperties()
                {
                    type = "string"
                };
                armTemplate.parameters.Add("PolicyXMLSasToken", policyTemplateSasTokenParameterProperties);
            }
            if (policyXMLBaseUrl != null)
            {
                TemplateParameterProperties policyTemplateBaseUrlParameterProperties = new TemplateParameterProperties()
                {
                    type = "string"
                };
                armTemplate.parameters.Add("PolicyXMLBaseUrl", policyTemplateBaseUrlParameterProperties);
            }
            return armTemplate;
        }

        public Template GenerateEmptyApiTemplateWithParameters(Extractor exc)
        {
            Template armTemplate = GenerateEmptyTemplate();
            armTemplate.parameters = new Dictionary<string, TemplateParameterProperties> { { "ApimServiceName", new TemplateParameterProperties() { type = "string" } } };
            if (exc.policyXMLBaseUrl != null && exc.policyXMLSasToken != null)
            {
                TemplateParameterProperties policyTemplateSasTokenParameterProperties = new TemplateParameterProperties()
                {
                    type = "string"
                };
                armTemplate.parameters.Add("PolicyXMLSasToken", policyTemplateSasTokenParameterProperties);
            }
            if (exc.policyXMLBaseUrl != null)
            {
                TemplateParameterProperties policyTemplateBaseUrlParameterProperties = new TemplateParameterProperties()
                {
                    type = "string"
                };
                armTemplate.parameters.Add("PolicyXMLBaseUrl", policyTemplateBaseUrlParameterProperties);
            }
            if (exc.paramServiceUrl || (exc.serviceUrlParameters != null && exc.serviceUrlParameters.Length > 0))
            {
                TemplateParameterProperties serviceUrlParamProperty = new TemplateParameterProperties()
                {
                    type = "object"
                };
                armTemplate.parameters.Add("serviceUrl", serviceUrlParamProperty);
            }
            if (exc.paramApiLoggerId)
            {
                TemplateParameterProperties apiLoggerProperty = new TemplateParameterProperties()
                {
                    type = "object"
                };
                armTemplate.parameters.Add("ApiLoggerId", apiLoggerProperty);
            }
            return armTemplate;
        }
    }
}