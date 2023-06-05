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
    public class UserCreditCardController : BaseBusiness<UserCreditCardController>
    {
       

        public List<UserCreditCard> GetAllUsersCreditCards()//Bütün kullanıcıların kredi kartlarının getirildiği fonksiyon.
        {
            try
            {
                using (var context = new BatuBankContext())
                {
                    return context.UserCreditCards.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public UserCreditCard AddNewUserCreditCart(int _userId, int _creditCardTypeId) //Kullanıcıya kredi kartı atanması ve oluşturulmasını sağlayan fonksiyon.
        {
            try
            {
                using (var context = new BatuBankContext())
                {
                    var findedUser = context.Set<User>().FirstOrDefault(x => x.Id == _userId);
                     
                    if (findedUser != null)
                    {
                        Random Random = new Random();
                        

                        if (_creditCardTypeId == 3)
                        {
                            //Öğrenci kartı
                            CreditCard _creditCard = new CreditCard();
                            UserCreditCard _userCreditCard = new UserCreditCard();


                            var randomCreditCardNumber = GenerateRandomCreditCardNumber();
                            _creditCard.CreditCardNumber = (randomCreditCardNumber);
                            _creditCard.Cvv = Random.Next(100, 999);
                            _creditCard.ExpireDate = DateTime.UtcNow.AddYears(5);
                            _creditCard.PaymentDueDate = DateTime.UtcNow.AddDays(30);
                            _creditCard.CreditCardTypeId = _creditCardTypeId;
                            _creditCard.IsContactless = true;
                            _creditCard.CreditCardPassword = Random.Next(1000, 9999);
                            _creditCard.CreditCardLimit = 1000;
                            _creditCard.CreditCardBalance = 0;
                            _creditCard.IsBlocked = false;

                            context.Add(_creditCard);
                            context.SaveChanges();

                            _userCreditCard.CreditCardId = _creditCard.Id;
                            _userCreditCard.UserId = _userId;

                            context.Add(_userCreditCard);
                            context.SaveChanges();

                            return _userCreditCard;

                        }
                        else if (_creditCardTypeId == 1)
                        {
                            //Kredi Kartı

                            CreditCard _creditCard = new CreditCard();
                            UserCreditCard _userCreditCard = new UserCreditCard();

                            var randomCreditCardNumber = GenerateRandomCreditCardNumber();
                            _creditCard.CreditCardNumber = (randomCreditCardNumber);
                            _creditCard.Cvv = Random.Next(100, 999);
                            _creditCard.ExpireDate = DateTime.UtcNow.AddYears(5);
                            _creditCard.PaymentDueDate = DateTime.UtcNow.AddDays(30);
                            _creditCard.CreditCardTypeId = _creditCardTypeId;
                            _creditCard.IsContactless = true;
                            _creditCard.CreditCardPassword = Random.Next(1000, 9999);
                            _creditCard.CreditCardLimit = 10000;
                            _creditCard.CreditCardBalance = 0;
                            _creditCard.IsBlocked = false;


                            context.Add(_creditCard);
                            context.SaveChanges();

                            _userCreditCard.CreditCardId = _creditCard.Id;
                            _userCreditCard.UserId = _userId;

                            context.Add(_userCreditCard);
                            context.SaveChanges();

                            return _userCreditCard;

                        }
                        else if (_creditCardTypeId == 2)
                        {
                            //Banka Kartı

                            CreditCard _creditCard = new CreditCard();
                            UserCreditCard _userCreditCard = new UserCreditCard();

                            var randomCreditCardNumber = GenerateRandomCreditCardNumber();
                            _creditCard.CreditCardNumber = (randomCreditCardNumber);
                            _creditCard.Cvv = Random.Next(100, 999);
                            _creditCard.ExpireDate = DateTime.UtcNow.AddYears(5);
                            _creditCard.CreditCardTypeId = _creditCardTypeId;
                            _creditCard.IsContactless = false;
                            _creditCard.CreditCardPassword = Random.Next(1000, 9999);
                            _creditCard.CreditCardBalance = 0;
                            _creditCard.IsBlocked = false;


                            context.Add(_creditCard);
                            context.SaveChanges();

                            _userCreditCard.CreditCardId = _creditCard.Id;
                            _userCreditCard.UserId = _userId;

                            context.Add(_userCreditCard);
                            context.SaveChanges();

                            return _userCreditCard;
                        }
                        else
                        {
                            Console.WriteLine("Kart oluşturma başarısız.");
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


        public UserCreditCard DeleteUserCreditCart(int _userId , int _creditCardId) // Kullanıcının kredi kartını silen fonksiyon.
        {
            try
            {
                using (var context = new BatuBankContext())
                {
                    var findedUser = context.Set<User>().FirstOrDefault(x => x.Id == _userId);
                    var findedCreditCart = context.Set<CreditCard>().FirstOrDefault(x => x.Id == _creditCardId);

                    if (findedUser != null && findedCreditCart != null)
                    {
                        var findedUserCreditCart = context.Set<UserCreditCard>().FirstOrDefault(x=> x.UserId == _userId && x.CreditCardId == _creditCardId);
                        
                        if (findedUserCreditCart != null)
                        {
                            context.Remove(findedCreditCart);
                            context.Remove(findedUserCreditCart);
                            context.SaveChanges();

                            return findedUserCreditCart;
                        }
                        
                    }

                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public UserCreditCard BlockUserCreditCard(int _userId , int _creditCardId) // Kullanıcının kartını bloklayan fonksiyon.
        {
            try
            {
                using (var context = new BatuBankContext())
                {
                    var findedUser = context.Set<User>().FirstOrDefault(x => x.Id == _userId);
                    var findedCreditCart = context.Set<CreditCard>().FirstOrDefault(x => x.Id == _creditCardId);


                    if (findedUser != null && findedCreditCart != null)
                    {

                        var findedUserCreditCart = context.Set<UserCreditCard>().FirstOrDefault(x => x.UserId == _creditCardId && x.CreditCardId == _creditCardId);

                        if (findedUserCreditCart != null)
                        {
                            if (findedCreditCart.PaymentDueDate > DateTime.UtcNow.AddDays(30) && findedCreditCart.CreditCardBalance > 0)
                            {
                                findedCreditCart.IsBlocked = true;
                                context.Update(findedUserCreditCart);
                                context.SaveChanges();
                                return findedUserCreditCart;
                            }

                            
                        }

                    }

                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<UserCreditCard> GetUserCreditCards (int _userId) // Kullanıcının bütün kredi kartlarını getiren fonksiyon.
        {
            try
            {
                using (var context = new BatuBankContext())
                {
                    var findedUser = context.Set<User>().FirstOrDefault(x => x.Id == _userId);
                    if (findedUser != null)
                    {
                        return context.UserCreditCards.Where(x => x.UserId == findedUser.Id).OrderBy(x => x.CreditCardId).ToList();
                    }

                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }



        public string GenerateRandomCreditCardNumber()
        {
            Random random = new Random();
            string bin = "100"; 
            string accountNumber = random.Next(0, 999999999).ToString("D9");
            string partialNumber = bin + accountNumber;
            string checksum = CalculateLuhnChecksum(partialNumber);
            string creditCardNumber = partialNumber + checksum;
            return creditCardNumber;
        }

        public string CalculateLuhnChecksum(string number)
        {
            int sum = 0;
            bool isOdd = true;
            for (int i = number.Length - 1; i >= 0; i--)
            {
                int digit = int.Parse(number[i].ToString());
                if (isOdd)
                {
                    digit *= 2;
                    if (digit > 9)
                    {
                        digit -= 9;
                    }
                }
                sum += digit;
                isOdd = !isOdd;
            }
            int checksum = (sum * 9) % 10;
            return checksum.ToString();
        }
    }
}
