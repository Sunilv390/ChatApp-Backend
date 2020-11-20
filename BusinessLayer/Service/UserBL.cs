using BusinessLayer.Interface;
using CommonLayer.Models.RequestData;
using CommonLayer.Models.Response;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;
        public UserBL(IUserRL _userRL)
        {
            userRL = _userRL;
        }

        public ResponseData SignUp(UserSignUp registration)
        {
            try
            {
                ResponseData data = userRL.SignUp(registration);
                return data;
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
                ResponseData data = userRL.UserLogin(login);
                return data;
            }
            catch (Exception e)
            {
                // Exception
                throw new Exception(e.Message);
            }
        }
    }
}
