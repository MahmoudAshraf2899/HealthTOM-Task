using Boilerplate.Contracts.Enums;
using Boilerplate.Core.Entities.Visit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Core.Entities.Patient
{
    [Table("patient")]
    public class Patient : BaseEntityUpdate
    {
         

        [Column("name")]
        [Required(ErrorMessage = "Name is required"), MaxLength(50)]
        public string Name { get; set; }

        [Column("gender")]
        [Required(ErrorMessage = "Gender is required")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Birth Date is required")]
        [Column("birth_date")]
        public DateTime BirthDate { get; set; }

        [Column("email")]
        [Required(ErrorMessage = "Email is required"), MaxLength(100)]
        public string Email { get; set; }

        public virtual ICollection<Visit.Visit> Visits { get; set; }

    }
}
