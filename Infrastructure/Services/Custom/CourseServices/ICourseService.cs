using Domain.Models;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Custom.CourseServices
{
    public interface ICourseService
    {
        Task<ICollection<CourseViewModel>> GetAll();
        Task<CourseViewModel> GetById(Guid id);
        Task<CourseViewModel> GetByName(string name);
        Course GetLast();
        Task<bool> Insert(CourseInsertModel CourseInsertModel);
        Task<bool> Update(CourseUpdateModel CourseUpdateModel);
        Task<bool> Delete(Guid id);
        Task<Course> Find(Expression<Func<Course, bool>> match);
    }
}
