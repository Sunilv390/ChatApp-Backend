using CommonLayer.DBContext;
using CommonLayer.Models;
using CommonLayer.Models.RequestData;
using CommonLayer.Models.Response;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly ChatAppContext database;
        public UserRL(ChatAppContext _database)
        {
            database = _database;
        }

        public ResponseData SignUp(UserSignUp registration)
        {
            try
            {
                //It checks emailaddress should not exist
                var checkData = database.UserDetails.Where(exist => exist.EmailAddress == registration.EmailAddress).
                    FirstOrDefault();
                if (checkData == null)
                {
                    //Request Body
                    UserRegistration user = new UserRegistration()
                    {
                        FirstName = registration.FirstName,
                        LastName = registration.LastName,
                        EmailAddress = registration.EmailAddress,
                        Password = registration.Password,
                        RegisteredDate = DateTime.Now
                    };
                    database.Add(user);
                    database.SaveChanges();

                    //Response Body
                    ResponseData responseData = new ResponseData()
                    {
                        UserId = user.UserId,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        EmailAddress = user.EmailAddress,
                        RegisteredDate = user.RegisteredDate
                    };
                    return responseData;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ResponseData UserLogin(UserLogin login)
        {
            try
            {
                ResponseData userDetail = null;
                //match email and password from database for login
                var loginData = database.UserDetails.Where(user => user.EmailAddress == login.EmailAddress && user.Password == login.Password).
                   FirstOrDefault<UserRegistration>();

                if (loginData != null)
                {
                    userDetail = new ResponseData()
                    {
                        UserId = loginData.UserId,
                        FirstName = loginData.FirstName,
                        LastName = loginData.LastName,
                        EmailAddress = loginData.EmailAddress,
                        RegisteredDate = DateTime.Now
                    };
                }
                return userDetail;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
