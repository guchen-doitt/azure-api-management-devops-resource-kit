﻿using Microsoft.Azure.Management.ApiManagement.ArmTemplates.Create;

namespace Microsoft.Azure.Management.ApiManagement.ArmTemplates.Common
{
    public class FileNameGenerator
    {

        public FileNames GenerateFileNames(string baseFileName)
        {
            // generate useable object with file names for consistency throughout project
            return new FileNames()
            {
                apiVersionSets = $@"/{baseFileName}-apiVersionSets.template.json",
                authorizationServers = $@"/{baseFileName}-authorizationServers.template.json",
                backends = $@"/{baseFileName}-backends.template.json",
                globalServicePolicy = $@"/{baseFileName}-globalServicePolicy.template.json",
                loggers = $@"/{baseFileName}-loggers.template.json",
                namedValues = $@"/{baseFileName}-namedValues.template.json",
                tags = $@"/{baseFileName}-tags.template.json",
                products = $@"/{baseFileName}-products.template.json",
                parameters = $@"/{baseFileName}-parameters.json",
                linkedMaster = $@"/{baseFileName}-master.template.json",
                apis = "/Apis",
                splitAPIs = "/SplitAPIs",
                versionSetMasterFolder = "/VersionSetMasterFolder",
                revisionMasterFolder = "/RevisionMasterFolder",
                groupAPIsMasterFolder = "/MultipleApisMasterFolder"
            };
        }

        public string GenerateCreatorAPIFileName(string apiName, bool isSplitAPI, bool isInitialAPI, string apimServiceName)
        {
            // in case the api name has been appended with ;rev={revisionNumber}, take only the api name initially provided by the user in the creator config
            string sanitizedAPIName = GenerateOriginalAPIName(apiName);
            if (isSplitAPI == true)
            {
                return isInitialAPI == true ? $@"/{apimServiceName}-{sanitizedAPIName}-initial.api.template.json" : $@"/{apimServiceName}-{sanitizedAPIName}-subsequent.api.template.json";
            }
            else
            {
                return $@"/{apimServiceName}-{sanitizedAPIName}.api.template.json";
            }
        }

        public string GenerateExtractorAPIFileName(string singleAPIName, string apimServiceName)
        {
            return singleAPIName == null ? $@"/{apimServiceName}-apis.template.json" : $@"/{apimServiceName}-{singleAPIName}-api.template.json";
        }

        public string GenerateOriginalAPIName(string apiName)
        {
            // in case the api name has been appended with ;rev={revisionNumber}, take only the api name initially provided by the user in the creator config
            string originalName = apiName.Split(";")[0];
            return originalName;
        }
    }

    public class FileNames
    {
        public string apiVersionSets { get; set; }
        public string authorizationServers { get; set; }
        public string backends { get; set; }
        public string globalServicePolicy { get; set; }
        public string loggers { get; set; }
        public string namedValues { get; set; }
        public string tags { get; set; }
        public string products { get; set; }
        public string parameters { get; set; }
        // linked property outputs 1 master template
        public string linkedMaster { get; set; }
        public string apis { get; set; }
        public string splitAPIs { get; set; }
        public string versionSetMasterFolder { get; set; }
        public string revisionMasterFolder { get; set; }
        public string groupAPIsMasterFolder{ get; set; }
    }
}
