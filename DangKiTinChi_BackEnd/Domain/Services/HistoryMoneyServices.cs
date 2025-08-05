using Domain.Base.Services;
using Domain.Common.Http;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Model.Request.HistoryMoney;
using Domain.Model.Response.HistoryMoney;
using Domain.Model.Response.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class HistoryMoneyServices : BaseService, IHistoryMoneyServices
    {
        private readonly IRepositoryBase<HistoryMoney> _historyMoney;
        private readonly IRepositoryBase<User> _user;
        private readonly ITokenServices _tokenServices;
        private UserTokenResponse? userMeToken;
        public HistoryMoneyServices(IRepositoryBase<HistoryMoney> historyMoney, IRepositoryBase<User> user, ITokenServices tokenServices)
        {
            _historyMoney = historyMoney;
            _user = user;
            _tokenServices = tokenServices;
            userMeToken = _tokenServices.GetTokenBrowser();
        }
        public async Task<HttpResponse> CreateAsync(HistoryMoneyRequest FormData)
        {
            var user = _user.Find(u => u.Id == FormData.UserId);
            if (user == null)
                return HttpResponse.Error(message: "Tài khoản không tồn tại", statusCode: System.Net.HttpStatusCode.NotFound);

            var historyMoney = new HistoryMoney
            {
                Money = FormData.Money,
                Title = FormData.Title,
                Description = FormData.Description,
                UserId = FormData.UserId,
                CreatedDate = DateTime.Now,
                CreatedBy = userMeToken.Username
            };

            _historyMoney.Insert(historyMoney);
            await UnitOfWork.CommitAsync();

            return HttpResponse.OK(message: "Lịch sử thay đổi tiền đã được tạo thành công!", statusCode: System.Net.HttpStatusCode.Created);
        }
        public async Task<HttpResponse> UpdateAsync(long? id, HistoryMoneyRequest FormData)
        {
            var history = _historyMoney.Find(h => h.Id == id);

            if (history == null)
                return HttpResponse.Error(message: "Lịch sử thay đổi tiền không tồn tại", statusCode: System.Net.HttpStatusCode.NotFound);

            if(history.UserId != null && history.UserId != FormData.UserId)
            {
                var user = _user.Find(u => u.Id == FormData.UserId);
                if (user == null)
                    return HttpResponse.Error(message: "Tài khoản không tồn tại", statusCode: System.Net.HttpStatusCode.NotFound);

                history.UserId = user.Id;
                history.User = user;    
            }

            history.Money = FormData.Money;
            history.Title = FormData.Title;
            history.Description = FormData.Description;
            history.ModifiedDate = DateTime.Now;
            history.ModifiedBy = userMeToken?.Username;

            _historyMoney.Update(history);
            await UnitOfWork.CommitAsync();

            return HttpResponse.OK(message: "Lịch sử thay đổi tiền đã được cập nhật thành công!", statusCode: System.Net.HttpStatusCode.OK);
        }
        public async Task<HttpResponse> DeleteAsync(long? id)
        {
            var history = _historyMoney.Find(h => h.Id == id);

            if (history == null)
                return HttpResponse.Error(message: "Lịch sử thay đổi tiền không tồn tại", statusCode: System.Net.HttpStatusCode.NotFound);

            _historyMoney.Delete(history);
            await UnitOfWork.CommitAsync();

            return HttpResponse.OK(message: "Lịch sử thay đổi tiền đã được xóa thành công!", statusCode: System.Net.HttpStatusCode.OK);
        }
        public async Task<(List<HistoryMoneyResponse>?, int TotalRecords)> GetAllAsync(string search, int pageNumber, int pageSize)
        {
            var query = _historyMoney.All()
                .Include(d => d.User)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                query = query.Where(x => x.Title.ToLower().Contains(search) ||
                                         x.Description.ToLower().Contains(search) ||
                                         x.Money.ToString().Contains(search));
            }

            var TotalRecords = await query.CountAsync();

            if (pageSize != -1 && pageNumber != -1)
            {
                query = query.OrderByDescending(d => d.ModifiedDate)
                             .Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize);
            }

            var historySearch = await query.Select(x => new HistoryMoneyResponse
            {
                Id = x.Id,
                Money = x.Money,
                Title = x.Title,
                Description = x.Description,
                UserId = x.UserId,
            }).ToListAsync();

            return (historySearch, TotalRecords);
        }
        public async Task<(List<HistoryMoneyResponse>?, int TotalRecords)> GetMe(string search, int pageNumber, int pageSize)
        {
            var query = _historyMoney.All()
                .Where(d => d.UserId == userMeToken.Id)
                .Include(d => d.User)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Title.ToLower().Contains(search) ||
                                         x.Description.ToLower().Contains(search) ||
                                         x.Money.ToString().Contains(search));
            }

            var TotalRecords = await query.CountAsync();

            if (pageSize != -1 && pageNumber != -1)
            {
                query = query.OrderByDescending(d => d.ModifiedDate)
                             .Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize);
            }

            var historySearch = await query.Select(x => new HistoryMoneyResponse
            {
                Id = x.Id,
                Money = x.Money,
                Title = x.Title,
                Description = x.Description,
                UserId = x.UserId,
            }).ToListAsync();

            return (historySearch, TotalRecords);
        }
    }
}
