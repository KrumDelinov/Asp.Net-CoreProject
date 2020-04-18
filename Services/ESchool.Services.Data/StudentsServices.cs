namespace ESchool.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using ESchool.Data.Common.Repositories;
    using ESchool.Data.Models;
    using ESchool.Services.Data.Contracts;
    using ESchool.Services.Mapping;

    public class StudentsServices : IStudentsServices
    {
        private readonly IDeletableEntityRepository<Student> studentRepository;
        private readonly IDeletableEntityRepository<Course> gradeRepository;
        private readonly IDeletableEntityRepository<Attendance> attendanceRepository;
        private readonly IAtendacesServices atendacesServices;

        public StudentsServices(
            IDeletableEntityRepository<Student> studentRepository,
            IDeletableEntityRepository<Course> gradeRepository,
            IDeletableEntityRepository<Attendance> attendanceRepository,
            IAtendacesServices atendacesServices)
        {
            this.studentRepository = studentRepository;
            this.gradeRepository = gradeRepository;
            this.attendanceRepository = attendanceRepository;
            this.atendacesServices = atendacesServices;
        }

        public async Task AddAttendanceToStudent(int studentId, int attndanceId)
        {
            var attendance = this.atendacesServices.GetAttendance(attndanceId);
            var student = this.GetStudent(studentId);
            student.Attendances.Add(attendance);

            await this.studentRepository.SaveChangesAsync();

        }

        public async Task<int> CreateAsync(string firstName, string lastName, DateTime birthDate, int gradeId)
        {
            var student = new Student
            {
                FirstName = firstName,
                LastName = lastName,
                BirthDate = birthDate,
                CourseId = gradeId,
            };

            await this.studentRepository.AddAsync(student);
            await this.studentRepository.SaveChangesAsync();

            return student.Id;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.studentRepository.All().To<T>().ToList();
        }

        public IEnumerable<T> GetAllCourseStudents<T>(int courseId)
        {
            return this.studentRepository.All().Where(x => x.CourseId == courseId).To<T>().ToList();
        }

        public int GetCount()
        {
            throw new NotImplementedException();
        }

        public Student GetStudent(int id)
        {
            var student = this.studentRepository.All().Where(x => x.Id == id).FirstOrDefault();

            return student;
        }

        public T Student<T>(int id)
        {
            var student = this.studentRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();

            return student;
        }


        public IEnumerable<T> GetAllParentStudents<T>(int id)
        {
            return this.studentRepository.All()
              .Where(x => x.StudentParents.Any(i => i.ParentId == id))
              .To<T>()
              .ToList();
        }

        public IEnumerable<T> GetAllStudentAttendaces<T>(int studentId)
        {
            var attendaces = this.attendanceRepository.All().Where(x => x.StudentId == studentId).To<T>().ToList();

            return attendaces;
        }
    }
}
