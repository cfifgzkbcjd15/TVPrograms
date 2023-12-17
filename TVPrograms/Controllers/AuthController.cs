using TVPrograms.Data.Repository;
using TVPrograms.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TVPrograms.Models;

namespace TVPrograms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public Repository _repository;
        public AuthController(Repository repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// получение авторизационного токена
        /// </summary>
        /// <param name="loginData"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<object> Token(LoginModel loginData)
        {
            var identity = await _repository.CheckUser(loginData);
            //var identity = GetIdentity(username, password);
            if (identity == null)
            {
                return null;
            }
            var acesstoken = await CreateAcessToken(identity);
            return new { token=acesstoken};
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public async Task<string> CreateAcessToken(ClaimsAuth identity)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,identity.Id.ToString()),
                    new Claim("Sex",identity.Sex),
                    new Claim("Age",identity.Age.ToString()),
                    //new Claim(ClaimTypes.Role,identity.Role),
                    //new Claim(ClaimTypes.Email,identity.Email)
                };
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
            notBefore: now,
            claims: claims,
                    expires: now.AddHours(14),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            //var db = new Repository();
            //db.RefreshToken(token, encodedJwt);

            return encodedJwt;
        }


    }
}
