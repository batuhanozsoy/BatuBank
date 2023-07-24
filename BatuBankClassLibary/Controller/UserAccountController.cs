using BatuBankClassLibary.Business;
using BatuBankClassLibary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;


namespace BatuBankClassLibary.Controller
{
    public class UserAccountController : BaseBusiness<UserAccountController>
    {
        public List<UserAccount> GetAllUserAccounts() //Bütün kullanıcıların hesaplarının getirildiği fonksiyon.
        {
            try
            {
                using (var context = new BatuBankContext())
                {
                    return context.UserAccounts.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public UserAccount AddNewUserAccount(int _userId, int _accountTypeId) //Yeni hesap açıp kullanıcıya atayan fonksiyon.
        {
            try
            {
                using (var context = new BatuBankContext())
                {
                    var findedUser = context.Set<User>().FirstOrDefault(x => x.Id == _userId);

                    if (findedUser != null)
                    {
                        Account _account = new Account();
                        UserAccount _userAccount = new UserAccount();


                        _account.AccountName = findedUser.Name + " " + findedUser.Surname + " " + "Hesabı";
                        _account.AccountTypeId = _accountTypeId;
                        _account.AccountBalance = 0;
                        _account.AccountCreationTime = DateTime.UtcNow;
                        _account.İsBlocked = false;
                        _account.Iban = CreateIban(26);
                        context.Add(_account);
                        context.SaveChanges();
                        
                        _userAccount.UserId = _userId;
                        _userAccount.AccountId = _account.Id;
                        context.Add(_userAccount);
                        context.SaveChanges();

                        return _userAccount;
                        

                        
                    }




                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public UserAccount DeleteUserAccount(int _userId, string _accountIban) // Kullanıcının hesabını sildiği fonksiyon.
        {
            try
            {
                using (var context = new BatuBankContext())
                {
                    var findedAccount = context.Set<Account>().FirstOrDefault(x => x.Iban == _accountIban);

                    if (findedAccount != null)
                    {
                        var findedUserAccount = context.Set<UserAccount>().FirstOrDefault(x => x.UserId == _userId && x.AccountId == findedAccount.Id);

                        if (findedUserAccount != null && findedAccount.AccountBalance <= 0) //Kullanıcının hesabında para varsa silinmeyecek.
                        {
                            context.Remove(findedAccount);
                            context.Remove(findedUserAccount);
                            context.SaveChanges();

                            return findedUserAccount;
                        }
                        else
                        {
                            Console.WriteLine("Hesabınızdaki bakiyeyi çekiniz ya da aktarınız.");
                        }
                    }
                    

                    
                }

                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public UserAccount BuyProductFromUserAccount (int _userId, string _accountIban, int _productId, decimal _numberOfProducts) // Kullanıcının dolar,euro ya da altın aldığı fonksiyon
        {
            try
            {
                using (var context = new BatuBankContext())
                {
                    var findedAccount = context.Set<Account>().FirstOrDefault(x => x.Iban == _accountIban);

                    if (findedAccount != null)
                    {
                        var findedUserAccount = context.Set<UserAccount>().FirstOrDefault(x => x.UserId == _userId && x.AccountId == findedAccount.Id);
                        var findedProduct = context.Set<Product>().FirstOrDefault(x => x.Id == _productId);

                        if (findedUserAccount != null && findedProduct != null)
                        {

                            decimal _findedAccountBalance = findedAccount.AccountBalance;


                            if (_findedAccountBalance > findedProduct.ProductSellPrice * _numberOfProducts) 
                            {
                                var _lastAccountBalance = _findedAccountBalance - (decimal)findedProduct.ProductSellPrice * _numberOfProducts;

                                var findedUserProduct = context.Set<UserProduct>().FirstOrDefault(x => x.UserId == _userId);

                                if (findedUserProduct != null)
                                {
                                    findedAccount.AccountBalance = _lastAccountBalance;

                                    UserTransaction userTransaction = new UserTransaction();
                                    UserAccountObserver userAccountObserver = new UserAccountObserver();

                                    userTransaction.UserId = _userId;
                                    userTransaction.TransactionTypeId = 1;
                                    userTransaction.Report = findedProduct.ProductName + " " + "alış işlemi gerçekleşmiştir." + findedProduct.ProductSellPrice + " " + "kurundan alınmıştır.";

                                    userAccountObserver.UserId = _userId;
                                    userAccountObserver.AccountId = findedAccount.Id;
                                    userAccountObserver.ActionTakenDateTime = DateTime.UtcNow;
                                    userAccountObserver.OldAccountBalance = _findedAccountBalance;
                                    userAccountObserver.NewAccountBalance = _lastAccountBalance;
                                    userAccountObserver.IsRisky = false;
                                    userAccountObserver.Report = findedProduct.ProductName + " " + "alış işlemi gerçekleşmiştir." + findedProduct.ProductSellPrice + " " + "kurundan alınmıştır." + "Kullanılan hesap = " + findedAccount.Iban;

                                    var findedUserProductValue = findedUserProduct.ProductValue;

                                    findedUserProduct.ProductId = findedProduct.Id;
                                    findedUserProduct.UserId = _userId;
                                    findedUserProduct.ProductValue = findedUserProductValue + _numberOfProducts;


                                    context.Add(userAccountObserver);
                                    context.Add(userTransaction);
                                    context.Update(findedUserProduct);
                                    context.Update(findedAccount);

                                    context.SaveChanges();
                                    return findedUserAccount;
                                }
                                else if (findedUserProduct == null)
                                {
                                    findedAccount.AccountBalance = _lastAccountBalance;

                                    UserTransaction userTransaction = new UserTransaction();
                                    UserAccountObserver userAccountObserver = new UserAccountObserver();
                                    UserProduct userProduct = new UserProduct();

                                    userTransaction.UserId = _userId;
                                    userTransaction.TransactionTypeId = 1;
                                    userTransaction.Report = findedProduct.ProductName + " " + "alış işlemi gerçekleşmiştir." + findedProduct.ProductSellPrice + " " + "kurundan alınmıştır.";

                                    userAccountObserver.UserId = _userId;
                                    userAccountObserver.AccountId = findedAccount.Id;
                                    userAccountObserver.ActionTakenDateTime = DateTime.UtcNow;
                                    userAccountObserver.OldAccountBalance = _findedAccountBalance;
                                    userAccountObserver.NewAccountBalance = _lastAccountBalance;
                                    userAccountObserver.IsRisky = false;
                                    userAccountObserver.Report = findedProduct.ProductName + " " + "alış işlemi gerçekleşmiştir." + findedProduct.ProductSellPrice + " " + "kurundan alınmıştır." + "Kullanılan hesap = " + findedAccount.Iban;

                                    userProduct.ProductId = findedProduct.Id;
                                    userProduct.UserId = _userId;
                                    userProduct.ProductValue = _numberOfProducts;

                                    context.Add(userAccountObserver);
                                    context.Add(userTransaction);
                                    context.Add(userProduct);
                                    context.Update(findedAccount);

                                    context.SaveChanges();
                                    return findedUserAccount;
                                }
                                
                            }
                            else
                            {
                                Console.WriteLine("Hata!. Bakiye Yetersiz.");
                            }

                            
                        }
                    }
                }

                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public UserAccount SellProductFromUserAccount(int _userId, string _accountIban, int _productId, decimal _numberOfProducts) // kullanıcının döviz satışı yaptığı ve bunların kaydını tutan fonksiyon.
        {
            try
            {
                using (var context = new BatuBankContext())
                {
                    var findedAccount = context.Set<Account>().FirstOrDefault(x => x.Iban == _accountIban);

                    if (findedAccount != null)
                    {
                        var findedUserAccount = context.Set<UserAccount>().FirstOrDefault(x => x.UserId == _userId && x.AccountId == findedAccount.Id);
                        var findedProduct = context.Set<Product>().FirstOrDefault(x => x.Id == _productId);

                        if (findedUserAccount != null && findedProduct != null)
                        {

                            decimal _findedAccountBalance = findedAccount.AccountBalance;

                            if (_findedAccountBalance > findedProduct.ProductBuyPrice * _numberOfProducts)
                            {

                                var _lastAccountBalance = _findedAccountBalance + (decimal)findedProduct.ProductBuyPrice * _numberOfProducts;

                                var findedUserProduct = context.Set<UserProduct>().FirstOrDefault(x => x.UserId == _userId);

                                if (findedUserProduct != null)
                                {

                                    findedAccount.AccountBalance = _lastAccountBalance;
                                    var lastFindedUserProductValue = findedUserProduct.ProductValue - _numberOfProducts;

                                    findedUserProduct.ProductValue = lastFindedUserProductValue;
                                    findedUserProduct.ProductId = _productId;
                                    findedUserProduct.UserId = _userId;

                                    UserTransaction userTransaction = new UserTransaction();
                                    UserAccountObserver userAccountObserver = new UserAccountObserver();

                                    userTransaction.UserId = _userId;
                                    userTransaction.TransactionTypeId = 1;
                                    userTransaction.Report = findedProduct.ProductName + " " + "satış işlemi gerçekleşmiştir." + findedProduct.ProductBuyPrice + " " + "kurundan satılmıştır.";

                                    userTransaction.NetIncome = _lastAccountBalance -= _findedAccountBalance;

                                    if (userTransaction.NetIncome > 1000000)
                                    {
                                        userAccountObserver.UserId = _userId;
                                        userAccountObserver.AccountId = findedAccount.Id;
                                        userAccountObserver.ActionTakenDateTime = DateTime.UtcNow;
                                        userAccountObserver.OldAccountBalance = _findedAccountBalance;
                                        userAccountObserver.NewAccountBalance = _lastAccountBalance;
                                        userAccountObserver.IsRisky = true;
                                        userAccountObserver.Report = userTransaction.NetIncome + " " + "TL'lik " + findedProduct.ProductName + " " + "alınmıştır.";
                                    }
                                    else
                                    {
                                        userAccountObserver.UserId = _userId;
                                        userAccountObserver.AccountId = findedAccount.Id;
                                        userAccountObserver.ActionTakenDateTime = DateTime.UtcNow;
                                        userAccountObserver.OldAccountBalance = _findedAccountBalance;
                                        userAccountObserver.NewAccountBalance = _lastAccountBalance;
                                        userAccountObserver.IsRisky = false;
                                        userAccountObserver.Report = userTransaction.NetIncome + " " + "TL'lik" + findedProduct.ProductName + " " + "alınmıştır.";
                                    }

                                    context.Add(userTransaction);
                                    context.Add(userAccountObserver);
                                    
                                    context.Update(findedUserProduct);
                                    context.Update(findedAccount);
                                    context.SaveChanges();
                                    return findedUserAccount;
                                }
                               
                            }


                        }
                    }
                }

                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<UserAccount> GetUserAccounts(int _userId) // Kullanıcının hesaplarını getiren fonksiyon
        {
            try
            {
                using (var context = new BatuBankContext())
                {
                    var findedUser = context.Set<User>().FirstOrDefault(x => x.Id == _userId);
                    if (findedUser != null)
                    {
                        return context.UserAccounts.Where(x => x.UserId == findedUser.Id).OrderBy(x => x.AccountId).ToList();
                    }

                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string CreateIban(int _length) // IBAN oluşturan fonksiyon.
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder sb = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < _length; i++)
            {
                int index = random.Next(chars.Length);
                sb.Append(chars[index]);
            }

            return sb.ToString();
        }
    }
}
