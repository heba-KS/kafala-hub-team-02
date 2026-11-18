using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.DTOs.Students
{
    

    public record UpdateStudentDto(
        string FullName,
        string Email
    );
}
