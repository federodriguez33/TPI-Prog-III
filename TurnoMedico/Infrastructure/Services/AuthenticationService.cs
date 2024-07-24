using Application.Interfaces;
using Application.Models.Request;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static Infrastructure.Services.AuthenticationService;

namespace Infrastructure.Services
{
    public class AuthenticationService : ICustomAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthenticationServiceOptions _options;

        public AuthenticationService(IUserRepository userRepository, IOptions<AuthenticationServiceOptions> options)
        {
            _userRepository = userRepository;
            _options = options.Value;
        }

        private User? ValidateUser(AuthenticationRequest authenticationRequest)
        {
            if (string.IsNullOrEmpty(authenticationRequest.DNI) || string.IsNullOrEmpty(authenticationRequest.Password))
                return null;

            var paciente = _userRepository.GetPacienteByDNI(authenticationRequest.DNI);

            if (paciente != null && paciente.Password == authenticationRequest.Password)
            {
                return paciente;
            }

            var profesional = _userRepository.GetProfesionalByDNI(authenticationRequest.DNI);

            if (profesional != null && profesional.Password == authenticationRequest.Password)
            {
                return profesional;
            }

            var admin = _userRepository.GetAdminByDNI(authenticationRequest.DNI);

            if (admin != null && admin.Password == authenticationRequest.Password)
            {
                return admin;
            }

            throw new UnauthorizedAccessException("Credenciales inválidas. Verifique su DNI y/o contraseña.");
        }

        public string Autenticar(AuthenticationRequest authenticationRequest)
        {
            var user = ValidateUser(authenticationRequest);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretForKey));
            var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            string role = user switch
            {
                Paciente => "paciente",
                Profesional => "profesional",
                Admin => "admin",
                _ => throw new UnauthorizedAccessException("Unknown user role")
            };

            var claimsForToken = new List<Claim>
    {
        new Claim("id", user.Id.ToString()),
        new Claim("given_name", user.Nombre),
        new Claim("family_name", user.Apellido),
        new Claim(ClaimTypes.Role, role)
    };

            var jwtSecurityToken = new JwtSecurityToken(
                _options.Issuer,
                _options.Audience,
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                credentials
            );

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return tokenToReturn;
        }
    


    public class AuthenticationServiceOptions
        {
            public const string AuthenticationService = "AuthenticationService";

            public string? Issuer { get; set; }
            public string? Audience { get; set; }
            public string? SecretForKey { get; set; }
        }

    }

}
