using System;

namespace CognitiveServices.FaceApi.Helpers
{
    public static class UrlHelper
    {
        public static bool Check(string url)
        {
            return Uri.IsWellFormedUriString(url, UriKind.Absolute);
        }
    }
}
