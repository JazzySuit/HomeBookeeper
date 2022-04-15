using AutoMapper;
using FluentValidation;
using HomeBookeper.Application.Behaviours;
using HomeBookeper.Application.Features.Products.Commands.CreateProduct;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HomeBookeper.Application;

public static class ServiceExtensions
{
	public static void AddApplicationLayer(this IServiceCollection services)
	{
		services.AddAutoMapper(Assembly.GetExecutingAssembly());
		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
		services.AddMediatR(Assembly.GetExecutingAssembly());
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

	}
}
