using ECommerce.Api.Common;
using ECommerce.Api.DTOs.Users;
using ECommerce.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    // GET: api/users
    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<UserResponseDto>>>> GetAll()
    {
        var users = await _userService.GetAllAsync();
        return Ok(new ApiResponse<List<UserResponseDto>>(true, "Kullanıcılar başarıyla getirildi", users));
    }

    // GET: api/users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<UserResponseDto>>> GetById(int id)
    {
        var user = await _userService.GetByIdAsync(id);

        if (user == null)
            return NotFound(new ApiResponse<UserResponseDto>(false, "Kullanıcı bulunamadı", null));

        return Ok(new ApiResponse<UserResponseDto>(true, "Kullanıcı başarıyla getirildi", user));
    }

    // POST: api/users
    [HttpPost]
    public async Task<ActionResult<ApiResponse<UserResponseDto>>> Create([FromBody] UserCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<UserResponseDto>(false, "Geçersiz veri", null));

        var created = await _userService.CreateAsync(dto);

        if (created == null)
            return Conflict(new ApiResponse<UserResponseDto>(false, "Bu email adresi zaten kullanılıyor", null));

        return CreatedAtAction(nameof(GetById), new { id = created.Id },
            new ApiResponse<UserResponseDto>(true, "Kullanıcı başarıyla oluşturuldu", created));
    }

    // PUT: api/users/5
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<UserResponseDto>>> Update(int id, [FromBody] UserUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<UserResponseDto>(false, "Geçersiz veri", null));

        var updated = await _userService.UpdateAsync(id, dto);

        if (updated == null)
            return NotFound(new ApiResponse<UserResponseDto>(false, "Kullanıcı bulunamadı", null));

        return Ok(new ApiResponse<UserResponseDto>(true, "Kullanıcı başarıyla güncellendi", updated));
    }

    // DELETE: api/users/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<object>>> Delete(int id)
    {
        var result = await _userService.DeleteAsync(id);

        if (!result)
            return NotFound(new ApiResponse<object>(false, "Kullanıcı bulunamadı", null));

        return Ok(new ApiResponse<object>(true, "Kullanıcı başarıyla silindi", null));
    }
}
