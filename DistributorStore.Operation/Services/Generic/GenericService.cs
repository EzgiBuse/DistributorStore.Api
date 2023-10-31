using AutoMapper;
using DistributorStore.Base.Response;
using DistributorStore.Data.Uow;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributorStore.Operation.Services.Generic
{
    public class GenericService<TEntity, TRequest, TResponse> : IGenericService<TEntity, TRequest, TResponse> where TEntity : class
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public GenericService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public virtual ApiResponse Delete(int Id)
        {
            try
            {
                var entity = unitOfWork.DynamicRepository<TEntity>().GetById(Id);
                if (entity == null)
                {
                    return new ApiResponse("Record not found!");
                }

                unitOfWork.DynamicRepository<TEntity>().DeleteById(Id);
                unitOfWork.Complete();
                return new ApiResponse();
            }
            catch (Exception ex)
            {

                return new ApiResponse("GenericService.Delete");
            }
        }

        public virtual ApiResponse<List<TResponse>> GetAll(params string[] includes)
        {
            try
            {
                var entity = unitOfWork.DynamicRepository<TEntity>().GetAllWithInclude(includes);
                var mapped = mapper.Map<List<TEntity>, List<TResponse>>(entity);
                return new ApiResponse<List<TResponse>>(mapped);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<TResponse>>("GenericService.GetAll");
            }
        }

        public virtual ApiResponse<TResponse> GetById(int id, params string[] includes)
        {
            try
            {
                var entity = unitOfWork.DynamicRepository<TEntity>().GetByIdWithInclude(id, includes);
                var mapped = mapper.Map<TEntity, TResponse>(entity);
                return new ApiResponse<TResponse>(mapped);
            }
            catch (Exception ex)
            {

                return new ApiResponse<TResponse>("GenericService.GetById");
            }
        }
        public virtual ApiResponse Insert(TRequest request)
        {
            try
            {
                var entity = mapper.Map<TRequest, TEntity>(request);


                unitOfWork.DynamicRepository<TEntity>().Insert(entity);
                unitOfWork.DynamicRepository<TEntity>().Save();

                return new ApiResponse();
            }
            catch (Exception ex)
            {

                return new ApiResponse("GenericService.Insert");
            }
        }

        public virtual ApiResponse Update(int Id, TRequest request)
        {
            try
            {
                var exist = unitOfWork.DynamicRepository<TEntity>().GetById(Id);
                if (exist == null)
                {
                    return new ApiResponse("Record not found!");
                }

                var entity = mapper.Map<TRequest, TEntity>(request);
                unitOfWork.DynamicRepository<TEntity>().Update(entity);
                unitOfWork.DynamicRepository<TEntity>().Save();

                return new ApiResponse();
            }
            catch (Exception ex)
            {

                return new ApiResponse("GenericService.Update");
            }
        }
    }
}
