using EmailService.Model;
using EmailService.Model.UserEmail;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailService.DataAccess.LiteDb
{
    public interface IRepoLiteDB
    {
        ResponseModel Delete(Guid ids, UserEmailModel user);
        List<UserEmailModel> FindAll();
        UserEmailModel FindOne(Guid ids);
        ResponseModel Insert(UserEmailModel model);
        ResponseModel Update(UserEmailModel model);
    }
}