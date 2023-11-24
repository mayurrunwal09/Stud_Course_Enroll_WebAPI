using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BaseEntity
{
    public class BaseEntityClass
    {
        [Key]
        public Guid Id { get; set; }
    }
}
