using BatuBankClassLibary.Business;
using BatuBankClassLibary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatuBankClassLibary.Controller
{
    public class RoleController : BaseBusiness<RoleController>
    {
        public List<Role> GetAllRoles()
        {
            try
            {
                using (var context = new BatuBankContext())
                {
                    return context.Roles.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
