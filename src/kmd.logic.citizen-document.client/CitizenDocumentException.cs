using System;
using System.Runtime.Serialization;

namespace kmd.logic.citizen_document.client
{
    [System.Serializable]
    public class CitizenDocumentException : Exception
    {
        public string InnerMessage { get; }

        public CitizenDocumentException()
        {
        }
        public CitizenDocumentException(string message, Microsoft.Rest.HttpOperationResponse<object> response)
            : base(message)
        {
        }

        public CitizenDocumentException(string message, string innerMessage)
           : base(message)
        {
            this.InnerMessage = innerMessage;
        }

        public CitizenDocumentException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected CitizenDocumentException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}


