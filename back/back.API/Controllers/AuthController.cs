using back.Core.ViewModel;
using back.DataAccess;
using back.DataAccess.Entities;
using back.DataAccess.Repositories;
using back.DataAccess.Repositories.Interfaces;
using back.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back.API.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IWebHostEnvironment _environment;
        public AuthController(ApplicationDbContext context,
             IUserRepository usersRepository,
              IPasswordHasher passwordHasher,
            IWebHostEnvironment environment)
        {
            _context = context;
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
            _environment = environment;
        }


        [HttpPost("user/login")]
        public async Task<IActionResult> Autorisation([FromBody] AutorisationViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_usersRepository.UserExist(model.email).Result)
                {

                    UserEntity user = await _context.Users.FirstOrDefaultAsync(c => c.Email == model.email);
                    var result = _passwordHasher.Verify(model.password, user.PasswordHash);
                    if (result == false)
                    {
                        return BadRequest(new { message = "Неправильный пароль" });
                    }

                    HttpContext.Response.Cookies.Append("userId", user.Id.ToString());
                    return Ok(user);
                }
                else
                {
                    return NotFound(new { message = "Нет юзера с таким мейлом" });
                }
            }
            else
            {
                return BadRequest(new { message = "хз че произошло" });
            }
        }

        [HttpPost("user/register")]
        public async Task<IActionResult> RegisterView([FromBody] RegisterViewModel model)
        { 
           
            if (ModelState.IsValid)
            {
                if (!_usersRepository.UserExist(model.email).Result)
                {
                    var hashedPassword = _passwordHasher.Generate(model.password);
                    UserEntity user = new UserEntity()
                    {
                        Id = Guid.NewGuid(),
                        Email = model.email,
                        PasswordHash = hashedPassword,
                        LastName = model.lastName,
                        FirstName = model.firstName,
                    };
                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();
                    HttpContext.Response.Cookies.Append("userId", user.Id.ToString());
                    return Ok(user);
                }
                else
                {
                    return Conflict(new { message = "уже есть юзер с таким мейлом" });
                }
            }
            return BadRequest(new { message = "Что то пошло не так хз что" });
        }

        [HttpPost("organization/login")]
        public async Task<IActionResult> AutorisationOrganization([FromBody] AutorisationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var check =  await _context.Organizations.AnyAsync(u => u.Email == model.email);
                if (check)
                {

                    OrganisationEntity user = await _context.Organizations.FirstOrDefaultAsync(c => c.Email == model.email);
                    var result = _passwordHasher.Verify(model.password, user.PasswordHash);
                    if (result == false)
                    {
                        return BadRequest(new { message = "Неправильный пароль" });
                    }

                    HttpContext.Response.Cookies.Append("orgId", user.Id.ToString());
                    return Ok(user);
                }
                else
                {
                    return NotFound(new { message = "Нет компании с таким мейлом" });
                } 
            }
            else
            {
                return BadRequest(new { message = "хз че произошло" });
            }
        }


        [HttpPost("organization/register")]
        public async Task<IActionResult> RegisterViewOrganization([FromBody] OrgViewModel model)
        {

            if (ModelState.IsValid)
            {
                
                
                var check =  await _context.Organizations.AnyAsync(u => u.Email == model.email);

                if (!check)
                {
                    var hashedPassword = _passwordHasher.Generate(model.password);
                    OrganisationEntity organization = new OrganisationEntity()
                    {
                        Id = Guid.NewGuid(),
                        Email = model.email,
                        PasswordHash = hashedPassword,
                        Name = model.name
                    };
                    await _context.Organizations.AddAsync(organization);
                    await _context.SaveChangesAsync();
                    HttpContext.Response.Cookies.Append("orgId", organization.Id.ToString());
                    return Ok(organization);
                }
                else
                {
                    return Conflict(new { message = "уже есть юзер с таким мейлом" });
                }
            }
            return BadRequest(new { message = "Что то пошло не так хз что" });
        }

        [HttpPost("organization/logout")]
        public async Task<IActionResult> LogoutOrganization()
        {

            var cookies = Request.Cookies.Keys;

            // Удаляем все куки
            foreach (var cookie in cookies)
            {
                Response.Cookies.Delete(cookie);
            }

            return Ok(new { message = "Все куки удалены" });
        }

        [HttpPost("user/logout")]
        public async Task<IActionResult> LogoutUser()
        {

            var cookies = Request.Cookies.Keys;

            // Удаляем все куки
            foreach (var cookie in cookies)
            {
                Response.Cookies.Delete(cookie);
            }

            return Ok(new { message = "Все куки удалены" });
        }
    }
}

