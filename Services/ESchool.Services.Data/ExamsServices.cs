using ESchool.Data.Common.Repositories;
using ESchool.Data.Models;
using ESchool.Data.Models.Enums;
using ESchool.Services.Data.Contracts;
using ESchool.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESchool.Services.Data
{
    public class ExamsServices : IExamsServices
    {
        private readonly IDeletableEntityRepository<Exam> examRepository;
        private readonly IRepository<StudentsExams> studentsExamRepository;

        public ExamsServices(IDeletableEntityRepository<Exam> examRepository, IRepository<StudentsExams> studentsExamRepository)
        {
            this.examRepository = examRepository;
            this.studentsExamRepository = studentsExamRepository;
        }

        public async Task AddExamToStudent(int studentId, int examId)
        {
            StudentsExams studentsExams = new StudentsExams
            {
                StudentId = studentId,
                ExamId = examId,
            };

            await this.studentsExamRepository.AddAsync(studentsExams);
            await this.studentsExamRepository.SaveChangesAsync();
        }

        public async Task<int> CreateAsync(string examType, decimal result, int teacherId)
        {

            var exam = new Exam
            {
                ExamType = Enum.Parse<ExamType>(examType),
                Result = result,
                TeacherId = teacherId,
            };

            await this.examRepository.AddAsync(exam);
            await this.examRepository.SaveChangesAsync();

            return exam.Id;
        }

        public IEnumerable<T> GetAllStuentExans<T>(int studentId)
        {
            return this.examRepository.All()
                .Where(x => x.StudentsExam.All(i => i.StudentId == studentId))
                .To<T>()
                .ToList();
        }
    }
}
