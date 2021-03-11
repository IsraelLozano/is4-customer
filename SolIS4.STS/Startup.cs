using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SolIS4.STS.Configuration;
using SolIS4.STS.Configuration.ClientConfigurator;
using IdentityServer4.Stores;
using dic.sso.identityserver.oauth.Repositories;
using dic.sso.identityserver.oauth.Configuration;
using System.Data;
using SolIS4.STS.Configuration.UserConfigurator;
using System.Data.SqlClient;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SolIS4.STS.Models;
using SolIS4.STS.Repositories.RepoIdentity;

namespace SolIS4.STS
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
            services.AddTransient<IUserValidator, UserValidator>();
            services.AddTransient<IUserRepository, UserRepository>();
            //services.AddTransient<IRepository, CorsPolicyService>();

            services.AddSingleton<Func<IDbConnection>>(() => new SqlConnection(Configuration.GetConnectionString("sqlConnectionNet")));

            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;



            services.AddIdentityServer()
                .AddDeveloperSigningCredential() //not something we want to use in a production environment;
                                                 //.AddCustomUserStore()
                                                 //.AddClientStore<CustomClientStore>()
                                                 //.AddResourceStore<CustomResourceStore>()

                  .AddClientStore<ClientStore>()
                  //.AddCorsPolicyService<CorsPolicyService>()
                  .AddResourceStore<ResourceStore>()
                  .AddResourceOwnerValidator<CustomResourceOwnerPasswordValidator>();

            //services.AddDbContext<ApplicationDbContext>(options =>options.UseSqlServer(Configuration.GetConnectionString("sqlConnection")));

            //.AddConfigurationStore(opt =>
            // {
            //     opt.ConfigureDbContext = c => c.UseSqlServer(Configuration.GetConnectionString("sqlConnection"),
            //         sql => sql.MigrationsAssembly(migrationAssembly));
            // })
            //.AddOperationalStore(opt =>
            //{
            //    opt.ConfigureDbContext = o => o.UseSqlServer(Configuration.GetConnectionString("sqlConnection"),
            //        sql => sql.MigrationsAssembly(migrationAssembly));
            //});

            services.AddControllersWithViews();

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SolIS4.STS", Version = "v1" });
            //});


            services.AddCors(options =>
            {

                options.AddPolicy("PublicApi", builder =>
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

                options.AddPolicy("Internas", b =>
                {
                    b.WithOrigins("http://localhost:4200").WithMethods(new string[] { "Get", "Post" }).AllowAnyHeader();
                });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SolIS4.STS v1"));
            }
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCors("PublicApi");
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();

            });
        }
    }
}
