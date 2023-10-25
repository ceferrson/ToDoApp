using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ToDoAppNTier.Business.Interfaces;
using ToDoAppNTier.Business.Services;
using ToDoAppNTier.Business.ValidationRules;
using ToDoAppNTier.DataAccess.Contexts;
using ToDoAppNTier.DataAccess.UnitOfWork;
using ToDoAppNTier.Dtos.Dtos;

namespace ToDoAppNTier.Business.DependencyResolver.Microsoft
{
    public static class DependencyExtension
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddDbContext<ToDoContext>(options =>
            {
                options.UseSqlServer("Server=localhost; Database=ToDoDb; Trusted_Connection=True; Encrypt=False");
                //entity frame work uzerinden edilen proseslerin consol'a yansidilmasi
                options.LogTo(Console.WriteLine, LogLevel.Information);
            });

            //AutoMapper elave etme ilk usul:
            //var configuration = new MapperConfiguration(options =>
            //{
            //    options.AddProfile(new MappingProfiles());
            //});

            //var mapper = configuration.CreateMapper();
            // for using with dependency injection
            //services.AddSingleton(mapper);

            //AutoMapper elave etme 2 ci (esas) usul: 
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IUow, Uow>();
            services.AddScoped<IWorkService, WorkService>();
            //Adding Validators to Ioc
            services.AddTransient<IValidator<WorkDto>, WorkDtoValidator>();
            services.AddTransient<IValidator<WorkCreateDto>, WorkCreateDtoValidator>();
        }
    }
}
