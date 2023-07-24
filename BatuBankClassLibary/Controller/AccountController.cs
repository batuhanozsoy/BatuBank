using BatuBankClassLibary.Business;
using BatuBankClassLibary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatuBankClassLibary.Controller
{
    public class AccountController : BaseBusiness<AccountController>
    {
        public List<Account> GetAllAccounts()
        {
            try
            {
                using (var context = new BatuBankContext())
                {
                    return context.Accounts.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
