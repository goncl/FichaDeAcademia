using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FichaAcademia.AcessoDados;
using FichaAcademia.AcessoDados.Interfaces;
using FichaAcademia.AcessoDados.Repositorios;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rotativa.AspNetCore;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace FichaAcademia
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
            //Faz a referencia a conexão do appsetting.json
            services.AddDbContext<Contexto>(opcoes => opcoes.UseSqlServer(Configuration.GetConnectionString("Conexao"), b => b.MigrationsAssembly("FichaAcademia")));

            //Faz a referencia da interface e do repositorio
            services.AddTransient<ICategoriaExercicioRepositorio, CategoriaExercicioRepositorio>();
            services.AddTransient<IAdministradorRepositorio, AdministradorRepositorio>();
            services.AddTransient<IExercicioRepositorio, ExercicioRepositorio>();
            services.AddTransient<IProfessorRepositorio, ProfessorRepositorio>();
            services.AddTransient<IObjetivoRepositorio, ObjetivoRepositorio>();
            services.AddTransient<IAlunoRepositorio, AlunoRepositorio>();
            services.AddTransient<IFichaRepositorio, FichaRepositorio>();
            services.AddTransient<IListaExercicioRepositorio, ListaExercicioRepositorio>();

            //faz a injeção de dependencia no httpcontest para poder usar seçoes
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Configuração das seções. cada seçõa durara 1 hora
            services.AddSession(opcoes =>
            {
                opcoes.IdleTimeout = TimeSpan.FromHours(1);
            });

            //Faz a autenticação por cookies
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opcoes =>
                {
                    opcoes.LoginPath = "/Administradores/Login";
                });

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostingEnvironment env2)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //para deixar clara que estamos usando secão e autenticação
            app.UseSession();
            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            RotativaConfiguration.Setup(env2);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Administradores}/{action=Login}/{id?}");
            });
        }
    }
}
