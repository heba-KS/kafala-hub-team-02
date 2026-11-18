using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.DTOs.Courses;
using DalilAcademy.Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace DalilAcademy.API.Controllers;

// Lecture 6: no try/catch, no null checks — the controller only describes the happy path
[ApiController]
[Route("api/[controller]")]
public class CoursesController(ICourseService courseService)
    : ControllerBase
{
    // GET /api/courses?page=1&pageSize=10&minPrice=50&maxPrice=100
    [HttpGet]
    public async Task<ActionResult<PagedResult<CourseResponseDto>>> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = Paging.DefaultPageSize,
        [FromQuery] decimal? minPrice = null,
        [FromQuery] decimal? maxPrice = null)
    {
        (page, pageSize) = Paging.Normalize(page, pageSize);

        return Ok(await courseService.GetAllAsync(page, pageSize, minPrice, maxPrice));
    }

    // Lab 7 – Task 5: GET /api/courses/count
    [HttpGet("count")]
    public async Task<ActionResult<int>> Count()
        => Ok(await courseService.CountAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CourseResponseDto>> GetById(int id)
        => Ok(await courseService.GetByIdAsync(id));

    [HttpPost]
    public async Task<ActionResult<CourseResponseDto>> Create(CreateCourseDto dto)
    {
        var created = await courseService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = created.Id },
            created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateCourseDto dto)
    {
        await courseService.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await courseService.DeleteAsync(id);
        return NoContent();
    }
}
