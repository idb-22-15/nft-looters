using back.Core.ViewModel;
using back.DataAccess;
using back.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back.API.Controllers
{
    [ApiController]
    [Route("organization")]
    public class OrganizationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrganizationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("diploma")]
        public async Task<IActionResult> DiplomCreate([FromBody] DiplomViewModel model)
        {
            string? userId = HttpContext.Request.Cookies["userId"];

            string? orgId = HttpContext.Request.Cookies["orgId"];

            if (userId != null && orgId == null)
            {
                return BadRequest(new { message = "вызови метод для юзера" });

            }
            else if (userId == null && orgId != null)
            {
                DiplomEntity diplomEntity = new DiplomEntity()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    IssuerId = Guid.Parse(orgId),
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
            string? orgId = HttpContext.Request.Cookies["orgId"];

            var OrgId = Guid.Parse(orgId);

            var diploms = await _context.Diploma
                 .Where(o => o.IssuerId == OrgId)
                 .ToListAsync();

            return Ok(diploms);
        }

        [Route("diploma/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DiplomDelete(Guid id)
        {
            string? orgId = HttpContext.Request.Cookies["orgId"];

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

            if (userId != null && orgId == null)
            {
                return BadRequest(new { message = "вызови метод для юзера" });

            }
            else if (userId == null && orgId != null)
            {
                CertificateEntity certificateEntity = new CertificateEntity()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    IssuerId = Guid.Parse(orgId),
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
            string? orgId = HttpContext.Request.Cookies["orgId"];

            var OrgId = Guid.Parse(orgId);

            var diploms = await _context.Certificates
                 .Where(o => o.IssuerId == OrgId)
                 .ToListAsync();

            return Ok(diploms);
        }

        [Route("certificate/{id}")]
        [HttpDelete]
        public async Task<IActionResult> CertificateDelete(Guid id)
        {
            string? orgId = HttpContext.Request.Cookies["orgId"];

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

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {

            var diploms = await _context.Organizations
                 .Where(o => o.Id == id)
                 .Include(o => o.Employees)
                 .Select(o => new {o.Id, o.Name, o.Email, o.Employees})
                 .ToListAsync();

            return Ok(diploms);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var diploms = await _context.Organizations
                 .Select(o => new { o.Id, o.Name, o.Email })
                 .ToListAsync();

            return Ok(diploms);
        }
    }
}
