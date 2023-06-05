using BatuBankClassLibary.Controller;
using BatuBankClassLibary.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BatuBankAPI.Controllers
{
    public class UserCreditCardServiceController
    {
        [HttpPost("AddNewUserCreditCard")]
        public string AddNewUserCreditCard([FromBody] List<ServiceParameterObject> _data)
        {
            try
            {
                var _userId = Convert.ToInt32(_data.FirstOrDefault(x => x.Key == "_userId").Value);
                var _creditCardTypeId = Convert.ToInt32(_data.FirstOrDefault(x => x.Key == "_creditCardTypeId").Value);

                if (_creditCardTypeId > 0 && _userId > 0)
                {
                    var userCreditCardDb = UserCreditCardController.Instance;
                    var userCreditCard = userCreditCardDb.AddNewUserCreditCart(_userId, _creditCardTypeId);

                    return JsonConvert.SerializeObject(new ApiResult()
                    {
                        Response = userCreditCard != null ? JsonConvert.SerializeObject(userCreditCard) : "",
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

       

        

        [HttpPost("DeleteUserCreditCart")]
        public string DeleteUserCreditCart([FromBody] List<ServiceParameterObject> _data)
        {
            try
            {
                var _userId = Convert.ToInt32(_data.FirstOrDefault(x => x.Key == "_userId").Value);
                var _creditCardTypeId = Convert.ToInt32(_data.FirstOrDefault(x => x.Key == "_creditCardTypeId").Value);

                if (_userId > 0 && _creditCardTypeId > 0)
                {
                    var userCreditCardDb = UserCreditCardController.Instance;
                    var userCreditCard = userCreditCardDb.DeleteUserCreditCart(_userId, _creditCardTypeId);

                    return JsonConvert.SerializeObject(new ApiResult()
                    {
                        Response = "",
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

        [HttpPost("BlockUserCreditCard")]
        public string BlockUserCreditCard([FromBody] List<ServiceParameterObject> _data)
        {
            try
            {
                var _userId = Convert.ToInt32(_data.FirstOrDefault(x => x.Key == "_userId").Value);
                var _creditCardTypeId = Convert.ToInt32(_data.FirstOrDefault(x => x.Key == "_creditCardTypeId").Value);

                if (_userId > 0 && _creditCardTypeId > 0)
                {
                    var userCreditCardDb = UserCreditCardController.Instance;
                    var userCreditCard = userCreditCardDb.BlockUserCreditCard(_userId, _creditCardTypeId);

                    return JsonConvert.SerializeObject(new ApiResult()
                    {
                        Response = "",
                        Message = "Kullanıcı Başarıyla Bloklandı.",
                        Status = true
                    });
                }

                return JsonConvert.SerializeObject(new ApiResult()
                {
                    Response = "",
                    Message = "Kullanıcıyı Bloklama İşlemi Başarısız.",
                    Status = false
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        
        [HttpPost("GetUserCreditCards")]
        public string GetUserCreditCards([FromBody] List<ServiceParameterObject> _data)
        {
            try
            {
                var _userId = Convert.ToInt32(_data.FirstOrDefault(x => x.Key == "_userId").Value);

                if (_userId > 0 )
                {
                    var userCreditCardDb = UserCreditCardController.Instance;
                    var userCreditCard = userCreditCardDb.GetUserCreditCards(_userId);

                    return JsonConvert.SerializeObject(new ApiResult()
                    {
                        Response = "",
                        Message = "Kullanıcı Başarıyla Bulundu.",
                        Status = true
                    });
                }

                return JsonConvert.SerializeObject(new ApiResult()
                {
                    Response = "",
                    Message = "Kullanıcıyı Bulma İşlemi Başarısız.",
                    Status = false
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("GetAllUsersCreditCards")]
        public string GetAllUsersCreditCards()
        {
            try
            {
                var userCreditCardDb = UserCreditCardController.Instance;
                var userCreditCard = userCreditCardDb.GetAllUsersCreditCards();
                if (userCreditCard != null)
                {
                    return JsonConvert.SerializeObject(new ApiResult()
                    {
                        Response = userCreditCard != null ? JsonConvert.SerializeObject(userCreditCard) : "",
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
    }
}
