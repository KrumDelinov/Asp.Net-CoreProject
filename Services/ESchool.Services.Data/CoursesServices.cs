namespace ESchool.Services.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using ESchool.Data.Common.Repositories;
    using ESchool.Data.Models;
    using ESchool.Services.Data.Contracts;
    using ESchool.Services.Mapping;

    public class CoursesServices : ICoursesServices
    {
        private readonly IDeletableEntityRepository<Course> courseRepository;
        private readonly IDeletableEntityRepository<Teacher> teacherRepository;
        private readonly IRepository<CoursesTeachers> coursesTeacherRepository;
        private readonly IStudentsServices studentsServices;

        public CoursesServices(
            IDeletableEntityRepository<Course> gradeRepository,
            IDeletableEntityRepository<Teacher> teacherRepository,
            IRepository<CoursesTeachers> coursesTeacherRepository,
            IStudentsServices studentsServices)
        {
            this.courseRepository = gradeRepository;
            this.teacherRepository = teacherRepository;
            this.coursesTeacherRepository = coursesTeacherRepository;
            this.studentsServices = studentsServices;
        }

        public async Task<int> CreateAsync(int issue, string description, int techerId)
        {
            var course = new Course
            {
                Issue = issue,
                Description = description,
                TeacherId = techerId,

            };

            await this.courseRepository.AddAsync(course);
            await this.courseRepository.SaveChangesAsync();

            return course.Id;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.courseRepository.All().OrderBy(x => x.Issue).To<T>().ToList();
        }

        public Course GetCourse(int id)
        {
            var course = this.courseRepository.All().Where(x => x.Id == id).FirstOrDefault();

            return course;
        }

        public async Task UpdateCourse(Course course)
        {
            this.courseRepository.Update(course);
            await this.courseRepository.SaveChangesAsync();
        }

        public async Task DeleteCourse(Course course)
        {
            this.courseRepository.Delete(course);

            await this.courseRepository.SaveChangesAsync();
        }

        public T Course<T>(int id)
        {
            var course = this.courseRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();

            return course;
        }

        public async Task AddStudetToCourse(int courseId, int studentId)
        {
            var course = this.GetCourse(courseId);

            var student = this.studentsServices.GetStudent(studentId);

            course.Students.Add(student);
            await this.courseRepository.SaveChangesAsync();
        }

        public async Task AddTeachersToCourse(int courseId, int techerId)
        {

            CoursesTeachers courseTeachers = new CoursesTeachers
            {
                CourseId = courseId,
                TeacherId = techerId,
            };

            await this.coursesTeacherRepository.AddAsync(courseTeachers);
            await this.coursesTeacherRepository.SaveChangesAsync();

        }

        public IEnumerable<T> GetAllTeacherCourses<T>(int id)
        {
            return this.courseRepository.All()
                .Where(x => x.CourseTeachers.Any(i => i.TeacherId == id))
                .OrderBy(x => x.Issue)
                .To<T>()
                .ToList();
        }

        public int GetTeacherCourseId(int teacherId)
        {
            var course = this.courseRepository.All()
                .Where(x => x.TeacherId == teacherId)
                .Select(x => x.Id)
                .FirstOrDefault();
            return course;
        }
    }
}
