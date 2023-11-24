using Domain.Models;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Custom.EnrollementServices
{
    public interface IEnrollementService
    {
        Task<ICollection<EnrollementViewModel>> GetAll();
        Task<EnrollementViewModel> GetById(Guid id);
        Enrollments GetLast();
        Task<bool> Insert(EnrollementInsertModel EnrollementInsertModel);
        Task<bool> Update(EnrollementUpdateModel EnrollementUpdateModel);
        Task<bool> Delete(Guid id);
        Task<Enrollments> Find(Expression<Func<Enrollments, bool>> match);
    }
}
