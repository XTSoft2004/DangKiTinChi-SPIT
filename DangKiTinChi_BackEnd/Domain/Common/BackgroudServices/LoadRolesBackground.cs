using Domain.Entities;
using Domain.Interfaces.Database;
using Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.BackgroudServices
{
    public class LoadRolesBackground : BackgroundService
    {
        private readonly ILogger<LoadRolesBackground>? _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        private bool _isRunning = false;

        public LoadRolesBackground(
            ILogger<LoadRolesBackground>? logger,
            IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _time_Callback(null);
        }
        public static List<Role> Roles = new List<Role>();
        public async Task _time_Callback(object? state)
        {
            if (_isRunning) return;
            _isRunning = true;
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var roles = scope.ServiceProvider.GetRequiredService<IRepositoryBase<Role>>();
                Roles = roles.All().ToList();
                AppExtension.PrintWithRandomColor($"Đã lấy {Roles.Count} role thành công!");
            }
            catch (Exception ex)
            {
                AppExtension.PrintWithRandomColor($"Lỗi khi lấy roles: {ex.Message}"); // In ra thông báo lỗi với màu ngẫu nhiên
            }
            finally
            {
                _isRunning = false;
            }
        }
    }
}