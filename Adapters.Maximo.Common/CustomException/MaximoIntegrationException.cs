using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Tlm.Fed.Adapters.Maximo.Common.CustomException
{
    public class MaximoIntegrationException : Exception
    {
        public MaximoIntegrationException()
        {

        }

        public MaximoIntegrationException(string context, object request, string httpStatusCode, string errorMessage)
                        : base($"Error occured while creating {context} for request {JsonConvert.SerializeObject(request)} with httpStatus code {httpStatusCode} and error message {errorMessage}")
        {

        }
    }
}
