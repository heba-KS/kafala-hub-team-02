using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.DTOs.Students
{
   

    public record CreateStudentDto(
        string FullName,
        string Email
    );
}
