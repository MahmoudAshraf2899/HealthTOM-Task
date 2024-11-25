using Autofac.Features.OwnedInstances;
using Boilerplate.Contracts.Enums;
using Boilerplate.Core.Entities.Patient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Core.Entities.Visit
{
    [Table("visit")]
    public class Visit : BaseEntityUpdate
    {
        [Column("exam_status")]
        [Required(ErrorMessage = "Exam Status Is Required")]
        public ExamStatus ExamStatus { get; set; }

        [Column("exam_type")]
        [Required(ErrorMessage = "Exam Type Is Required")]
        public ExamType ExamType { get; set; }

        [Column("comment")]
        public string? Comment { get; set; }

        [Required(ErrorMessage = "Patient Id is required")]
        [Column("patient_id")]
        public long PatientId { get; set; }


        [ForeignKey(nameof(PatientId))]
        public virtual Patient.Patient Patient { get; set; }
    }
}
