using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.ViewModels
{
    public class EnrollementViewModel
    {
        public Guid EnrollmentId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }

        public List<Student> Students { get; set; } = new List<Student>();
        public List<Course> Courses { get; set; } = new List<Course>();

    }

    public class EnrollementInsertModel 
    {
        [Required(ErrorMessage = "Enter Your Name ...!")]
        [Display(Name = "Enrollment Date")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date, ErrorMessage = ("Please Enter Valid Date Format ...!"))]

        public DateTime EnrollmentDate { get; set; }
       public Guid StudentId { get; set; }
       
        public Guid CourseId { get; set; }
        
    }

    public class EnrollementUpdateModel : EnrollementInsertModel
    {
        [Required(ErrorMessage = "Id is neccessory for updation...!")]
        public Guid Id { get; set; }
    }
}
