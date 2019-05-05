using System;
using System.Collections.Generic;
using System.Text;

namespace StoicMeditations.AzureFunctions.Models
{
    public class Results<T> where T : class, new()
    {
        public T Result { get; set; }
        public int? ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public Results(T successfulResult)
        {
            Result = successfulResult;
        }
        public Results(int errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
    }
}
