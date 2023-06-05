using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatuBankClassLibary.Business
{
    public interface IDAO
    {
        T Get<T>(int _id) where T : class;
        T Add<T>(T newObject) where T : class;
        T Update<T>(T findedObject) where T : class;
        bool Delete<T>(T deletedObject) where T : class;
    }
}
