using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatuBankClassLibary.Entities
{
    public partial class UserLoginObserver
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int WrongLoginAttempt { get; set; }

        public DateTime WrongLoginDateTime { get; set; }
    }
}
