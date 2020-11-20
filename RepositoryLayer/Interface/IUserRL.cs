using CommonLayer.Models.RequestData;
using CommonLayer.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        ResponseData SignUp(UserSignUp registration);

        ResponseData UserLogin(UserLogin login);

    }
}
