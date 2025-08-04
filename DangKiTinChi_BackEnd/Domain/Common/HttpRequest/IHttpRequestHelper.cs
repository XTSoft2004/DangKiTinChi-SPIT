using Domain.Entities;
using HelperHttpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.HttpRequest
{
    public interface IHttpRequestHelper
    {
        RequestHttpClient Client { get; }
        string GetResponse(HttpResponseMessage httpResponseMessage);
        void SetAccount(Account account);
    }
}
