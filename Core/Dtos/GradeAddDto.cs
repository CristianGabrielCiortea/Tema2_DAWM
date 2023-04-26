using DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class GradeAddDto
    {
        [Required]
        public double Value { get; set; }

        [Required]
        public CourseType Course { get; set; }

        [Required]    
        public int StudentId { get; set; }
    }
}