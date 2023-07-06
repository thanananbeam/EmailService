using EmailService.DataAccess.LiteDb;
using EmailService.Model;
using EmailService.Model.UserEmail;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailService.Service
{
    public class LiteDbEmailService : ILiteDbEmailService
    {
        private LiteDatabase _liteDb;

        private IRepoLiteDB _repo;

        public LiteDbEmailService(ILiteDbContext liteDbContext, IRepoLiteDB repo)
        {
            _liteDb = liteDbContext.Database;
            _repo = repo;
        }

        public ResponseModel FindAll()
        {
            var _resView = new ResponseModel();

            try
            {
                /*var result = _liteDb.GetCollection<UserEmailModel>("EmailGroup").FindAll();

                if (result.Count() > 0)
                {
                    _resView.data = result;
                    _resView.status = 200;
                }
                else 
                {
                    _resView.status = 404;
                }*/
                
            }
            catch (Exception ex)
            {

                _resView.data = ex.Message.ToString();
                _resView.status = 500;
            }

            return _resView;
        }

        public ResponseModel FindOne(int ids)
        {
            var _resView = new ResponseModel();

            try
            {
                /*var result = _liteDb.GetCollection<UserEmailModel>("EmailGroup")
                .Find(x => x.ids == ids).FirstOrDefault();

                if (result != null)
                {
                    _resView.data = result;
                    _resView.status = 200;
                }
                else 
                {
                    _resView.status = 404;
                }*/
            }
            catch (Exception ex)
            {

                _resView.data = ex.Message.ToString();
                _resView.status = 500;
            }

            return _resView;
        }

        public ResponseModel Insert(CreateEmailModel model)
        {
            var _resView = new ResponseModel();

            try
            {
                /*var _new = new UserEmailModel
                {
                    email = model.email
                };

                _liteDb.GetCollection<UserEmailModel>("EmailGroup").Insert(_new);

                _resView.status = 200;
                _resView.message = "insert success";*/

            }
            catch (Exception ex)
            {

                _resView.data = ex.Message.ToString();
                _resView.status = 500;
            }


            return _resView;
        }

        public ResponseModel Update(UserEmailModel model)
        {
            var _resView = new ResponseModel();

            try
            {
                /*_liteDb.GetCollection<UserEmailModel>("EmailGroup")
                .Update(model);

                _resView.status = 200;
                _resView.message = "update success";*/
            }
            catch (Exception ex)
            {

                _resView.data = ex.Message.ToString();
                _resView.status = 500;
            }

            return _resView;
        }

        public ResponseModel Delete(int ids)
        {
            var _resView = new ResponseModel();

            try
            {
                /*var user = _liteDb.GetCollection<UserEmailModel>("EmailGroup")
                .Find(x => x.ids == ids).FirstOrDefault();

                if (user != null)
                {
                    var col = _liteDb.GetCollection<UserEmailModel>("EmailGroup");
                    col.Delete(user.Id);

                    _resView.status = 200;
                    _resView.message = "delete success";
                }
                else 
                {
                    _resView.status = 404;
                    _resView.message = "can't delete";

                }*/
            }
            catch (Exception ex)
            {
                _resView.data = ex.Message.ToString();
                _resView.status = 500;
            }

            return _resView;
        }
    }
}
