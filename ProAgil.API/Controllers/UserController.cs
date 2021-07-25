using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProAgil.Domain.Identity;
using System.Threading.Tasks;
using System;
using ProAgil.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace ProAgil.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly UserManager<User> UserManager;
        private readonly SignInManager<User> SignInManager;
        private readonly IMapper Mapper;

        public UserController(IConfiguration configuration, UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            Configuration = configuration;
            UserManager = userManager;
            SignInManager = signInManager;
            Mapper = mapper;
        }

        [HttpGet("GetUser")]
        public IActionResult GetUser()
        {
            return Ok(new UserDTO());
        }

        [HttpPost("Register")]
        [AllowAnonymous] //Permite ser anonimo. Nao necessita de autenticação
        public async Task<IActionResult> Register(UserDTO inputModel)
        {
            try
            {
                User user = Mapper.Map<User>(inputModel);
                IdentityResult result = await UserManager.CreateAsync(user, inputModel.Password);
                UserDTO returnUser = Mapper.Map<UserDTO>(user);

                if (result.Succeeded)
                {
                    return Created("GetUser", returnUser);
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro! {exception.Message}");
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous] //Permite ser anonimo. Nao necessita de autenticação
        public async Task<IActionResult> Login(UserLoginDTO userLogin)
        {
            try
            {
                User user = await UserManager.FindByNameAsync(userLogin.UserName);
                Microsoft.AspNetCore.Identity.SignInResult signInResult = await SignInManager.CheckPasswordSignInAsync(user, userLogin.Password, false);

                if (signInResult.Succeeded)
                {
                    User appUser = await UserManager.Users.FirstOrDefaultAsync(user => user.NormalizedUserName.Equals(userLogin.UserName));
                    UserLoginDTO returnUser = Mapper.Map<UserLoginDTO>(appUser);

                    return Ok(new { 
                        token = GenerateJWT(appUser).Result,
                        user = returnUser
                    });
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro! {exception.Message}");
            }
        }

        private async Task<string> GenerateJWT(User user)
        {
            // Permissões/Autorizações
            List<Claim> claims = new List<Claim>() 
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName.ToString())
            };

            IEnumerable<string> roles = await UserManager.GetRolesAsync(user);
            foreach (string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value));
            SigningCredentials credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature); // Pesquisar essa algoritmo
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor() {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credential
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
