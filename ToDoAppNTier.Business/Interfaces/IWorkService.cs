using ToDoAppNTier.Dtos.Dtos;
using ToDoAppNTIer.Common.Response;

namespace ToDoAppNTier.Business.Interfaces
{
    public interface IWorkService
    {
        Task<IResponse<List<WorkDto>>> GetAll();
        Task<IResponse<WorkCreateDto>> Create(WorkCreateDto work);
        Task<IResponse<WorkDto>> GetById(int id);
        Task<IResponse> Remove(int id);
        Task<IResponse<WorkDto>> Update(WorkDto work);
    }
}
