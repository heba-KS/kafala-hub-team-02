using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.DTOs.Courses;
using DalilAcademy.Core.DTOs.Students;
using DalilAcademy.Core.Exceptions;
using DalilAcademy.Core.Mappings;
using DalilAcademy.Core.Repository;
using DalilAcademy.Core.Service;
using FluentValidation;

namespace DalilAcademy.Infra.Service;

public class StudentService(
    IStudentRepository studentRepository,
    IValidator<CreateStudentDto> createValidator,
    IValidator<UpdateStudentDto> updateValidator)
    : IStudentService
{
    public Task<PagedResult<StudentResponseDto>> GetAllAsync(int page, int pageSize)
        => studentRepository.GetPagedAsync(page, pageSize);

    public async Task<StudentResponseDto> GetByIdAsync(int id)
        => await studentRepository.GetDtoByIdAsync(id)
            ?? throw new NotFoundException($"Student {id} was not found.");

    public async Task<StudentResponseDto> CreateAsync(CreateStudentDto dto)
    {
        await createValidator.ValidateAndThrowAsync(dto);                    //  400

        var student = dto.ToEntity();
        var created = await studentRepository.CreateAsync(student);         // duplicate email → 409

        return created.ToDto();
    }

    public async Task UpdateAsync(int id, UpdateStudentDto dto)
    {
        await updateValidator.ValidateAndThrowAsync(dto);                    //  400

        var student = await studentRepository.GetByIdAsync(id)
            ?? throw new NotFoundException($"Student {id} was not found.");  //  404

        student.UpdateFromDto(dto);

        await studentRepository.UpdateAsync(student);                       // duplicate email → 409
    }

    public async Task DeleteAsync(int id)
    {
        if (!await studentRepository.DeleteAsync(id))
            throw new NotFoundException($"Student {id} was not found.");     //  404
    }

    // Lab 7 – Task 1: one SQL query (projection)
    public async Task<List<CourseResponseDto>> GetCoursesAsync(int id)
        => await studentRepository.GetCoursesAsync(id)
            ?? throw new NotFoundException($"Student {id} was not found.");
}
