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
using Domain.BaseEntity;

namespace Domain.Models
{
    public class Student : BaseEntityClass
    {
        [Required(ErrorMessage = "Please Enter StudentId...!")]
        [RegularExpression(@"(?:\s|^)#[A-Za-z0-9]+(?:\s|$)", ErrorMessage = "UserID start with # and Only Number and character are allowed eg(#User1001)")]
        [StringLength(10)]
        public string StudentId { get; set; }

        [Required(ErrorMessage = "Enter Your FirstName ...!")]
        [Display(Name = "Student FirstName")]
        [Column(TypeName = "Varchar(50)")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Enter Your LastName ...!")]
        [Display(Name = "Student LastName")]
        [Column(TypeName = "Varchar(50)")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Enter Your Date of Birth ...!")]
        [Display(Name = "Student DateOfBirth")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date, ErrorMessage = ("Please Enter Valid Date Format ...!"))]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Enter Your Address ")]
        [Display(Name = "Student Address")]
        [Column(TypeName = "Varchar(50)")]
        public string Address { get; set; }

        [JsonIgnore]
        public virtual ICollection<Enrollments> Enrollments { get; set; }
    }
}
