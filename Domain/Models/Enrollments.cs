using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using Domain.BaseEntity;

namespace Domain.Models
{
    public class Enrollments : BaseEntityClass
    {
        [Required(ErrorMessage = "Enter Your Name ...!")]
        [Display(Name = "Enrollment Date")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date, ErrorMessage = ("Please Enter Valid Date Format ...!"))]
        public DateTime EnrollmentDate { get; set; }
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
       
        [JsonIgnore]
        public virtual Course Course { get; set; }
        public virtual Student Students { get; set; }
    }
}