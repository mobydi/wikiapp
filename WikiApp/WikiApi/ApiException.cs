using System;

namespace WikiApp.WikiApi
{
    public class ApiException : Exception
    {
        public string Code { get; }
        public string Info { get; }

        public ApiException(string code, string info)
        {
            Code = code;
            Info = info;
        }
    }
}