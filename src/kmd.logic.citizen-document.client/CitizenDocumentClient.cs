using kmd.logic.citizen_document.client.Models;
using Kmd.Logic.Identity.Authorization;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace kmd.logic.citizen_document.client
{
    public sealed class CitizenDocumentClient
    {
        private readonly HttpClient httpClient;
        private readonly CitizenDocumentOptions options;
        private readonly LogicTokenProviderFactory tokenProviderFactory;

        private InternalClient internalClient;

        public CitizenDocumentClient(HttpClient httpClient, LogicTokenProviderFactory tokenProviderFactory, CitizenDocumentOptions options)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.tokenProviderFactory = tokenProviderFactory ?? throw new ArgumentNullException(nameof(tokenProviderFactory));
        }

        public async Task<CitizenDocumentUploadResponse> UploadAttachmentWithHttpMessagesAsync(Guid subscriptionId,string configurationId, int retentionPeriodInDays, string cpr, string documentType, Stream document)
        {
            var client = this.CreateClient();

            var response = await client.UploadAttachmentWithHttpMessagesAsync(
                                subscriptionId: subscriptionId,
                                configurationId: configurationId,
                                retentionPeriodInDays: retentionPeriodInDays,
                                cpr: cpr,
                                documentType: documentType,
                                document: document).ConfigureAwait(false);

            switch (response.Response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return (CitizenDocumentUploadResponse)response.Body;

                case System.Net.HttpStatusCode.NotFound:
                    return null;

                case System.Net.HttpStatusCode.Unauthorized:
                    throw new CitizenDocumentException("Unauthorized ",  response.Body as string);

                default:
                    throw new CitizenDocumentException("Invalid configuration provided to access Citizen Document service", response.Body as string);
            }
        }

        public async Task<SendCitizenDocumentResponse> SendDocumentWithHttpMessagesAsync(Guid subscriptionId,SendCitizenDocumentRequest sendCitizenDocumentRequest)
        {
            var client = this.CreateClient();

            var response = await client.SendDocumentWithHttpMessagesAsync(
                                subscriptionId: subscriptionId,
                                sendCitizenDocumentRequest: sendCitizenDocumentRequest).ConfigureAwait(false);
                      
            switch (response.Response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return (SendCitizenDocumentResponse)response.Body;

                case System.Net.HttpStatusCode.NotFound:
                    return null;

                case System.Net.HttpStatusCode.Unauthorized:
                    throw new CitizenDocumentException("Unauthorized ", (response.Response.Content.ReadAsStringAsync()).Result as string);

                default:
                    throw new CitizenDocumentException("Invalid configuration provided to access CPR service", (response.Response.Content.ReadAsStringAsync()).Result as string);
            }
        }

        private InternalClient CreateClient()
        {
            if (this.internalClient != null)
            {
                return this.internalClient;
            }

            var tokenProvider = this.tokenProviderFactory.GetProvider(this.httpClient);

            this.internalClient = new InternalClient(new TokenCredentials(tokenProvider))
            {
                BaseUri = this.options.Serviceuri,
            };

            return this.internalClient;
        }

    }
}
