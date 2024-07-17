using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : Controller
    {

        private readonly ICustomAuthenticationService _customAuthenticationService;

        public AuthenticationController(ICustomAuthenticationService customAuthenticationService)
        {

            _customAuthenticationService = customAuthenticationService;

        }

        [HttpPost("authenticate")]

        public ActionResult<string> Autenticar(AuthenticationRequest authenticationRequest) //Enviamos como parámetro la clase que creamos arriba
        {
            string token = _customAuthenticationService.Autenticar(authenticationRequest); //Lo primero que hacemos es llamar a una función que valide los parámetros que enviamos.

            return Ok(token);
        }

    }
}
