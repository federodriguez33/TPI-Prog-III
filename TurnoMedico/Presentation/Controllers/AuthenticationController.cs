using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/Autenticacion")]
    [ApiController]
    public class AuthenticationController : Controller
    {

        private readonly ICustomAuthenticationService _customAuthenticationService;

        public AuthenticationController(ICustomAuthenticationService customAuthenticationService)
        {

            _customAuthenticationService = customAuthenticationService;

        }

        [HttpPost]

        public ActionResult<string> Autenticar(AuthenticationRequest authenticationRequest) //Enviamos como parámetro la clase que creamos
        {
            string token = _customAuthenticationService.Autenticar(authenticationRequest); //Llamamos a función que valide los parámetros que enviamos y genere el token

            return Ok(token);
        }

    }
}
