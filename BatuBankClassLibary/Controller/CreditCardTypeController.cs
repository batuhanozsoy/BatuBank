using BatuBankClassLibary.Business;
using BatuBankClassLibary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatuBankClassLibary.Controller
{
    public class CreditCardTypeController : BaseBusiness<CreditCardTypeController>
    {
        public List<CreditCardType> GetAllUsers()
        {
            try
            {
                using (var context = new BatuBankContext())
                {
                    return context.CreditCardTypes.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
