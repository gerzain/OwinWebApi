using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Claims;

namespace AppAutentificacion.Controllers
{
    public class DataController : ApiController
    {

        [AllowAnonymous]
        [HttpGet]
        [Route("api/data/abierta")]
        public IHttpActionResult Get()
        {
            return Ok("Sin autentificar " + DateTime.Now.ToString());

        }

        [Authorize]
        [HttpGet]
        [Route("api/data/authenticate")]
        public IHttpActionResult GetForAuthenticate()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return Ok("Autentificado  " + identity.Name);
        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("api/data/authorize")]
        public IHttpActionResult GetForAdmin()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(x => x.Value);
            return Ok("Autentificado " + identity.Name + " Como: " + string.Join(",", roles.ToList()));
        }

    }
}
