using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

using A2019128143.Data;
using Radzen;
namespace A2019128143
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        partial void OnConfigureServices(IServiceCollection services);

        partial void OnConfiguringServices(IServiceCollection services);

        public void ConfigureServices(IServiceCollection services)
        {
            OnConfiguringServices(services);

            services.AddHttpContextAccessor();
            services.AddScoped<HttpClient>(serviceProvider =>
            {

              var uriHelper = serviceProvider.GetRequiredService<NavigationManager>();

              return new HttpClient
              {
                BaseAddress = new Uri(uriHelper.BaseUri)
              };
            });

            services.AddHttpClient();
            services.AddScoped<BibliotecaDbService>();

            services.AddDbContext<A2019128143.Data.BibliotecaDbContext>(options =>
            {
              options.UseSqlServer(Configuration.GetConnectionString("BibliotecaDBConnection"));
            });

            services.AddRazorPages();
            services.AddServerSideBlazor().AddHubOptions(o =>
            {
                o.MaximumReceiveMessageSize = 10 * 1024 * 1024;
            });

            services.AddScoped<DialogService>();
            services.AddScoped<NotificationService>();
            services.AddScoped<TooltipService>();
            services.AddScoped<ContextMenuService>();

            OnConfigureServices(services);
        }

        partial void OnConfigure(IApplicationBuilder app, IWebHostEnvironment env);
        partial void OnConfiguring(IApplicationBuilder app, IWebHostEnvironment env);

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            OnConfiguring(app, env);
            if (env.IsDevelopment())
            {
                Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.Use((ctx, next) =>
                {
                    return next();
                });
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            OnConfigure(app, env);
        }
    }


  public class BlankTriggerAddingConvention : Microsoft.EntityFrameworkCore.Metadata.Conventions.IModelFinalizingConvention
  {
      public virtual void ProcessModelFinalizing(
          Microsoft.EntityFrameworkCore.Metadata.Builders.IConventionModelBuilder modelBuilder,
          Microsoft.EntityFrameworkCore.Metadata.Conventions.IConventionContext<Microsoft.EntityFrameworkCore.Metadata.Builders.IConventionModelBuilder> context)
      {
          foreach (var entityType in modelBuilder.Metadata.GetEntityTypes())
          {
              var table = Microsoft.EntityFrameworkCore.Metadata.StoreObjectIdentifier.Create(entityType, Microsoft.EntityFrameworkCore.Metadata.StoreObjectType.Table);
              if (table != null
                  && entityType.GetDeclaredTriggers().All(t => t.GetDatabaseName(table.Value) == null))
              {
                  entityType.Builder.HasTrigger(table.Value.Name + "_Trigger");
              }

              foreach (var fragment in entityType.GetMappingFragments(Microsoft.EntityFrameworkCore.Metadata.StoreObjectType.Table))
              {
                  if (entityType.GetDeclaredTriggers().All(t => t.GetDatabaseName(fragment.StoreObject) == null))
                  {
                      entityType.Builder.HasTrigger(fragment.StoreObject.Name + "_Trigger");
                  }
              }
          }
      }
  }
}
