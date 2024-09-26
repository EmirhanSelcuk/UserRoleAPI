﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserRoleAPI.Controllers
{
    [Authorize(Roles ="User")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Success user Controller");
        }
    }
}
