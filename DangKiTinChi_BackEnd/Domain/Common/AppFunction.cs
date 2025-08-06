using Domain.Common.BackgroudServices;
using Domain.Common.Http;
using Domain.Entities;
using Domain.Model.Response.User;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
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
    }
}
