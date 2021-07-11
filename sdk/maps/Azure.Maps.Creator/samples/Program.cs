// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Maps.Creator;
using Azure.Maps.Creator.Models;
using Azure.Identity;
using Microsoft.Azure.Management.Maps;
using System.Text.RegularExpressions;
using System.Text.Json;

namespace Azure.Maps.Creator.Samples
{
    public static class Program
    {
        internal static async Task UploadAndThenDeleteResource(DataClient client, object resourceContent, UploadDataFormat dataFormat){
            Console.WriteLine("Started Upload");
            var operation = await client.StartUploadPreviewAsync(dataFormat, resourceContent);

            Console.WriteLine("Wait for Completion");
            Response<LongRunningOperationResult> result;
            try {
                result = await operation.WaitForCompletionAsync();
            } catch (Exception e) {
                Console.Error.WriteLine(e);
                return;
            }

            string resourceLocation;
            var hasResourceLocation = result.GetRawResponse().Headers.TryGetValue("Resource-Location", out resourceLocation);
            if (!hasResourceLocation){
                // resource location not available
                throw new Exception("Resource location not provided");
            }

            var uuidRule = new Regex(@"[0-9A-Fa-f\-]{36}");
            var match = uuidRule.Match(resourceLocation);
            if (!match.Success){
                // return with error
                throw new Exception("Unable to extract resource uuid from resource location.");
            }

            var uuid = match.Value;
            Console.WriteLine("Deleting resource");
            await client.DeletePreviewAsync(uuid);
            Console.WriteLine("Done");
        }

        public static async Task Main(string[] args)
        {
            var credential = new DefaultAzureCredential(true);
            // x-ms-client id from your Azure Maps Account resource (found in Authentication tab)
            var clientId = Environment.GetEnvironmentVariable("CLIENT_ID");
            var dataClient = new DataClient(credential, Geography.Us, clientId);

            // Approach #0: create a feature directly
            var feature = new GeoJsonFeature(new GeoJsonPoint(new double[]{-122.126986, 47.639754}));
            feature.Properties = JsonDocument.Parse("{\"geometryId\": \"001\", \"radius\": 50}").RootElement;
            var featureCollection = new GeoJsonFeatureCollection(
                new GeoJsonFeature[]{feature}
            );

            Console.WriteLine("Uploading geojson feature collection");
            await UploadAndThenDeleteResource(dataClient, featureCollection, UploadDataFormat.Geojson);

            // Approach #2: parse a json document
            Console.WriteLine("Uploading json document");
            var geojson = JsonDocument.Parse(File.ReadAllText(@"./resources/data_sample_upload.json"));
            await UploadAndThenDeleteResource(dataClient, geojson, UploadDataFormat.Geojson);

            // Approach #3: read raw bytes
            Console.WriteLine("Uploading DWG zip");
            var bytes = File.ReadAllBytes(@"./resources/data_sample_upload.zip");
            await UploadAndThenDeleteResource(dataClient, bytes, UploadDataFormat.Dwgzippackage);
        }
    }
}
