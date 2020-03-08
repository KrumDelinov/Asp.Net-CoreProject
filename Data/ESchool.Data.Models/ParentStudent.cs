namespace ESchool.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ParentStudent
    {
        public int ParentId { get; set; }

        public virtual Parent Parent { get; set; }

        public int StudentId { get; set; }

        public virtual Student Student { get; set; }
    }
}
