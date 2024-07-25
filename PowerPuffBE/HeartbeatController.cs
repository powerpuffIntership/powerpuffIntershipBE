namespace PowerPuffBE;

using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service.Services;




[Route("api/[controller]")]
[ApiController]
public class HeartbeatController : ControllerBase
{

    public HeartbeatController()
    {
    }

    [HttpGet]
    public async Task<IActionResult> Beat()
    {
        return Ok("Tłusty Beat");
    }
}