using back.Core.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace back.API.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : Controller
    {
        [HttpPost("login")]
        public async Task<IActionResult> Autorisation()
        {
            //if (ModelState.IsValid)
            //{
            //    var userService = new UserService(_usersRepository, _passwordHasher, _jwtProvider);
            //    if (_usersRepository.UserExist(model.email).Result)
            //    {
            //        var token = await userService.Login(model.email, model.password);
            //        if (token == "")
            //        {
            //            return BadRequest(new { message = "Неправильный пароль" });
            //        }
            //        HttpContext.Response.Cookies.Append("token", token, new CookieOptions());
            //        return Ok(new { token });
            //    }
            //    else
            //    {
            //        return NotFound(new { message = "User with this email is not registered" });
            //    }
            //}
            //else
            //{
            //    return BadRequest(ModelState);
            //}
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterView([FromBody] RegisterViewModel model)
        {
            //    if (ModelState.IsValid)
            //    {
            //        if (!_usersRepository.UserExist(model.email).Result)
            //        {
            //            foreach (var person1 in _context.Users)
            //            {
            //                _context.Remove(person1);
            //            }
            //            var userService = new UserService(_usersRepository, _passwordHasher, _jwtProvider);
            //            await userService.Register(model.email, model.password);
            //            HttpContext.Response.Cookies.Append("login", model.email);
            //            await _context.SaveChangesAsync();

            //            return Ok(new { message = "User registered successfully" });
            //        }
            //        else
            //        {
            //            return Conflict(new { message = "User with this email already registered" });
            //        }
            //    }
            //    return BadRequest(model);
            //}
            return Ok();
        }
    }
}
