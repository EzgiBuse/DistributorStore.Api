using DistributorStore.Base.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributorStore.Operation.Services.Generic
{
    public interface IGenericService<TEntity, TRequest, TResponse>
    {
        ApiResponse<List<TResponse>> GetAll(params string[] includes);
        ApiResponse<TResponse> GetById(int id, params string[] includes);
        ApiResponse Insert(TRequest request);
        ApiResponse Update(int Id, TRequest request);
        ApiResponse Delete(int Id);
    }
}
