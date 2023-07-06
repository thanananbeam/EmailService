using EmailService.Model;
using EmailService.Model.UserEmail;
using System.Collections.Generic;

namespace EmailService.Service
{
    public interface ILiteDbEmailService
    {
        ResponseModel Delete(int ids);
        ResponseModel FindAll();
        ResponseModel FindOne(int ids);
        ResponseModel Insert(CreateEmailModel model);
        ResponseModel Update(UserEmailModel model);
    }
}