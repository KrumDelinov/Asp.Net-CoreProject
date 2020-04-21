namespace ESchool.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using ESchool.Data.Common.Models;
    using ESchool.Data.Models.Enums;

    public class Exam : BaseDeletableModel<int>
    {
        public ExamType ExamType { get; set; }

        [Required]
        [Range(2.00, 6.00)]
        public decimal Result { get; set; }

        [Required]
        public int TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; }

        public virtual ICollection<StudentsExams> StudentsExam { get; set; } = new HashSet<StudentsExams>();
    }
}
