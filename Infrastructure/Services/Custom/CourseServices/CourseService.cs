using Domain.Models;
using Domain.ViewModels;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Custom.CourseServices
{
    public class CourseService : ICourseService
    {
        #region Private Variables
        private readonly IRepository<Course> _course;

        public CourseService(IRepository<Course> course)
        {
            _course = course;
                
        }
        #endregion


        #region GetAll
        public async Task<ICollection<CourseViewModel>> GetAll()
        {
            ICollection<CourseViewModel> CourseViewModel = new List<CourseViewModel>();

            ICollection<Course> courses = await _course.GetAll();

            foreach (Course course in courses)
            {
                CourseViewModel viewModel = new()
                {
                    Id = course.Id,
                    CouseId = course.CourseId,
                    CourseName = course.CourseName,
                    Instructor = course.Instructor,
                    Credits = course.Credits
                };
                CourseViewModel.Add(viewModel);
            }
            return CourseViewModel;
        }
        #endregion

        #region GetById
        public async Task<CourseViewModel> GetById(Guid id)
        {
            var result = await _course.GetById(id);
            if (result == null)
            {
                return null;
            }
            else
            {
                CourseViewModel viewModel = new()
                {
                    Id = result.Id,
                    CouseId = result.CourseId,
                    CourseName = result.CourseName,
                    Instructor = result.Instructor,
                    Credits = result.Credits
                };
                return viewModel;
            }
        }
        #endregion

        #region GetByName
        public async Task<CourseViewModel> GetByName(string name)
        {
            var result = await _course.GetByName(name);
            if (result == null)
            {
                return null;
            }
            else
            {
                CourseViewModel viewModel = new()
                {
                    Id = result.Id,
                    CouseId = result.CourseId,
                    CourseName = result.CourseName,
                    Instructor = result.Instructor,
                    Credits = result.Credits
                };
                return viewModel;
            }
        }
        #endregion

        #region GetLast
        public Course GetLast()
        {
            return _course.GetLast();
        }
        #endregion

        #region Insert
        public Task<bool> Insert(CourseInsertModel CourseInsertModel)   
        {
            Course course = new()
            {               
                CourseId = CourseInsertModel.CourseId,
                CourseName = CourseInsertModel.CourseName,
                Instructor = CourseInsertModel.Instructor,
                Credits = CourseInsertModel.Credits
            };
            return _course.Insert(course);
        }

        #endregion

        #region Update

        public async Task<bool> Update(CourseUpdateModel CourseUpdateModel)
        {
            Course course = await _course.GetById(CourseUpdateModel.Id);
            if (course != null)
            {
                course.CourseName = CourseUpdateModel.CourseName;
                course.Instructor = CourseUpdateModel.Instructor;
                course.Credits = CourseUpdateModel.Credits;

                var result = await _course.Update(course);
                return result;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Delete
        public async Task<bool> Delete(Guid id)
        {
            if (id != Guid.Empty)
            {
                Course course = await _course.GetById(id);
                if (course != null)
                {
                    //Direct Declaration
                    _ = _course.Delete(course);
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
        public Task<Course> Find(Expression<Func<Course, bool>> match)
        {
            return _course.Find(match);
        }
        #endregion
    }
}
