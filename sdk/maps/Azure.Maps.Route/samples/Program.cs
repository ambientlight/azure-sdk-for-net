// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Identity;
using Azure.Maps.Route;
using Azure.Maps.Route.Models;

namespace Azure.Maps.Route.Samples
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var credential = new DefaultAzureCredential(true);
            var clientId = Environment.GetEnvironmentVariable("CLIENT_ID");
            var routeClient = new RouteClient(credential, Geography.Us, clientId);

            var resp = await routeClient.GetRouteDirectionsAsync(avoid: new RouteAvoidType[]{RouteAvoidType.TollRoads, RouteAvoidType.Motorways},
                format: TextFormat.Json,
                query: "24.768,121.061:25.0409,121.56716"
            );

            Console.WriteLine(resp);
            Console.WriteLine("completed");
        }
    }
}
