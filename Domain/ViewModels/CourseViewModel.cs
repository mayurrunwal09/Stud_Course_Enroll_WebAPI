using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.ViewModels
{
    public class CourseViewModel
    {
        public Guid Id { get; set; }
        public string CouseId { get; set; }
        public string CourseName { get; set; }
        public string Instructor { get; set; }
        public string Credits { get; set; }
    }

    public class CourseInsertModel
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
    }

    public class CourseUpdateModel : CourseInsertModel
    {
        [Required(ErrorMessage = "Id is neccessory for updation...!")]
        public Guid Id { get; set; }
    }

}
