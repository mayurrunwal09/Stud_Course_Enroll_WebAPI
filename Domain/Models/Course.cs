using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using Domain.BaseEntity;

namespace Domain.Models
{
    public class Course : BaseEntityClass
    {
        [Required(ErrorMessage = "Please Enter CourseID...!")]
        [RegularExpression(@"(?:\s|^)#[A-Za-z0-9]+(?:\s|$)", ErrorMessage = "UserID start with # and Only Number and character are allowed eg(#User1001)")]
        [StringLength(10)]
        public string CourseId { get; set; }

        [Required(ErrorMessage = "Enter Your Course Name ...!")]
        [Display(Name = "Course Name")]
        [Column(TypeName = "Varchar(50)")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "Enter Your Instructor Name ...!")]
        [Display(Name = "Instructor Name")]
        [Column(TypeName = "Varchar(50)")]
        public string Instructor { get; set; }

        [Required(ErrorMessage = "Enter Your Credits ...!")]
        [Display(Name = "Instructor Credits")]
        [Column(TypeName = "Varchar(50)")]
        public string Credits { get; set; }

        [JsonIgnore]
        public virtual ICollection<Enrollments> Enrollments { get; set; }
    }
}