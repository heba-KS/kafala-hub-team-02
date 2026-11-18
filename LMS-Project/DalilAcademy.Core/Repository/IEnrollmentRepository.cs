using System;
using System.Collections.Generic;
using System.Text;
using global::DalilAcademy.Core.Entities;

namespace DalilAcademy.Core.Repository
{

    public interface IEnrollmentRepository
    {
        Task<Enrollment> CreateAsync(Enrollment enrollment);
    }

}
