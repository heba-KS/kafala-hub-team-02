using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.DTOs.Courses;
using DalilAcademy.Core.DTOs.Students;
using DalilAcademy.Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace DalilAcademy.API.Controllers;

// Lab 6: zero try/catch blocks — errors go to GlobalExceptionHandler
[ApiController]
[Route("api/[controller]")]
public class StudentsController(IStudentService studentService)
    : ControllerBase
{
    // Lab 7 – Task 2: GET /api/students?page=1&pageSize=10
    [HttpGet]
    public async Task<ActionResult<PagedResult<StudentResponseDto>>> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = Paging.DefaultPageSize)
    {
        (page, pageSize) = Paging.Normalize(page, pageSize);

        return Ok(await studentService.GetAllAsync(page, pageSize));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<StudentResponseDto>> GetById(int id)
        => Ok(await studentService.GetByIdAsync(id));

    [HttpPost]
    public async Task<ActionResult<StudentResponseDto>> Create(CreateStudentDto dto)
    {
        var created = await studentService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = created.Id },
            created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateStudentDto dto)
    {
        await studentService.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await studentService.DeleteAsync(id);
        return NoContent();
    }

    // Lab 7 – Task 1: all courses a student is enrolled in — 1 SQL query
    [HttpGet("{id:int}/courses")]
    public async Task<ActionResult<List<CourseResponseDto>>> GetCourses(int id)
        => Ok(await studentService.GetCoursesAsync(id));
}
