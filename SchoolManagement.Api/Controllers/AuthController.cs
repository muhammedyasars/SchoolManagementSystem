﻿using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Infrastructure.Data;
using SchoolManagement.Application.Interfaces;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using SchoolManagement.Application.DTOs;
using System.Security.Claims;

namespace SchoolManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IJwtService _jwtService;
    private readonly IPasswordHasher _passwordHasher;

    public AuthController(ApplicationDbContext context, IJwtService jwtService, IPasswordHasher passwordHasher)
    {
        _context = context;
        _jwtService = jwtService;
        _passwordHasher = passwordHasher;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(string email, string password)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Email == email);

        if (user == null)
            return Unauthorized("Invalid Email");

        var hashed = _passwordHasher.HashPassword(password);

        if (user.PasswordHash != hashed)
            return Unauthorized("Invalid Password");

        if (!user.IsActive)
            return Unauthorized("User is deactivated");

        var token = _jwtService.GenerateToken(user.Email, user.Role);

        return Ok(new
        {
            Token = token,
            Role = user.Role
        });
    }

    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Email == email);

        if (user == null)
            return NotFound("User not found");

        if (user.PasswordHash != _passwordHasher.HashPassword(dto.CurrentPassword))
            return BadRequest("Current password is incorrect");

        user.PasswordHash = _passwordHasher.HashPassword(dto.NewPassword);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Password changed successfully" });
    }
}