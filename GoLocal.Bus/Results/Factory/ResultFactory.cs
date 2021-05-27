using System.Collections.Generic;
using GoLocal.Bus.Results.Enums;

namespace GoLocal.Bus.Results.Factory
{
    public static class ResultFactory
    {
        public static Result Ok()
            => new Result().WithStatus(ResultStatus.Ok);

        public static Result NotFound<TEntity>(object id = null)
            => new Result().WithStatus(ResultStatus.NotFound).WithMessage(id == null ? 
                $"{typeof(TEntity).Name} not found." : $"{typeof(TEntity).Name} with id '{id}' not found.");

        public static Result BadRequest(string message, List<object> errors = null)
            => new Result().WithStatus(ResultStatus.BadRequest).WithErrors(errors).WithMessage(message);
        
        public static Result Unauthorized()
            => new Result().WithStatus(ResultStatus.Unauthorized);
        
        public static Result<TEntity> Ok<TEntity>(TEntity entity)
            => new Result<TEntity>().WithStatus(ResultStatus.Ok).WithEntity(entity);

        public static Result<TEntity> BadRequest<TEntity>(string message, List<object> errors = null)
            => new Result<TEntity>().WithStatus(ResultStatus.BadRequest).WithErrors(errors).WithMessage(message);

        public static Result<TEntity> Unauthorized<TEntity>()
            => new Result<TEntity>().WithStatus(ResultStatus.Unauthorized);
    }
}