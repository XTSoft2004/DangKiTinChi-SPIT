using Domain.Common.BackgroudServices;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
