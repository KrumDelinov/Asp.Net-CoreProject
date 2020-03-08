namespace ESchool.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ESchool.Data.Common.Models;

    public class Exam : BaseDeletableModel<int>
    {
        public string Description { get; set; }

        public decimal Result { get; set; }

        public int SubjectId { get; set; }

        public virtual Subject Subject { get; set; }

        public int StudentId { get; set; }

        public virtual Student Student { get; set; }
    }
}
