using BatuBankClassLibary.Business;
using BatuBankClassLibary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatuBankClassLibary.Controller
{
    public class UserRoleController : BaseBusiness<UserRoleController>
    {
        public List<UserRole> GetAllUsers()
        {
            try
            {
                using (var context = new BatuBankContext())
                {
                    return context.UserRoles.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
