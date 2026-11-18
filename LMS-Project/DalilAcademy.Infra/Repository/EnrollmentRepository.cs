using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Exceptions;
using DalilAcademy.Core.Repository;
using DalilAcademy.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DalilAcademy.Infra.Repository;

public class EnrollmentRepository(AppDbContext db)
    : IEnrollmentRepository
{
    public async Task<Enrollment> CreateAsync(
        Enrollment enrollment)
    {
        try
        {
            db.Enrollments.Add(enrollment);
            await db.SaveChangesAsync();

            return enrollment;
        }
        catch (DbUpdateException ex)
            when (ex.InnerException is PostgresException
            {
                SqlState: PostgresErrorCodes.UniqueViolation
            })
        {
            // unique index (StudentId, CourseId) → 409
            throw new ConflictException(
                "This student is already enrolled in this course.");
        }
    }
}
