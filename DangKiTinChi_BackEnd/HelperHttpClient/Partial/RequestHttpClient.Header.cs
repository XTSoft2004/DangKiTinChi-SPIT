using System;
using System.Collections.Generic;

namespace HelperHttpClient
{
    public partial class RequestHttpClient
    {
        // Header Management Methods
        public void SetHeader(string key, string value)
        {
            if (_client.DefaultRequestHeaders.Contains(key))
                _client.DefaultRequestHeaders.Remove(key);
            _client.DefaultRequestHeaders.TryAddWithoutValidation(key, value);
        }

        public void SetHeader(Dictionary<string, string> headers, bool isClear = false)
        {
            if (headers == null)
                throw new ArgumentNullException(nameof(headers));

            if (isClear)
                _client.DefaultRequestHeaders.Clear();

            foreach (KeyValuePair<string, string> header in headers)
            {
                SetHeader(header.Key, header.Value);
            }
        }
    }
}