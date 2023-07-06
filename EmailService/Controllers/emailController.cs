using EmailService.DataAccess;
using EmailService.DataAccess.Attributes;
using EmailService.Model.UserEmail;
using EmailService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmailService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class emailController : ControllerBase
    {
        
        private readonly ILogger<emailController> _logger;
        private readonly IEmailLogicService _IEmailLogicService;

        public emailController(ILogger<emailController> logger, IEmailLogicService iEmailLogicService)
        {
            _IEmailLogicService = iEmailLogicService;
            _logger = logger;
        }


        [HttpGet("getall")]
        [Authorization(AuthorizationType.Admin)]
        [ServiceFilter(typeof(ActionFilter))]
        public IActionResult getall()
        {
            var res = _IEmailLogicService.FindAll();
            return Ok(res);
        }

        [HttpGet("get/{ids}")]
        [Authorization(AuthorizationType.Admin)]
        [ServiceFilter(typeof(ActionFilter))]
        public IActionResult getData([FromRoute] Guid ids)
        {
            var res = _IEmailLogicService.FindOne(ids);
            return Ok(res);
        }

        [HttpPost("insert")]
        [Authorization(AuthorizationType.Admin)]
        [ServiceFilter(typeof(ActionFilter))]
        public IActionResult insertEmail([FromBody, Required] CreateEmailModel model)
        {
            model.TrimData();
            
            var res = _IEmailLogicService.Insert(model);
            return Ok(res);
        }


        [HttpPost("update")]
        [Authorization(AuthorizationType.Admin)]
        [ServiceFilter(typeof(ActionFilter))]
        public IActionResult getUpdate([FromBody, Required] UpdateEmailModel model)
        {

            model.TrimData();
            var res = _IEmailLogicService.Update(model);
            return Ok(res);
        }

        [HttpPost("delete/{ids}")]
        [Authorization(AuthorizationType.Admin)]
        [ServiceFilter(typeof(ActionFilter))]
        public IActionResult getDelete(Guid ids)
        {

            var res = _IEmailLogicService.Delete(ids);
            return Ok(res);
        }

    }
}
