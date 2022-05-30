using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text;

namespace Tlm.Fed.Adapters.Sap.Common.CustomException
{
    [ExcludeFromCodeCoverage]
    public class SapIntegrationException : Exception
    {
        public SapIntegrationException()
        {

        }

        public SapIntegrationException(string context, object request, string httpStatusCode, string errorMessage)
                        : base($"Error occured while creating {context} for request {JsonConvert.SerializeObject(request)} with httpStatus code {httpStatusCode} and error message {errorMessage}")
        {

        }
    }
}
