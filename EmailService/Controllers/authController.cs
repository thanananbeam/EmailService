using EmailService.DataAccess;
using EmailService.DataAccess.Attributes;
using EmailService.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace EmailService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class authController : ControllerBase
    {
        private readonly ILogger<emailController> _logger;

        public authController(ILogger<emailController> logger)
        {
            _logger = logger;
        }


        [HttpPost("getToken")]
        [Authorization(AuthorizationType.None)]
        [ServiceFilter(typeof(ActionFilter))]
        public IActionResult getToken([FromBody, Required] TokenModel model)
        {
            TokenResponseModel _res = new TokenResponseModel();
            model.TrimData();

            if (!string.IsNullOrEmpty(model.email))
            {
                if (model.email == "user@example.com")
                {
                    _res.Token = JWT.GenerateToken(model.email);
                }
            }
            
            return Ok(_res);
        }
    }
}
