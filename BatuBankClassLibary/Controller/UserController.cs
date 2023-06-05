using BatuBankClassLibary.Business;
using BatuBankClassLibary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BatuBankClassLibary.Controller
{
    public class UserController : BaseBusiness<UserController>
    {
        public List<User> GetAllUsers() //Bütün kullanıcıların getirildiği fonksiyon.
        {
			try
			{
                using (var context = new BatuBankContext())
				{
					return context.Users.ToList();
				}
			}
			catch (Exception)
			{

				throw;
			}
        }

		public User AddNewUser(User _user) //Yeni kullanıcının eklendiği fonksiyon.
		{
			try
			{
				using (var context = new BatuBankContext())
				{
					var userPassword = _user.Password;
					var hashedPassword = PasswordHasher.HashPassword(userPassword);
					_user.Password = hashedPassword;
					_user.IsBlocked = false;
					
					UserRole _userRole = new UserRole();

					_userRole.RoleId = 1;  // Herkes başta kullanıcı olarak atanacak. Admin kullanıcıları bankaya gidip özel istekle açılacaktır.

					context.Users.Add(_user);
                    context.SaveChanges();

                    _userRole.UserId = _user.Id;
                    context.UserRoles.Add(_userRole);
					context.SaveChanges();
					return _user;
					
					
				}
			}
			catch (Exception)
			{

				throw;
			}
		}

		public User UpdateUserPassword(int _userId, string _newPassword, string _oldPassword) //Kullanıcının şifresini değiştirdiği fonksiyon.
		{
			try
			{
				using (var context = new BatuBankContext())
				{
					var findedUser = context.Set<User>().FirstOrDefault(x => x.Id == _userId);

					if (findedUser != null) 
					{
						if (PasswordHasher.VerifyPassword(_oldPassword, findedUser.Password)) //Kullanıcı şifresini değiştirirken yapılan eski şifre kontrolu
						{
							var hashedPassword = PasswordHasher.HashPassword(_newPassword);
                            findedUser.Password = hashedPassword;
                            context.Update(findedUser);
                            context.SaveChanges();
                            return findedUser;
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

		public User CheckUserLogin(string _email, string _password) // Kullanıcı girişinin kontrol edildiği ve 3'ten fazla hatalı giriş yaptığında hesabı bloke eden fonksiyon.
		{
			try
			{
				using (var context = new BatuBankContext())
				{
                    var findedUser = context.Set<User>().FirstOrDefault(x => x.Email == _email);
					if (findedUser == null)
					{
                        Console.WriteLine("Email Yanlış.");
						return null;
                    }
					if (findedUser != null) 
					{
						if (findedUser.Email == _email && PasswordHasher.VerifyPassword(_password, findedUser.Password))
						{
							CheckUserRole(findedUser.Id);
							return findedUser;
                        }
						else
						{
                            var findedUserLoginObserver = context.UserLoginObservers.Where(x=> x.UserId == findedUser.Id).FirstOrDefault();// en son yaptığı giriş datası dönecek.

							if (findedUserLoginObserver != null)
							{
								var _wrongLoginAttemtp = findedUserLoginObserver.WrongLoginAttempt++;
								findedUserLoginObserver.WrongLoginDateTime = DateTime.UtcNow;
								context.Update(findedUserLoginObserver);
								context.SaveChanges();

								if (_wrongLoginAttemtp == 3)
								{
									context.Remove(findedUserLoginObserver);
									findedUser.IsBlocked = true;
									context.Update(findedUser);
									context.SaveChanges();
								}
							}
							else if (findedUserLoginObserver == null)
							{
								UserLoginObserver observer = new UserLoginObserver();
								
								observer.WrongLoginAttempt = 1;
								observer.UserId = findedUser.Id;
								observer.WrongLoginDateTime = DateTime.UtcNow;
								context.Add<UserLoginObserver>(observer);
								context.SaveChanges();

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

		public UserRole CheckUserRole(int _userId) //Kullanıcının rolünün kontrol edildiği fonksiyon.
		{
			try
			{
				using (var context = new BatuBankContext())
				{
					var findedUser = context.Set<UserRole>().FirstOrDefault(x=> x.UserId == _userId);

					if (findedUser != null)
					{
						if (findedUser.RoleId == 1)
						{
                            Console.WriteLine("Admin Paneline Yönlendiriliyor");
							return findedUser;
                        }
						else if (findedUser.RoleId == 2)
						{
							Console.WriteLine("Kullanıcı Paneline Yönlendiriliyor");
							return findedUser;
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

		public User ChangeUserRole (int _userId , UserRole _userRole, int _roleId) //Kullanıcının rolünün değiştirildiği fonksiyon.
		{
			
			try
			{
				using (var context = new BatuBankContext())
				{
					var findedUser = context.Set<User>().FirstOrDefault(x => x.Id == _userId);

					if (findedUser != null) 
					{
						findedUser.Id = _userRole.Id;
						_userRole.RoleId = _roleId;
						context.Update(_userRole);
						context.SaveChanges();
						return findedUser;
					}

					return null;
				}
			}
			catch (Exception)
			{

				throw;
			}
		}

    }
}
