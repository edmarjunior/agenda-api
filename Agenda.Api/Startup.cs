using Agenda.Api.Data.Context;
using Agenda.Api.Data.Repository;
using Agenda.Api.Infra;
using Agenda.Api.Services.Medicos;
using Agenda.Api.Services.Pacientes;
using Agenda.Api.Services.Usuarios;
using Agenda.ApiServices.Usuarios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Api
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
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<AgendaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Agenda")));

            // retirando a validação automatica do ModelState
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // injetando dependencias do projeto
            InjectDependences(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // ativando o filtro de exceção
            app.ConfigureExceptionHandler();

            if (!env.IsDevelopment())
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }

        public void InjectDependences(IServiceCollection services)
        {
            services.AddScoped<Notification>();

            // services
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IMedicoService, MedicoService>();
            services.AddTransient<IPacienteService, PacienteService>();

            // repositories
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IMedicoRepository, MedicoRepository>();
            services.AddTransient<IPacienteRepository, PacienteRepository>();

        }
    }
}
