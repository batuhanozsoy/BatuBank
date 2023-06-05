using BatuBankClassLibary.Controller;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BatuBankAPI.Controllers
{
    public class UserAccountServiceController
    {
        [HttpPost("AddNewUserAccount")]
        public string AddNewUserAccount([FromBody] List<ServiceParameterObject> _data)
        {
            try
            {
                var _userId = Convert.ToInt32(_data.FirstOrDefault(x => x.Key == "_userId").Value);
                var _accountTypeId = Convert.ToInt32(_data.FirstOrDefault(x => x.Key == "_accountTypeId").Value);

                if (_accountTypeId > 0 && _userId > 0)
                {
                    var userAccountDb = UserAccountController.Instance;
                    var userAccount = userAccountDb.AddNewUserAccount(_userId, _accountTypeId);

                    return JsonConvert.SerializeObject(new ApiResult()
                    {
                        Response = userAccount != null ? JsonConvert.SerializeObject(userAccount) : "",
                        Message = "Başarıyla Eklendi.",
                        Status = true
                    });
                }

                return JsonConvert.SerializeObject(new ApiResult()
                {
                    Response = "",
                    Message = "Ekleme İşlemi Başarısız.",
                    Status = false
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("DeleteUserAccount")]
        public string DeleteUserAccount([FromBody] List<ServiceParameterObject> _data)
        {
            try
            {
                var _userId = Convert.ToInt32(_data.FirstOrDefault(x => x.Key == "_userId").Value);
                var _accountIban = _data.FirstOrDefault(x => x.Key == "_accountIban").Value;

                if (_accountIban != null && _userId > 0)
                {
                    var userAccountDb = UserAccountController.Instance;
                    var userAccount = userAccountDb.DeleteUserAccount(_userId, _accountIban);

                    return JsonConvert.SerializeObject(new ApiResult()
                    {
                        Response = userAccount != null ? JsonConvert.SerializeObject(userAccount) : "",
                        Message = "Başarıyla Silindi.",
                        Status = true
                    });
                }

                return JsonConvert.SerializeObject(new ApiResult()
                {
                    Response = "",
                    Message = "Silme İşlemi Başarısız.",
                    Status = false
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("BuyProductFromUserAccount")]
        public string BuyProductFromUserAccount([FromBody] List<ServiceParameterObject> _data)
        {
            try
            {
                var _userId = Convert.ToInt32(_data.FirstOrDefault(x => x.Key == "_userId").Value);
                var _accountIban = _data.FirstOrDefault(x => x.Key == "_accountIban").Value;
                var _productId = Convert.ToInt32(_data.FirstOrDefault(x => x.Key == "_productId").Value);
                var _numberOfProducts = Convert.ToInt32(_data.FirstOrDefault(x => x.Key == "_numberOfProducts").Value);

                if (_accountIban != null && _userId > 0 && _productId > 0 && _numberOfProducts > 0)
                {
                    var userAccountDb = UserAccountController.Instance;
                    var userAccount = userAccountDb.BuyProductFromUserAccount(_userId, _accountIban , _productId , _numberOfProducts);

                    return JsonConvert.SerializeObject(new ApiResult()
                    {
                        Response = userAccount != null ? JsonConvert.SerializeObject(userAccount) : "",
                        Message = "Başarıyla Satın Alındı.",
                        Status = true
                    });
                }

                return JsonConvert.SerializeObject(new ApiResult()
                {
                    Response = "",
                    Message = "Satın Alma İşlemi Başarısız.",
                    Status = false
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("SellProductFromUserAccount")]
        public string SellProductFromUserAccount([FromBody] List<ServiceParameterObject> _data)
        {
            try
            {
                var _userId = Convert.ToInt32(_data.FirstOrDefault(x => x.Key == "_userId").Value);
                var _accountIban = _data.FirstOrDefault(x => x.Key == "_accountIban").Value;
                var _productId = Convert.ToInt32(_data.FirstOrDefault(x => x.Key == "_productId").Value);
                var _numberOfProducts = Convert.ToInt32(_data.FirstOrDefault(x => x.Key == "_numberOfProducts").Value);

                if (_accountIban != null && _userId > 0 && _productId > 0 && _numberOfProducts > 0)
                {
                    var userAccountDb = UserAccountController.Instance;
                    var userAccount = userAccountDb.SellProductFromUserAccount(_userId, _accountIban, _productId, _numberOfProducts);

                    return JsonConvert.SerializeObject(new ApiResult()
                    {
                        Response = userAccount != null ? JsonConvert.SerializeObject(userAccount) : "",
                        Message = "Başarıyla Satış Yapıldı.",
                        Status = true
                    });
                }

                return JsonConvert.SerializeObject(new ApiResult()
                {
                    Response = "",
                    Message = "Satış Yapma İşlemi Başarısız.",
                    Status = false
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("GetUserAccounts")]
        public string GetUserAccounts([FromBody] List<ServiceParameterObject> _data)
        {
            try
            {
                var _userId = Convert.ToInt32(_data.FirstOrDefault(x => x.Key == "_userId").Value);

                if (_userId > 0)
                {
                    var userAccountDb = UserAccountController.Instance;
                    var userAccount = userAccountDb.GetUserAccounts(_userId);

                    return JsonConvert.SerializeObject(new ApiResult()
                    {
                        Response = userAccount != null ? JsonConvert.SerializeObject(userAccount) : "",
                        Message = "Kullanıcının Hesapları Gösteriliyor..",
                        Status = true
                    });
                }

                return JsonConvert.SerializeObject(new ApiResult()
                {
                    Response = "",
                    Message = "Kullanıcının Hesaplarını Gösterme İşlemi Başarısız.",
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
