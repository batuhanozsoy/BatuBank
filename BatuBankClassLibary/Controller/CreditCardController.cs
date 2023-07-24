using BatuBankClassLibary.Business;
using BatuBankClassLibary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatuBankClassLibary.Controller
{
    public class CreditCardController : BaseBusiness<CreditCardController>
    {
        public List<CreditCard> GetAllCreditCards()
        {
            try
            {
                using (var context = new BatuBankContext())
                {
                    return context.CreditCards.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
