using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.DTOs.Students
{
  

    public record StudentResponseDto(
        int Id,
        string FullName,
        string Email
    );
}
