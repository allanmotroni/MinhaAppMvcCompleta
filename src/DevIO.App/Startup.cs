using DevIO.App.Data;
using DevIO.App.Extensions;
using DevIO.Business.Interfaces;
using DevIO.Data.Context;
using DevIO.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Globalization;

namespace DevIO.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<MeuDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddAutoMapper(typeof(Startup));



            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();

            services.AddMvc(options => {
                options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => "O valor preenchido é inválido para este campo.");
                options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(x => "Este campo precisa ser preenchido.");
                options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => "Este campo precisa ser preenchido.");
                options.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => "É necessário que o body na requisição não esteja vazio.");
                options.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor(x => "O valor preenchido é inválido para este campo.");
                options.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => "O valor preenchido é inválido para este campo.");
                options.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => "O campo deve ser numérico.");                
                options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor(x => "O valor preenchido é inválido para este campo.");
                options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(x => "O valor preenchido é inválido para este campo.");
                options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(x => "O campo deve ser numérico.");
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(x => "Este campo precisa ser preenchido.");
            });

            services.AddScoped<MeuDbContext>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            var defaultCulture = new CultureInfo("pt-BR");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultCulture),
                SupportedCultures = new List<CultureInfo> { defaultCulture },
                SupportedUICultures = new List<CultureInfo> { defaultCulture }
            };
            app.UseRequestLocalization(localizationOptions);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
