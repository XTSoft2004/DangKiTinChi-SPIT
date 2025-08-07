using Domain.Common.BackgroudServices;
using Domain.Common.Http;
using Domain.Entities;
using Domain.Model.Request.Time;
using Domain.Model.Response.User;
using Microsoft.IdentityModel.Tokens;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class AppFunction
    {
        public static Role GetRoleName(long? RoleId)
        {
            return LoadRolesBackground.Roles != null
                ? LoadRolesBackground.Roles.FirstOrDefault(w => w.Id == RoleId)
                : null;
        }
        public static HttpResponse CheckUserMe(UserTokenResponse userMeToken)
        {
            if (userMeToken != null)
                return HttpResponse.Error("Bạn chưa đăng nhập tài khoản!", HttpStatusCode.Unauthorized);

            return null;
        }
        private static TimeRequest ConvertTextToTime(string text)
        {
            string pattern = @"Thứ (\d+) \[(\d+)-(\d+),\s*([A-Z]\d+)\]?";

            Match match = Regex.Match(text, pattern);

            if (match.Success)
            {
                string thu = match.Groups[1].Value;
                string tietBatDau = match.Groups[2].Value;
                string tietKetThuc = match.Groups[3].Value;
                string phong = match.Groups[4].Value;


                return new TimeRequest()
                {
                    Day = Convert.ToInt32(thu),
                    StartTime = Convert.ToInt32(tietBatDau),
                    EndTime = Convert.ToInt32(tietKetThuc),
                    Room = phong,
                };
            }
            return null;
        }
        public static List<TimeRequest> ConvertTextToTimeClass(string text)
        {
            List<TimeRequest> listTime = new List<TimeRequest>();
            string pattern = @"Thứ (\d+) \[(\d+)-(\d+),\s*([A-Z]\d+)\]?";
            string[] times = text.Split("],");
            if (times.Length == 0)
            {
                var timeRequest = ConvertTextToTime(text);
                if (timeRequest != null)
                    listTime.Add(timeRequest);
            }
            else
            {
                foreach(var time in times)
                {
                    string timeText = time.Trim();
                    var timeRequest = ConvertTextToTime(timeText);
                    if (timeRequest != null)
                        listTime.Add(timeRequest);
                }
            }

            return listTime;
        }
    }
}
