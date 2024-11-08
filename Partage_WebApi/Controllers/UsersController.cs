using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Partage.DTOs;
using Partage.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Partage.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly UserManager<User> UserManager;

        public UsersController(UserManager<User> userManager)
        {
            UserManager = userManager;
        }


        [HttpPost]

        async public Task<ActionResult> Inscription(RegisterDTO register)
        {
            if (register.MotDePasse != register.MotDePasseConfirm)

            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new { Message = "Les deux mots de passe spécifiés sont différents" });

            }


            User user = new User()

            {
                UserName = register.Pseudo,
                Email = register.Courriel,
                Nom = register.Nom,
                Prenom = register.Prenom,
            };

            IdentityResult ir = await this.UserManager.CreateAsync(user, register.MotDePasse);

            if (!ir.Succeeded)

            {

                return StatusCode(StatusCodes.Status500InternalServerError,

                    new { Message = "La création de l'utilisateur a échoué." });

            }

            return Ok();

        }

        [HttpPost]

        public async Task<ActionResult> Connection(LoginDTO login)

        {

            User user = await UserManager.FindByEmailAsync(login.Courriel);

            if (user != null && await UserManager.CheckPasswordAsync(user, login.MotDePasse))

            {

                IList<string> roles = await UserManager.GetRolesAsync(user);
                List<Claim> authClaims = new List<Claim>();

                foreach (string role in roles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }
                authClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Cette fonction est couverte par la loi du Quebec"));

                JwtSecurityToken token = new JwtSecurityToken(

                    issuer: "http://localhost:5026",

                    audience: "http://localhost:4200",

                    claims: authClaims,

                    expires: DateTime.Now.AddMinutes(30),

                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)

                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    validTo = token.ValidTo,
                     userId = user.Id
                });
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new { Message = "Le nom d'utilisateur ou le mot de passe est invalide." });
            }
        }
    }
}
