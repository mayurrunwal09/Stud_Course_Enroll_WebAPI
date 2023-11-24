using Domain.Models;
using Domain.ViewModels;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Custom.StudentServices
{
    public class StudentService : IStudentService
    {
        #region Private Variables
        private readonly IRepository<Student> _student;

        public StudentService(IRepository<Student> student)
        {
            _student = student;
        }
        #endregion

        
        #region GetAll
        public async Task<ICollection<StudentViewModel>> GetAll()
        {
            ICollection<StudentViewModel> studentViewModels = new List<StudentViewModel>();

            ICollection<Student> students = await _student.GetAll();
            
            foreach(Student student in students)
            {
                StudentViewModel viewModel = new()
                {
                    Id = student.Id,
                    StudentId = student.StudentId,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    DateOfBirth = student.DateOfBirth,
                    Address = student.Address
                };
                studentViewModels.Add(viewModel);
            }
            return studentViewModels;
        }
        #endregion

        #region GetById
        public async Task<StudentViewModel> GetById(Guid id)
        {
            var result = await _student.GetById(id);
            if(result == null)
            {
                return null;
            }
            else
            {
                StudentViewModel viewModel = new()
                {
                    Id = result.Id,
                    StudentId = result.StudentId,
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    DateOfBirth = result.DateOfBirth,
                    Address = result.Address
                };
                return viewModel;
            }
        }
        #endregion

        #region GetByName
        public async Task<StudentViewModel> GetByName(string name)
        {
            var result = await _student.GetByName(name);
            if (result == null)
            {
                return null;
            }
            else
            {
                StudentViewModel viewModel = new()
                {
                    Id = result.Id,
                    StudentId = result.StudentId,
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    DateOfBirth = result.DateOfBirth,
                    Address = result.Address
                };
                return viewModel;
            }
        }
        #endregion

        #region GetLast
        public Student GetLast()
        {
            return _student.GetLast();
        }
        #endregion

        #region Insert
        public Task<bool> Insert(StudentInsertModel StudentInsertModel)
        {
            Student student = new()
            {
                StudentId = StudentInsertModel.StudentId,
                FirstName = StudentInsertModel.FirstName,
                LastName = StudentInsertModel.LastName,
                DateOfBirth = StudentInsertModel.DateOfBirth,
                Address = StudentInsertModel.Address
            };
            return _student.Insert(student);
        }

        #endregion

        #region Update

        public async Task<bool> Update(StudentUpdateModel StudentUpdateModel)
        {
            Student student = await _student.GetById(StudentUpdateModel.Id);
            if(student != null)
            {
                student.FirstName = StudentUpdateModel.FirstName;
                student.LastName = StudentUpdateModel.LastName;
                student.DateOfBirth = StudentUpdateModel.DateOfBirth;
                student.Address = StudentUpdateModel.Address;

                var result = await _student.Update(student);
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
                Student student = await _student.GetById(id);
                if (student != null)
                {
                    //Direct Declaration
                    _ = _student.Delete(student);
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
        public Task<Student> Find(Expression<Func<Student, bool>> match)
        {
            return _student.Find(match);
        }
        #endregion
        
    }
}
