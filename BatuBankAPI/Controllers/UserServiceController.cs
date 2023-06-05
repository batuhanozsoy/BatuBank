using BatuBankClassLibary.Controller;
using BatuBankClassLibary.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BatuBankAPI.Controllers
{
    public class UserServiceController
    {
        [HttpPost("AddNewUser")]
        public string AddNewUser([FromBody] List<ServiceParameterObject> _data)
        {
            try
            {
                var _user = JsonConvert.DeserializeObject<User>(_data.FirstOrDefault(x => x.Key == "_user").Value);

                if (_user != null)
                {
                    var userDb = UserController.Instance;
                    var user = userDb.AddNewUser(_user);

                    return JsonConvert.SerializeObject(new ApiResult()
                    {
                        Response = user != null ? JsonConvert.SerializeObject(user) : "",
                        Message = "Kullanıcı Başarıyla Eklendi.",
                        Status = true
                    });
                }

                return JsonConvert.SerializeObject(new ApiResult()
                {
                    Response = "",
                    Message = "Kullanıcı Ekleme İşlemi Başarısız.",
                    Status = false
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("UpdateUser")]
        public string UpdateUser([FromBody] List<ServiceParameterObject> _data)
        {
            try
            {
                var _user = JsonConvert.DeserializeObject<User>(_data.FirstOrDefault(x => x.Key == "_user").Value);

                if (_user != null)
                {
                    var userDb = UserController.Instance;
                    var user = userDb.Update(_user);

                    return JsonConvert.SerializeObject(new ApiResult()
                    {
                        Response = user != null ? JsonConvert.SerializeObject(user) : "",
                        Message = "Kullanıcı Bilgileri Başarıyla Güncellendi.",
                        Status = true
                    });
                }

                return JsonConvert.SerializeObject(new ApiResult()
                {
                    Response = "",
                    Message = "Kullanıcı Bilgileri Güncelleme İşlemi Başarısız.",
                    Status = false
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("GetUserById")]
        public string GetUserById([FromBody] List<ServiceParameterObject> _data)
        {
            try
            {

                    var _user = JsonConvert.DeserializeObject<User>(_data.FirstOrDefault(x => x.Key == "_user").Value);

                    if (_user != null) 
                    {
                        var userDb = UserController.Instance;
                        var user = userDb.Get<User>(_user.Id);

                        return JsonConvert.SerializeObject(new ApiResult()
                        {
                            Response = user != null ? JsonConvert.SerializeObject(user) : "",
                            Message = "Kullanıcı Başarıyla Bulundu.",
                            Status = true
                        });
                    }
                    
                return JsonConvert.SerializeObject(new ApiResult()
                {
                    Response = "",
                    Message = "Kullanıcı Bulunamadı.",
                    Status = false
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("DeleteUser")]
        public string DeleteUser([FromBody] List<ServiceParameterObject> _data)
        {
            try
            {
                var _user = JsonConvert.DeserializeObject<User>(_data.FirstOrDefault(x => x.Key == "_user").Value);
                if (_user != null)
                {
                    var userDb = UserController.Instance;
                    var user = userDb.Delete<User>(_user);

                    return JsonConvert.SerializeObject(new ApiResult()
                    {
                        Response = user != null ? JsonConvert.SerializeObject(user) : "",
                        Message = "Kullanıcı Başarıyla Silindi.",
                        Status = true
                    });
                }

                return JsonConvert.SerializeObject(new ApiResult()
                {
                    Response = "",
                    Message = "Kullanıcıyı Silme İşlemi Başarısız.",
                    Status = false
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("GetAllUsers")]
        public string GetAllUsers()
        {
            try
            {
                    var userDb = UserController.Instance;
                    var user = userDb.GetAllUsers();
                    if (user != null)
                    {
                        return JsonConvert.SerializeObject(new ApiResult()
                        {
                            Response = user != null ? JsonConvert.SerializeObject(user) : "",
                            Message = "Kullanıcı Başarıyla Silindi.",
                            Status = true
                        });
                    }


                return JsonConvert.SerializeObject(new ApiResult()
                {
                    Response = "",
                    Message = "Kullanıcıyı Silme İşlemi Başarısız.",
                    Status = false
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("UpdateUserPassword")]
        public string UpdateUserPassword([FromBody] List<ServiceParameterObject> _data)
        {
            try
            {
                var _userId = Convert.ToInt32(_data.FirstOrDefault(x => x.Key == "_userId").Value);
                var _newPassword = _data.FirstOrDefault(x => x.Key == "_newPassword").Value;
                var _oldPassword = _data.FirstOrDefault(x => x.Key == "_oldPassword").Value;


                if (_userId != null && _newPassword != null && _oldPassword != null)
                {
                    var userDb = UserController.Instance;
                    var user = userDb.UpdateUserPassword(_userId,_newPassword,_oldPassword);

                    return JsonConvert.SerializeObject(new ApiResult()
                    {
                        Response = user != null ? JsonConvert.SerializeObject(user) : "",
                        Message = "Başarıyla Güncellendi.",
                        Status = true
                    });
                }

                return JsonConvert.SerializeObject(new ApiResult()
                {
                    Response = "",
                    Message = "Güncelleme İşlemi Başarısız.",
                    Status = false
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("CheckUserLogin")]
        public string CheckUserLogin([FromBody] List<ServiceParameterObject> _data)
        {
            try
            {
                
                var _email = _data.FirstOrDefault(x => x.Key == "_newPassword").Value;
                var _password = _data.FirstOrDefault(x => x.Key == "_oldPassword").Value;


                if (_email != null && _password != null)
                {
                    var userDb = UserController.Instance;
                    var user = userDb.CheckUserLogin(_email, _password);

                    return JsonConvert.SerializeObject(new ApiResult()
                    {
                        Response = user != null ? JsonConvert.SerializeObject(user) : "",
                        Message = "Başarıyla giriş yapıldı.",
                        Status = true
                    });
                }

                return JsonConvert.SerializeObject(new ApiResult()
                {
                    Response = "",
                    Message = "Giriş İşlemi Başarısız.",
                    Status = false
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("ChangeUserRole")]
        public string ChangeUserRole([FromBody] List<ServiceParameterObject> _data)
        {
            try
            {

                var _email = _data.FirstOrDefault(x => x.Key == "_newPassword").Value;
                var _password = _data.FirstOrDefault(x => x.Key == "_oldPassword").Value;


                if (_email != null && _password != null)
                {
                    var userDb = UserController.Instance;
                    var user = userDb.CheckUserLogin(_email, _password);

                    return JsonConvert.SerializeObject(new ApiResult()
                    {
                        Response = user != null ? JsonConvert.SerializeObject(user) : "",
                        Message = "Başarıyla giriş yapıldı.",
                        Status = true
                    });
                }

                return JsonConvert.SerializeObject(new ApiResult()
                {
                    Response = "",
                    Message = "Giriş İşlemi Başarısız.",
                    Status = false
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
