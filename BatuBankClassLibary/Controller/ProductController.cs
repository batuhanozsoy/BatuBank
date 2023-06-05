using BatuBankClassLibary.Business;
using BatuBankClassLibary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatuBankClassLibary.Controller
{
    public class ProductController : BaseBusiness<ProductController>
    {
        public List<Product> GetAllUsers()
        {
            try
            {
                using (var context = new BatuBankContext())
                {
                    return context.Products.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
