using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.DTOs.Courses;
using DalilAcademy.Core.DTOs.Students;
using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Exceptions;
using DalilAcademy.Core.Repository;
using DalilAcademy.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DalilAcademy.Infra.Repository;

public class StudentRepository(AppDbContext db)
    : IStudentRepository
{
    // Lecture 7: paginated, projected, no tracking
    public async Task<PagedResult<StudentResponseDto>> GetPagedAsync(int page, int pageSize)
    {
        var query = db.Students.AsNoTracking();

        var total = await query.CountAsync();                 // query 1: the total

        var items = await query
            .OrderBy(s => s.Id)                               // always order before Skip
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(s => new StudentResponseDto(s.Id, s.FullName, s.Email))
            .ToListAsync();                                   // query 2: one page

        return new PagedResult<StudentResponseDto>(items, page, pageSize, total);
    }

    public Task<StudentResponseDto?> GetDtoByIdAsync(int id)
        => db.Students
            .AsNoTracking()
            .Where(s => s.Id == id)
            .Select(s => new StudentResponseDto(s.Id, s.FullName, s.Email))
            .FirstOrDefaultAsync();

    // Lab 7 – Task 1: a student's courses in ONE query.
    // Returns null when the student does not exist (→ 404), an empty list when not enrolled.
    public Task<List<CourseResponseDto>?> GetCoursesAsync(int studentId)
        => db.Students
            .AsNoTracking()
            .Where(s => s.Id == studentId)
            .Select(s => s.Enrollments
                .OrderBy(e => e.CourseId)
                .Select(e => new CourseResponseDto(
                    e.Course!.Id,
                    e.Course.Title,
                    e.Course.Price,
                    e.Course.Instructor!.FullName))
                .ToList())
            .FirstOrDefaultAsync();

    public async Task<Student?> GetByIdAsync(int id)
    {
        return await db.Students.FindAsync(id);
    }

    public async Task<Student> CreateAsync(Student student)
    {
        try
        {
            db.Students.Add(student);
            await db.SaveChangesAsync();

            return student;
        }
        catch (DbUpdateException ex)
            when (ex.InnerException is PostgresException
            {
                SqlState: PostgresErrorCodes.UniqueViolation
            })
        {
            throw new ConflictException(
                "A student with this email already exists.");
        }
    }

    public async Task UpdateAsync(Student student)
    {
        try
        {
            db.Students.Update(student);
            await db.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
            when (ex.InnerException is PostgresException
            {
                SqlState: PostgresErrorCodes.UniqueViolation
            })
        {
            throw new ConflictException(
                "A student with this email already exists.");
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var student = await db.Students.FindAsync(id);

        if (student is null)
            return false;

        db.Students.Remove(student);
        await db.SaveChangesAsync();

        return true;
    }
}
