using AlkemyDisney.Models;
using AlkemyDisney.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AlkemyDisney.Controllers
{

    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {


        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private IConfiguration _configuration;
        private string WriteToken(JwtSecurityToken token) => new JwtSecurityTokenHandler().WriteToken(token);

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, 
            RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;

        }

        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> Register(UserVM nuevoUsuario)
        {
            var usuarioExistente = await _userManager.FindByNameAsync(nuevoUsuario.userName);

            if (usuarioExistente != null)
            {
                return BadRequest(new
                {
                    Status = "Error",
                    Message = $"El usuario {nuevoUsuario.userName} ya existe"
                });

            }

            var usuario = new User
            {
                UserName = nuevoUsuario.userName,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(usuario, nuevoUsuario.Password);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new
                    {
                        Status = "Error",
                        Message = $"Error interno al intentar crear el usuario. Errors: {string.Join(",",result.Errors.Select(x => x.Description))}."
                    });
            }

            return Ok(
                new { 
                Status = "Success",
                Message = $"Se creo el usuario {usuario.UserName}"
                });

        }


        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login(UserVM usuario)
        {
            var resultado = await _signInManager.PasswordSignInAsync(usuario.userName, usuario.Password, false, false);

            if (resultado.Succeeded)
            {
                var usuarioLog = await _userManager.FindByNameAsync(usuario.userName);

                if (usuarioLog.IsActive)
                {
                    return Ok(await GetToken(usuarioLog));
                }

            }
            return StatusCode(StatusCodes.Status401Unauthorized,
                new
                {
                    Status = "Error",
                    Message = $"{usuario.userName} no tiene autorización"
                });
        }


        private async Task<LoginResponseVM> GetToken(User usuarioLog)
        {
            var rolesUsuario = await _userManager.GetRolesAsync(usuarioLog);

            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, usuarioLog.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            authClaims.AddRange(rolesUsuario.Select(x => new Claim(ClaimTypes.Role, x)));

            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));

            var token = new JwtSecurityToken(
                issuer: "",
                audience: "",
                expires: DateTime.Now.AddHours(2),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
                );

            return new LoginResponseVM
            {
                Token = WriteToken(token),
                Validez = token.ValidTo
            }; 

        }
    }


}
