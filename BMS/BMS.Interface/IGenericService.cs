using BMS.Entity.IBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BMS.Interface
{
    public interface IGenericService<T, TDto> where T : IEntityBase
                                              where TDto : IDtoBase
    {
        IResponse<List<TDto>> GetAll();
        IResponse<List<TDto>> GetAll(Expression<Func<T, bool>> expression);
        IResponse<TDto> Find(int id);
        IResponse<TDto> Add(TDto model, bool saveChanges = false);
        Task<IResponse<TDto>> AddAsync(TDto model, bool saveChanges = false);
        IResponse<TDto> Update(TDto model, bool saveChanges = false);
        IResponse<bool> DeleteById(int id, bool saveChanges = false);
        IResponse<bool> Delete(TDto model, bool saveChanges = false);
        IResponse<IQueryable<TDto>> GetQueryable();
        IResponse<TDto> SingleOrDefault(Expression<Func<T, bool>> expression);
        void Save();
    }
}
