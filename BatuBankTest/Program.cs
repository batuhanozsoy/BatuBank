
using BatuBankClassLibary;
using BatuBankClassLibary.Controller;
using BatuBankClassLibary.Entities;
using MimeKit;
using MailKit.Net.Smtp;

class Program
{
    static public void Main(String[] args)
    {
        var userInstance = UserController.Instance;
        //var userCreditCardInstance = UserCreditCardController.Instance;
        //var userAccountInstance = UserAccountController.Instance;

        //User user = new User();
        //UserCreditCard userCreditCard = new UserCreditCard();
        //UserAccount userAccount = new UserAccount();

        //user.Name = "Test";
        //user.Surname = "Test";
        //user.Email = "Test@gmail.com";
        //user.Password = "Test";

        //userAccountInstance.SellProductFromUserAccount(17, "9yibyGzmb4SFemvDhIW8HoScdt", 1 , 1000);

        userInstance.CheckUserLogin("batuhanozsoy@gmail.com", "sajkldnajksdas");



    }
}