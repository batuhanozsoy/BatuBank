using BatuBankClassLibary.Business;
using BatuBankClassLibary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatuBankClassLibary.Controller
{
    public class AccountTypeController : BaseBusiness<AccountTypeController>
    {
        public List<AccountType> GetAllAccountTypes()
        {
            try
            {
                using (var context = new BatuBankContext())
                {
                    return context.AccountTypes.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
