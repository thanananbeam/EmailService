using EmailService.Model;
using EmailService.Model.UserEmail;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailService.DataAccess.LiteDb
{
    public class RepoLiteDB : IRepoLiteDB
    {
        private LiteDatabase _liteDb;

        public RepoLiteDB(ILiteDbContext liteDbContext)
        {
            _liteDb = liteDbContext.Database;
        }

        public List<UserEmailModel> FindAll()
        {
            List<UserEmailModel> result = _liteDb.GetCollection<UserEmailModel>("EmailGroup").FindAll().ToList();
            return result;
        }

        public UserEmailModel FindOne(Guid ids)
        {

            UserEmailModel result = _liteDb.GetCollection<UserEmailModel>("EmailGroup")
                                                .Find(x => x.Id == ids).FirstOrDefault();

            return result;
        }

        public ResponseModel Insert(UserEmailModel model)
        {
            var _resView = new ResponseModel();

            _liteDb.GetCollection<UserEmailModel>("EmailGroup").Insert(model);
            _resView.status = 200;

            return _resView;
        }

        public ResponseModel Update(UserEmailModel model)
        {
            var _resView = new ResponseModel();

            _liteDb.GetCollection<UserEmailModel>("EmailGroup")
                .Update(model);

            _resView.status = 200;
            _resView.message = "update success";

            return _resView;
        }

        public ResponseModel Delete(Guid ids, UserEmailModel user)
        {
            var _resView = new ResponseModel();

            var col = _liteDb.GetCollection<UserEmailModel>("EmailGroup");
            col.Delete(user.Id);

            _resView.status = 200;
            _resView.message = "delete success";

            return _resView;
        }


    }
}
