using HotChocolate.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using StudentDepartment.Abstraction;
using StudentDepartment.Abstraction.StudentDepartment.Repositories;
using StudentDepartment.Abstraction.StudentDepartment.Service;
using StudentDepartment.API.GrapgQL;
using StudentDepartment.API.GrapgQL.Types;
using StudentDepartment.Core.Services;
using StudentDepartment.Infrastructure.Data.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDepartment.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGraphQLServer()
               .AddQueryType<Query>()
               .AddType<DepartmentType>()
               .AddType<LectureType>()
               .AddType(new UuidType('D'))

               .AddMutationType<Mutation>()
               .AddFiltering()
               .AddErrorFilter<GraphQLErrorFilter>()
               .AddSorting();

            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<ILecturerRepository, LecturerRepository>();

            services.AddTransient<IDemoGraphQLApplication, DemoGraphQLApplication>();
            services.AddTransient<IDemoGraphQLQueryHandler, DemoGraphQLQueryHandler>();

         
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Department.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Department.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }
    }
}
