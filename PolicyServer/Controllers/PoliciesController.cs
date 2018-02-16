using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace policyserver.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class PoliciesController : Controller
    {
        // GET api/policies
        [HttpGet()]
        public IActionResult Get()
        {
            var roles = User.FindAll("role");
            if (roles == null)
                return BadRequest();

            var result = new JsonResult(from r in roles select new { r.Type, r.Value });
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }
    }
}
