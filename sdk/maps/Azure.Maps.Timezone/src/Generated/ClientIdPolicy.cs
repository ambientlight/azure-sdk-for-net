// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// added manually to hotfix Long Running Operation final state GET request that also need x-ms-client-id added

using Azure.Core.Pipeline;
using Azure.Core;

namespace Azure.Maps.Timezone {
    internal class ClientIdPolicy : HttpPipelineSynchronousPolicy
    {
        private readonly string _xMsClientId;
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientIdPolicy"/> class.
        /// </summary>
        /// <param name="xMsClientId">Specifies which account is intended for usage in conjunction with the Azure AD security model.  It represents a unique ID for the Azure Maps account and can be retrieved from the Azure Maps management  plane Account API. To use Azure AD security in Azure Maps see the following [articles](https://aka.ms/amauthdetails) for guidance. </param>
        public ClientIdPolicy(string xMsClientId)
        {
            Argument.AssertNotNullOrEmpty(xMsClientId, nameof(xMsClientId));
            _xMsClientId = xMsClientId;
        }

        /// <inheritdoc/>
        public override void OnSendingRequest(HttpMessage message)
        {
            base.OnSendingRequest(message);
            message.Request.Headers.SetValue("x-ms-client-id", _xMsClientId);
        }
    }
}
