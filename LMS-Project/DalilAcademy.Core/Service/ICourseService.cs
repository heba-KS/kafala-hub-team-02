using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.DTOs.Courses;

namespace DalilAcademy.Core.Service;

public interface ICourseService
{
    Task<PagedResult<CourseResponseDto>> GetAllAsync(
        int page, int pageSize, decimal? minPrice, decimal? maxPrice);

    Task<CourseResponseDto> GetByIdAsync(int id);

    Task<int> CountAsync();

    Task<CourseResponseDto> CreateAsync(CreateCourseDto dto);

    Task UpdateAsync(int id, UpdateCourseDto dto);

    Task DeleteAsync(int id);
}
