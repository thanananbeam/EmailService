using EmailService.Model;
using EmailService.Model.UserEmail;
using System;
using System.Threading.Tasks;

namespace EmailService.Service
{
    public interface IEmailLogicService
    {
        ResponseModel Delete(Guid ids);
        ResponseModel FindAll();
        ResponseModel FindOne(Guid ids);
        ResponseModel Insert(CreateEmailModel model);
        ResponseModel Update(UpdateEmailModel model);
    }
}