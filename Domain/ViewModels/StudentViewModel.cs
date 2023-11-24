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
    public class StudentViewModel
    {
        public Guid Id { get; set; }
        public string StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public DateTime DateOfBirth { get; set; }   
        public string Address { get; set; }
    }

    public class StudentInsertModel
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
    }

    public class StudentUpdateModel : StudentInsertModel
    {
        [Required(ErrorMessage = "Id is neccessory for updation...!")]
        public Guid Id { get; set; }
    }


}
