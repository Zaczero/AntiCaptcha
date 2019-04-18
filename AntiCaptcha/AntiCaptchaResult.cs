using System.Collections.Generic;

namespace _AntiCaptcha
{
    public struct AntiCaptchaResult
    {
        public bool Success;
        public string Response;

        public AntiCaptchaResult(bool success, string response)
        {
            Success = success;
            Response = response;
        }
    }

    internal struct InternalAntiCaptchaResult
    {
        public bool Success;
        public string Response;
        public Dictionary<string, object> Dictionary;

        public InternalAntiCaptchaResult(bool success, string response, Dictionary<string, object> dictionary)
        {
            Success = success;
            Response = response;
            Dictionary = dictionary;
        }
    }
}
