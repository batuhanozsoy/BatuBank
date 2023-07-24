using BatuBankClassLibary.Business;
using BatuBankClassLibary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatuBankClassLibary.Controller
{
    public class UserTransactionController : BaseBusiness<UserTransactionController>
    {
        public List<UserTransaction> GetAllUserTransaction()
        {
            try
            {
                using (var context = new BatuBankContext())
                {
                    return context.UserTransactions.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
