using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace policyserver.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class PoliciesController : Controller
    {
        // GET api/policies
        [HttpGet()]
        public JsonResult Get()
        {
            var roles = User.FindAll("role");
            return new JsonResult(from r in roles select new { r.Type, r.Value });
        }
    }
}
