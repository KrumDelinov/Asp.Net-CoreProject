namespace ESchool.Data.Models
{
    public class StudentsExams
    {
        public int StudentId { get; set; }

        public Student Student { get; set; }

        public int ExamId { get; set; }

        public Exam Exam { get; set; }
    }
}