using ECommerce.Api.Common;
using ECommerce.Api.DTOs.Auth;
using ECommerce.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // POST: api/auth/login
    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Login([FromBody] LoginDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<AuthResponseDto>(false, "Geçersiz veri", null));

        var result = await _authService.LoginAsync(dto);

        if (result == null)
            return Unauthorized(new ApiResponse<AuthResponseDto>(false, "Email veya şifre hatalı", null));

        return Ok(new ApiResponse<AuthResponseDto>(true, "Giriş başarılı", result));
    }

    // POST: api/auth/register
    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Register([FromBody] RegisterDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<AuthResponseDto>(false, "Geçersiz veri", null));

        var result = await _authService.RegisterAsync(dto);

        if (result == null)
            return Conflict(new ApiResponse<AuthResponseDto>(false, "Bu email adresi zaten kullanılıyor", null));

        return CreatedAtAction(nameof(Register), 
            new ApiResponse<AuthResponseDto>(true, "Kayıt başarılı", result));
    }
}
