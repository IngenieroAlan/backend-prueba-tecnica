using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using BackendPruebaTecnica.Custom;
using BackendPruebaTecnica.Models;
using BackendPruebaTecnica.Models.DTOs;
using BackendPruebaTecnica.Context;
using System.Security.Cryptography.X509Certificates;
using NuGet.Common;

namespace BackendPruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    //Permitimos que los usuarios no loggeados puedan loggearse
    [AllowAnonymous]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly Utilities _utilities;
        public LoginController(AppDbContext context, Utilities utilities)
        {
            _context = context;
            _utilities = utilities;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO user)
        {
            var foundedUser = await _context.Users.Where(u =>
                    u.UserName == user.UserName &&
                    u.Email == user.Email
                ).FirstOrDefaultAsync();
            if (foundedUser == null) {
                return StatusCode(StatusCodes.Status200OK, new{ isSuccess = false, token = ""});
            }
            else {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, token = _utilities.generateJWT(foundedUser) });
            }
        }
    }
}
