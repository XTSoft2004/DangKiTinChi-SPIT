using Domain.Base.Services;
using Domain.Common;
using Domain.Common.Http;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Model.Request.InfoProxy;
using Domain.Model.Response.InfoProxy;
using Domain.Model.Response.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class InfoProxyServices : BaseService, IInfoProxyServices
    {
        private readonly IRepositoryBase<InfoProxy> _infoProxy;
        private readonly IRepositoryBase<AccountClasses> _accountClasses;
        private readonly ITokenServices _tokenServices;
        private UserTokenResponse? userMeToken;
        public InfoProxyServices(IRepositoryBase<InfoProxy> infoProxy, IRepositoryBase<AccountClasses> accountClasses, ITokenServices tokenServices)
        {
            _infoProxy = infoProxy;
            _accountClasses = accountClasses;
            _tokenServices = tokenServices;
            userMeToken = _tokenServices.GetTokenBrowser();
        }
        public async Task<HttpResponse> CreateAsync(InfoProxyRequest FormData)
        {
            var infoProxy = new InfoProxy
            {
                Proxy = FormData.Proxy,
                TypeProxy = FormData.TypeProxy,
                Status = Status_Proxy_Enum.Active,
                CreatedDate = DateTime.Now,
                CreatedBy = userMeToken?.Username,
            };

            _infoProxy.Insert(infoProxy);
            await UnitOfWork.CommitAsync();

            return HttpResponse.OK(message: "Thêm mới proxy thành công", statusCode: System.Net.HttpStatusCode.OK);
        }
        public async Task<HttpResponse> UpdateAsync(long? id, InfoProxyRequest FormData)
        {
            var infoProxy = await _infoProxy.FindAsync(x => x.Id == id);
            if (infoProxy == null)
            {
                return HttpResponse.Error(message: "Proxy không tồn tại", statusCode: System.Net.HttpStatusCode.NotFound);
            }

            Status_Proxy_Enum? status_Proxy_Enum = null;
            if (FormData.Status != null)
            {
                status_Proxy_Enum = EnumExtensions.GetEnumFromDisplayName<Status_Proxy_Enum>(FormData.Status);
                if (status_Proxy_Enum == null)
                {
                    return HttpResponse.Error(message: "Trạng thái proxy không hợp lệ", statusCode: System.Net.HttpStatusCode.BadRequest);
                }
            }

            infoProxy.Proxy = FormData.Proxy;
            infoProxy.TypeProxy = FormData.TypeProxy;
            infoProxy.Status = (Status_Proxy_Enum)status_Proxy_Enum;
            infoProxy.ModifiedDate = DateTime.Now;
            infoProxy.ModifiedBy = userMeToken?.Username;

            _infoProxy.Update(infoProxy);
            await UnitOfWork.CommitAsync();
            return HttpResponse.OK(message: "Cập nhật proxy thành công");
        }
        public async Task<HttpResponse> DeleteAsync(long? id)
        {
            var infoProxy = await _infoProxy.FindAsync(x => x.Id == id);

            if (infoProxy == null)
            {
                return HttpResponse.Error(message: "Proxy không tồn tại", statusCode: System.Net.HttpStatusCode.NotFound);
            }

            _infoProxy.Delete(infoProxy);
            await UnitOfWork.CommitAsync();

            return HttpResponse.OK(message: "Xóa proxy thành công", statusCode: System.Net.HttpStatusCode.OK);
        }
        public async Task<(List<InfoProxyResponse>, int TotalRecords)> GetAllAsync(string search, int pageNumber, int pageSize)
        {
            var query = _infoProxy.All()
                .Include(search => search.AccountClasses)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                query = query.Where(x => x.Proxy.Contains(search)
                                    || x.TypeProxy.Contains(search)
                                    || x.Status.GetEnumDisplayName().ToLower().Contains(search));
            }

            var TotalRecords = await query.CountAsync();

            if (pageSize != -1 && pageNumber != -1)
            {
                query = query.OrderByDescending(d => d.ModifiedDate)
                             .Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize);
            }

            var infoProxySearch = await query.Select(x => new InfoProxyResponse
            {
                Id = x.Id,
                Proxy = x.Proxy,
                TypeProxy = x.TypeProxy,
                Status = x.Status.GetEnumDisplayName(),
            }).ToListAsync();

            return (infoProxySearch, TotalRecords);
        }
    }
}
