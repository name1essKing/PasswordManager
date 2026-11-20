using Microsoft.AspNetCore.Mvc;

namespace PasswordManager.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class PasswordControllerBase : ControllerBase
{
}