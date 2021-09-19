using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


namespace IdentityServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [Authorize(Policy ="UserSecure")]
        [HttpGet("order")]
        public ActionResult GetOrderData()
        {
            return Ok("User accessed order api");
        }

        [Authorize(Policy = "AdminSecure")]
        [HttpGet("basket")]
        public ActionResult GetBasketData()
        {
            return Ok("Admin accessed basket api");
        }

        [Authorize(Policy = "AdminSecure")]
        [HttpGet("product")]
        public ActionResult GetProductData()
        {
            return Ok("Admin accessed product api");
        }
    }
}