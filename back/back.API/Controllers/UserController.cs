using back.Core.ViewModel;
using back.DataAccess;
using back.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace back.API.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : Controller
    {

        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpPost("diploma")]
        public async Task<IActionResult> DiplomCreate([FromBody] DiplomViewModel model)
        {
            string? userId = HttpContext.Request.Cookies["userId"];

            string? orgId = HttpContext.Request.Cookies["orgId"];

            if (userId == null && orgId != null)
            {
                return BadRequest(new { message = "вызови метод для орги" });

            }
            else if (userId != null && orgId == null)
            {
                DiplomEntity diplomEntity = new DiplomEntity()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    IssuerId = Guid.Parse(userId),
                    Image = model.Image,
                    UserId = model.UserId,
                    OrganisationName = model.OrganisationName,
                    Faculty = model.Faculty,
                    Specialization = model.Specialization,
                    DateOfIssue = model.DateOfIssue
                };

                await _context.Diploma.AddAsync(diplomEntity);
                await _context.SaveChangesAsync();

                return Ok(diplomEntity);
            }
            else { return BadRequest(new { message = "нет айди из кук или указаны оба айди" }); }
        }

        [Route("diploma")]
        [HttpGet]
        public async Task<IActionResult> DiplomGet()
        {
            string? userId = HttpContext.Request.Cookies["userId"];

            var UserId = Guid.Parse(userId);

            var diploms = await _context.Diploma
                 .Where(o => o.UserId == UserId)
                 .ToListAsync();

            return Ok(diploms);
        }

        [Route("diploma/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DiplomDelete(Guid id)
        {
            string? userId = HttpContext.Request.Cookies["userId"];

            var diploma = await _context.Diploma.FindAsync(id);

            if (diploma == null)
            {
                return NotFound(); // Если запись не найдена
            }

            // Удаляем запись
            _context.Diploma.Remove(diploma);

            await _context.SaveChangesAsync();

            return Ok(new { message = "Диплом удалён успешно" });
        }

        [HttpPost("certificate")]
        public async Task<IActionResult> CertificateCreate([FromBody] CertificateViewModel model)
        {
            string? userId = HttpContext.Request.Cookies["userId"];

            string? orgId = HttpContext.Request.Cookies["orgId"];

            if (userId == null && orgId != null)
            {
                return BadRequest(new { message = "вызови метод для орги" });

            }
            else if (userId != null && orgId == null)
            {
                CertificateEntity certificateEntity = new CertificateEntity()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    IssuerId = Guid.Parse(userId),
                    Image = model.image,
                    UserId = model.userId,
                    Title = model.title,
                };

                await _context.Certificates.AddAsync(certificateEntity);
                await _context.SaveChangesAsync();

                return Ok(certificateEntity);
            }
            else { return BadRequest(new { message = "нет айди из кук или указаны оба айди" }); }
        }

        [Route("certificate")]
        [HttpGet]
        public async Task<IActionResult> CertificateGet()
        {
            string? userId = HttpContext.Request.Cookies["userId"];

            var UserId = Guid.Parse(userId);

            var diploms = await _context.Certificates
                 .Where(o => o.UserId == UserId)
                 .ToListAsync();

            return Ok(diploms);
        }

        [Route("certificate/{id}")]
        [HttpDelete]
        public async Task<IActionResult> CertificateDelete(Guid id)
        {
            string? userId = HttpContext.Request.Cookies["userId"];

            var diploma = await _context.Certificates.FindAsync(id);

            if (diploma == null)
            {
                return NotFound(); // Если запись не найдена
            }

            // Удаляем запись
            _context.Certificates.Remove(diploma);

            await _context.SaveChangesAsync();

            return Ok(new { message = "Диплом удалён успешно" });
        }

        [HttpPost("recommendation")]
        public async Task<IActionResult> RecommendationCreate([FromBody] RecommendationViewModel model)
        {
            string? userId = HttpContext.Request.Cookies["userId"];

            string? orgId = HttpContext.Request.Cookies["orgId"];

            if (userId == null && orgId != null)
            {
                return BadRequest(new { message = "вызови метод для орги" });

            }
            else if (userId != null && orgId == null)
            {
                RecommendationEntity recommendationEntity = new RecommendationEntity()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    IssuerId = Guid.Parse(userId),
                    UserId = model.userId,
                    Title = model.title,
                    Text = model.text,
                };

                await _context.Recommendations.AddAsync(recommendationEntity);
                await _context.SaveChangesAsync();

                return Ok(recommendationEntity);
            }
            else { return BadRequest(new { message = "нет айди из кук или указаны оба айди" }); }
        }

        [Route("recommendation")]
        [HttpGet]
        public async Task<IActionResult> RecommendationGet()
        {
            string? userId = HttpContext.Request.Cookies["userId"];

            var UserId = Guid.Parse(userId);

            var diploms = await _context.Recommendations
                 .Where(o => o.UserId == UserId)
                 .ToListAsync();

            return Ok(diploms);
        }

        [Route("recommendation/{id}")]
        [HttpDelete]
        public async Task<IActionResult> RecommendationDelete(Guid id)
        {
            string? userId = HttpContext.Request.Cookies["userId"];

            var diploma = await _context.Recommendations.FindAsync(id);

            if (diploma == null)
            {
                return NotFound(); // Если запись не найдена
            }

            // Удаляем запись
            _context.Recommendations.Remove(diploma);

            await _context.SaveChangesAsync();

            return Ok(new { message = "Диплом удалён успешно" });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {

            var user = await _context.Users
                .Where(o => o.Id == id)
                .Include(p => p.Diplomas)
                .Include(p => p.Certificates)
                .Include(p => p.Organisation)
                .Select(o => new { o.Id, o.Email, o.FirstName, o.LastName, o.Wallet, o.Organisation, o.Diplomas, o.Certificates, o.JobTitle })
                .ToListAsync();

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var user = await _context.Users
                .Select(o => new { o.Id, o.Email, o.FirstName, o.LastName, o.Wallet, o.Organisation, o.Diplomas, o.Certificates, o.JobTitle })
                .ToListAsync();

            return Ok(user);
        }
    }
}
