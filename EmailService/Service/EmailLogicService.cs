using EmailService.DataAccess.LiteDb;
using EmailService.Model;
using EmailService.Model.UserEmail;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EmailService.Service
{
    public class EmailLogicService : IEmailLogicService
    {

        private IRepoLiteDB _repo;

        public EmailLogicService(IRepoLiteDB repo)
        {
            _repo = repo;
        }

        public ResponseModel FindAll()
        {
            ResponseModel _resView = new ResponseModel();

            try
            {
                var result = _repo.FindAll();
                _resView.data = result;
                _resView.status = 200;

            }
            catch (Exception ex)
            {

                _resView.data = ex.Message.ToString();
                _resView.status = 500;
            }

            return _resView;
        }

        public ResponseModel FindOne(Guid ids)
        {
            ResponseModel _resView = new ResponseModel();

            try
            {
                var result = _repo.FindOne(ids);

                _resView.data = result;
                _resView.status = 200;
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
            ResponseModel _resView = new ResponseModel();

            try
            {
                // check dup
                var result = _repo.FindAll();

                var dataCheck = result.Where(x => x.email == model.email).FirstOrDefault();

                if (dataCheck == null)
                {
                    var _new = new UserEmailModel
                    {
                        email = model.email
                    };

                    _resView = _repo.Insert(_new);
                }
                else 
                {
                    _resView.status = 001;
                    _resView.message = "can't insert";
                }

            }
            catch (Exception ex)
            {

                _resView.data = ex.Message.ToString();
                _resView.status = 500;
            }


            return _resView;
        }

        public ResponseModel Update(UpdateEmailModel model)
        {
            ResponseModel _resView = new ResponseModel();

            try
            {
                // check dup
                var result = _repo.FindOne(model.Id);

                if (result != null)
                {
                    UserEmailModel _update = new UserEmailModel();
                    _update.email = model.email;
                    _resView = _repo.Update(_update);

                }
                else
                {
                    _resView.status = 002;
                    _resView.message = "can't update";
                }
                
            }
            catch (Exception ex)
            {

                _resView.data = ex.Message.ToString();
                _resView.status = 500;
            }

            return _resView;
        }

        public ResponseModel Delete(Guid ids)
        {
            ResponseModel _resView = new ResponseModel();

            try
            {
                var data = _repo.FindOne(ids);

                if (data != null)
                {
                    _resView = _repo.Delete(ids, data);
                }
                else 
                {
                    _resView.status = 404;
                    _resView.message = "can't delete";
                }
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
