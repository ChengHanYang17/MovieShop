using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> GetMoviesPurchasedBuUser()
        {
            // need to get the user Id from the token, using HttpContext
            return Ok();
        }
    }
}
