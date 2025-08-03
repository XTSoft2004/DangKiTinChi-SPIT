using ModelsHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace HelperHttpClient
{
    public partial class RequestHttpClient
    {
        public string Address()
        {
            if (_response != null && _response.RequestMessage != null && _response.RequestMessage.RequestUri != null)
                return _response.RequestMessage.RequestUri.ToString();
            else
                return string.Empty;
        }

        private string GET_URL_FIRST(string url)
        {
            Uri uri = new Uri(url);
            return uri.GetLeftPart(UriPartial.Authority);
        }

        private string GET_URL_LAST(string url)
        {
            Uri uri = new Uri(url);
            return uri.AbsolutePath;
        }
    }
}