using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.Interfaces;

namespace SchoolManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]  
public class StudentController : ControllerBase
{
    private readonly IStudentService _service;

    public StudentController(IStudentService service)
    {
        _service = service;
    }

    
    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());

     
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
        => Ok(await _service.GetByIdAsync(id));
 
    [Authorize(Roles = "Teacher,SuperAdmin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] StudentDto dto)
        => Ok(await _service.CreateAsync(dto));

   
    [Authorize(Roles = "Teacher,SuperAdmin")]
    [HttpPost("{id}/add-points")]
    public async Task<IActionResult> AddPoints(int id, int points)
        => Ok(await _service.AddPoints(id, points));

 
    [Authorize(Roles = "Teacher,SuperAdmin")]
    [HttpPost("{id}/swap")]
    public async Task<IActionResult> SwapStudent(int id, int newClassId)
        => Ok(await _service.SwapStudent(id, newClassId));

   
    [Authorize(Roles = "Teacher,SuperAdmin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] StudentDto dto)
        => Ok(await _service.UpdateAsync(id, dto));
     
    [Authorize(Roles = "Teacher,SuperAdmin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
        => Ok(await _service.DeleteAsync(id));
 
    [Authorize(Roles = "Teacher,SuperAdmin")]
    [HttpPatch("{id}/status")]
    public async Task<IActionResult> ActivateDeactivate(int id, bool isActive)
        => Ok(await _service.ActivateDeactivate(id, isActive));
}