using Domain.Models;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Custom.StudentServices
{
    public interface IStudentService
    {
        Task<ICollection<StudentViewModel>> GetAll();
        Task<StudentViewModel> GetById(Guid id);
        Task<StudentViewModel> GetByName(string name);
        Student GetLast();
        Task<bool> Insert(StudentInsertModel StudentInsertModel);
        Task<bool> Update(StudentUpdateModel StudentUpdateModel);
        Task<bool> Delete(Guid id);
        Task<Student> Find(Expression<Func<Student, bool>> match);
    }
}
