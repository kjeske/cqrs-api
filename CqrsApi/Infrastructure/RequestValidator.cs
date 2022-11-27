using FluentValidation;
using MediatR;

namespace CqrsApi.Infrastructure;

public abstract class RequestValidator<T> : AbstractValidator<T> where T : IBaseRequest
{
}