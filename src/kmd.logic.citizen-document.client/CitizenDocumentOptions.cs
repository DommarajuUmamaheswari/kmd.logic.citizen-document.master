using System;
using System.Collections.Generic;
using System.Text;

namespace kmd.logic.citizen_document.client
{
    /// <summary>
    /// Provide the configuration options for using the Citizen Document service.
    /// </summary>
    public sealed class CitizenDocumentOptions
    {
        /// <summary>
        /// Gets or sets the Logic Citizen Document service.
        /// </summary>
        /// <remarks>
        /// This option should not be overridden except for testing purposes.
        /// </remarks>
        public Uri Serviceuri { get; set; } = new Uri("https://kmd-logic-api-shareddev-webapp.azurewebsites.net");
    
    }
}
