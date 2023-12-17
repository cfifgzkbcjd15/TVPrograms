using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TVPrograms.Data.Repository;
using TVPrograms.Models.Users;

namespace TVPrograms.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Repository _repository;
        public UsersController(Repository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<ResponseUser> Get()
        {
            return await _repository.GetUserById(new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value));
        }
    }
}
