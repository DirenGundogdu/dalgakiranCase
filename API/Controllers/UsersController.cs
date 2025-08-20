using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Application.DTOs;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Tüm controller'ı koruma altına al
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    
    
}
