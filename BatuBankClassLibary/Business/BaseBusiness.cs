using BatuBankClassLibary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatuBankClassLibary.Business
{
    public class BaseBusiness<T> : IDAO where T : BaseBusiness<T>, new()
    {
        private static T _instance = new T();
        public static T Instance { get { return _instance; } }

        public T Add<T>(T newItem) where T : class
        {
            try
            {
                using (var context = new BatuBankContext())
                {
                    context.Add(newItem);
                    context.SaveChanges();
                    return newItem;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Delete<T>(T deletedItem) where T : class
        {
            try
            {
                using (var context = new BatuBankContext())
                {
                    context.Remove<T>(deletedItem);
                    var numberOfDeleted = context.SaveChanges();
                    return numberOfDeleted > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public T Update<T>(T newItem) where T : class
        {
            try
            {
                using (var context = new BatuBankContext())
                {
                    context.Update<T>(newItem);
                    context.SaveChanges();
                    return newItem;
                }
                  
            }
            catch (Exception)
            {

                throw;
            }
        }

        public T Get<T>(int _id) where T : class
        {
            try
            {
                using (var context = new BatuBankContext())
                {
                    return context.Set<T>().Find(_id);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
