using Domain.Models;
using Domain.ViewModels;
using Infrastructure.Repositories;
using Infrastructure.Services.Custom.CourseServices;
using Infrastructure.Services.Custom.StudentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Custom.EnrollementServices
{
    public class EnrollementService : IEnrollementService
    {
        #region Private Variables
        private readonly IRepository<Enrollments> _enrollement;
        private readonly IRepository<Student> _student;
        private readonly IRepository<Course> _course;

        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;

        public EnrollementService(IRepository<Enrollments> enrollement, IRepository<Student> student, IRepository<Course> course, IStudentService studentService, ICourseService courseService)
        {
            _enrollement = enrollement;
            _student = student;
            _course = course;
            _studentService = studentService;
            _courseService = courseService;
        }
        #endregion


        #region GetAll
        public async Task<ICollection<EnrollementViewModel>> GetAll()
        {
            ICollection<EnrollementViewModel> EnrollementViewModel = new List<EnrollementViewModel>();

            ICollection<Enrollments> enrollments = await _enrollement.GetAll();

            foreach (Enrollments en in enrollments)
            {
                EnrollementViewModel viewModel = new()
                {
                    EnrollmentId = en.Id,
                    EnrollmentDate = en.EnrollmentDate,
                   /* StudentId = en.StudentId,*/
                   StudentId = en.StudentId,
                    CourseId = en.CourseId,
                  
                };
                EnrollementViewModel.Add(viewModel);
            }
            return EnrollementViewModel;
        }
        #endregion

        #region GetById
        public async Task<EnrollementViewModel> GetById(Guid id)
        {
            var result = await _enrollement.GetById(id);
            if (result == null)
            {
                return null;
            }
            else
            {
                EnrollementViewModel viewModel = new()
                {
                    EnrollmentId = result.Id,
                    EnrollmentDate = result.EnrollmentDate,
                    StudentId = result.StudentId,
                    CourseId = result.CourseId,
                };
                
                return viewModel;
            }
        }
        #endregion

        #region GetLast
        public Enrollments GetLast()
        {
            return _enrollement.GetLast();
        }
        #endregion

        #region Insert
        public async Task<bool> Insert(EnrollementInsertModel EnrollementInsertModel)
        {
            
            var student = await _student.Find(x => x.Id == EnrollementInsertModel.StudentId);
            var course = await _course.Find(x => x.Id == EnrollementInsertModel.CourseId);

            var result = await _enrollement.Find(x => x.StudentId == student.Id && x.CourseId == course.Id);

            if (EnrollementInsertModel.StudentId == student.Id && EnrollementInsertModel.CourseId == course.Id)
            {
                Enrollments viewModel = new()
                {
                    EnrollmentDate = EnrollementInsertModel.EnrollmentDate,
                    StudentId = EnrollementInsertModel.StudentId,
                    CourseId = EnrollementInsertModel.CourseId,
                };
                var enrollment = await _enrollement.Insert(viewModel);
                if(enrollment == true)
                {

                    return true;
                

                }
                else
                    return false;
            }
            else
                return false;
           
        }

        #endregion

        #region Update

        public async Task<bool> Update(EnrollementUpdateModel EnrollementUpdateModel)
        {
            Enrollments enrollments = await _enrollement.GetById(EnrollementUpdateModel.Id);
            
            enrollments.EnrollmentDate = EnrollementUpdateModel.EnrollmentDate;
            enrollments.StudentId = enrollments.StudentId;
            enrollments.CourseId = enrollments.CourseId;

            var result = await _enrollement.Update(enrollments);
            if (result == true)
            {
                return true;
            }
            else
                return false; 
        }
        #endregion

        #region Delete
        public async Task<bool> Delete(Guid id)
        {
            
            if (id != Guid.Empty)
            {
                Enrollments enrollments = await _enrollement.GetById(id);
                if (enrollments != null)
                {
                    //Direct Declaration
                    _ = _enrollement.Delete(enrollments);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Find
        public Task<Enrollments> Find(Expression<Func<Enrollments, bool>> match)
        {
            return _enrollement.Find(match);
        }
        #endregion
    }
}
