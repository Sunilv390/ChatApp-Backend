using CommonLayer.Models.RequestData;
using CommonLayer.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        ResponseData SignUp(UserSignUp registration);

        ResponseData UserLogin(UserLogin login);
    }
}
