using BatuBankClassLibary.Business;
using BatuBankClassLibary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatuBankClassLibary.Controller
{
    public class TransactionTypeController : BaseBusiness<TransactionTypeController>
    {
        public List<TransactionType> GetAllTransactionTypes()
        {
            try
            {
                using (var context = new BatuBankContext())
                {
                    return context.TransactionTypes.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
