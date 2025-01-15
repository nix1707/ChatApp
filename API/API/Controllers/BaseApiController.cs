using API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;

[ApiController, Route("api/[controller]")]
public class BaseApiController() : ControllerBase
{
    protected string GetUserId()
    {
        return User.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}
