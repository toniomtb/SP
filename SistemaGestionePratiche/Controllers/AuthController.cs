using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SP.Logic.Interfaces;
using SP.Model.RequestModels;

namespace SP.Controllers
{
    [ApiController]
    [Route("/api/authenticate")]
    public class AuthController : Controller
    {
        private readonly IAuthLogic _authLogic;

        public AuthController(IAuthLogic authLogic)
        {
            _authLogic = authLogic;
        }

        [HttpPost]
        public IActionResult Login([FromBody] RequestUserCredentials userCredentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList());
            }

            var response = _authLogic.Authenticate(userCredentials.UserName, userCredentials.Password);
            if (response == null)
            {
                return BadRequest("Wrong username or password");
            }

            return Ok(response);
        }
    }
}