using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.DTO;
using Data.Entities;

namespace Core.Services
{
    internal interface ITodoListService
    {
        Task<ReturnTodoListDTO?> GetByIdAsync(int id);
        Task<IEnumerable<ReturnTodoListDTO?>> GetAllAsync();
        Task<ReturnTodoListDTO> CreateAsync(GetTodoListDTO dto);
        Task<ReturnTodoListDTO> UpdateAsync(GetTodoListDTO dto);
        Task DeleteAsync(int id);
        //IQueryable<ReturnTodoListDTO?> GetAllQueryAsync();

        //Task<IEnumerable<ReturnTodoListDTO>> FindByConditionAsync(Expression<Func<ReturnTodoListDTO, bool>> condition,
        //    params Expression<Func<ReturnTodoListDTO, object>>? [] attributes);
        //IQueryable<ReturnTodoListDTO> FindByCondition(Expression<Func<ReturnTodoListDTO, bool>> condition,
        //    params Expression<Func<ReturnTodoListDTO, object>>?[] attributes);

    }
}
