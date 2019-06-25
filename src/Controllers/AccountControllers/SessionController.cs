

using System;
using System.Security.Claims;
using System.Threading.Tasks;
using backendProject.Database;
using backendProject.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backendProject.Controllers.AccountControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SessionController : ControllerBase
    {
        private ApplicationDbContext _dbContext;
        public SessionController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("sessions")]
        public async Task<ActionResult> GetSessions()
        {
            var uniqueid = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var profile = await _dbContext.Identity.Include(x => x.Sessions).FirstOrDefaultAsync(x => x.UniqueId.ToString() == uniqueid);
            if (profile != null)
            {
                return Ok(profile.Sessions);
            }

            return BadRequest();
        }

        [HttpPost("logout")]
        public async Task<ActionResult> LogoutSession([FromBody] string sessionIdToDelete)
        {
            var uniqueid = User.GetUniqueId();

            var sessionId = User.GetSessionId();

            if (sessionId.Equals(sessionIdToDelete))
            {
                return BadRequest("Cannot delete your own session!");
            }

            var sessionIdToDeleteGuid = new Guid(sessionIdToDelete);

            var session = await _dbContext.Session.FirstOrDefaultAsync(x => x.UniqueId.ToString() == uniqueid && x.SessionId == sessionIdToDeleteGuid);
            if (session != null)
            {
                _dbContext.Session.Remove(session);
                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    return Ok();
                }
            }
            else
            {
                return BadRequest("You don't own the sessionId or the session don't exist already!");
            }

            return BadRequest("Something happen");
        }
    }
}