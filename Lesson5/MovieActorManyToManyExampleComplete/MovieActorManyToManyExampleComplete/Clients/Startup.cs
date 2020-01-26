using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SimpleModelsAndRelations.Model;

namespace SimpleModelsAndRelations
{
  

  
  public class ApiOptions
  {
    public ApiOptions() { }
    public string ApiToken { get; set; }
  }

  public class ProjectNameOptions
  {
    public ProjectNameOptions() { }
    public string Value { get; set; }
  }

  public class Startup
  {
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

      builder.AddEnvironmentVariables();
      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

      services.AddDbContext<MovieContext>(options =>
      {
        options.UseSqlite("Filename=./SimpleModelsAndRelations.db");
      });

      services.Configure<ApiOptions>(Configuration);
      services.Configure<ProjectNameOptions>(options => options.Value = Configuration["ProjectName"]);


      services.AddMvc().AddJsonOptions(options =>
         {
           options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
           options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
           options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.RoundtripKind;
         });

      // Adds a default in-memory implementation of IDistributedCache.
      services.AddDistributedMemoryCache();

      services.AddSession(options =>
      {
        options.Cookie.Name = ".SimpleModelsAndRelations.Session804";
        options.Cookie.HttpOnly = true;
      });
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IOptions<ApiOptions> apiOptionsAccessor, IHostingEnvironment env, ILoggerFactory loggerFactory, MovieContext dbContext, IAntiforgery antiforgery )
    {

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseDatabaseErrorPage();
        app.UseBrowserLink();
        
      }
      else
      {
        //app.UseMiddleware(typeof(ErrorHandling));
      }
      app.Use(async (context, next) =>
      {
        context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
        await next();
      });

      app.UseStaticFiles();

      app.UseSession();
      

      app.UseMvc(routes =>
      {
      });

      Thread KeepAliveThread = new Thread(new ThreadStart(KeepAlive));

    }

    public static void KeepAlive() {
      while (true) {
        Thread.Sleep(TimeSpan.FromSeconds(10));
        Console.WriteLine("KEEP-ALIVE");
      }
    }
  }
}