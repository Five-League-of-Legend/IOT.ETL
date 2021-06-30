using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using IOT.ETL.Repository.etl_data_engine;
using IOT.ETL.Repository.Login;
using IOT.ETL.Repository.sys_role;
using IOT.ETL.Repository.sys_user;
using IOT.ETL.Repository.sys_param;
using System;
using IOT.ETL.IRepository.etl_data_engine;
using IOT.ETL.IRepository.sys_param;
using IOT.ETL.Repository.etl_task_info;
using IOT.ETL.IRepository.etl_task_info;
using IOT.ETL.IRepository.Login;
using IOT.ETL.IRepository.sys_role;
using IOT.ETL.IRepository.sys_user;
using IOT.ETL.IRepository.BI_DataAnalysis;
using IOT.ETL.Repository.BI_DataAnalysis;
using IOT.ETL.IRepository.TaskIRepository;
using IOT.ETL.Repository.TaskRepository;
using IOT.ETL.IRepository.Etl_task_join_info;
using IOT.ETL.Repository.Etl_task_join_info;
using IOT.ETL.IRepository.sys_modules;
using IOT.ETL.Repository.sys_modules;
using IOT.ETL.IRepository.sysy_user_role;
using IOT.ETL.Repository.sys_user_role;
using IOT.ETL.IRepository.sys_role_modules;
using IOT.ETL.Repository.sys_role_modules;
using IOT.ETL.Repository.datasourceRepository;
using IOT.ETL.IRepository.IdatasourceRepository;

namespace IOT.ETL.Api
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IOT.ETL.Api", Version = "v1" });
            });
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(5);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            #region ×¢Èë
            services.AddSingleton<TaskIRepository, TaskRepository>();
            services.AddSingleton<Etl_task_join_infoIRepository, Etl_task_join_infoRepository>();

            #endregion
            services.AddScoped<etl_data_engine_IRepository, etl_data_engine_Repository>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<Isys_roleRepository, sys_roleRepository>();
            services.AddScoped<Isys_userRepository, sys_userRepository>();
            services.AddScoped<sys_param_IRepository, sys_param_Repository>();
            services.AddScoped<etl_task_info_IRepository, etl_task_info_Repository>();
            services.AddSingleton<IBI_DataAnalysisRepositor, BI_DataAnalysisRepository>();

            services.AddScoped<IdatasourceRepository, DatasourceRepository>();


            //¿çÓò
            services.AddScoped<Isys_modulesRepository, sys_modulesRepository>();
            services.AddScoped<Isys_user_roleRepository, sys_user_roleRepository>();
            services.AddScoped<Isys_role_modulesRepository, sys_role_modulesRepository>();
            //¿çÓò0
            services.AddCors(options =>
            options.AddPolicy("cors",
            p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IOT.ETL.Api v1"));
            }
            app.UseSession();
            app.UseCors("cors");
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
