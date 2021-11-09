using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Project.BL;
using Project.DAL;
using Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjectAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddControllers().AddJsonOptions(option =>
            {
                option.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                 
            })
            .AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.Converters.Add(new TimeSpanToStringConverter());                    
            });; 



            services.AddDbContext<ProjectContext>(options => options.UseSqlServer(
            Configuration.GetSection("ConnectionString")["ProjectConnection"]));

            using var dbPSC = new ProjectContext();
            //  dbContext.Database.Migrate();

             services.AddAutoMapper(typeof(Startup));
  
            services.AddScoped<IUserDAL, UserDAL>();
            services.AddScoped<IUserBL, UserBL>();
            services.AddScoped<ILoginBL, LoginBL>();
            services.AddScoped<IExpertBL, ExpertBL>();
            services.AddScoped<IExpertDAL, ExpertDAL>();
            services.AddScoped<ISubjectDAL, SubjectDAL>();
            services.AddScoped<ISubjectBL, SubjectBL>();
            services.AddScoped<IRecommendsBL, RecommendsBL>();
            services.AddScoped<IRecommendsDAL, RecommendsDAL>();
            services.AddScoped<IMeetingBL, MeetingBL>();
            services.AddScoped<IMeetingDAL, MeetingDAL>();
            services.AddScoped<ICityBL, CityBL>();
            services.AddScoped<ICityDal, CityDAL>();

            services.AddCors();

            // services.AddTransient<StartupHostedService>(new StartupHostedService());
            //services.AddSingleton<StartupHostedService>();

            services.AddSingleton<IHostedService, StartupHostedService>();


    //        services
    //.AddHostedService<StartupHostedService>()
    //.AddSingleton<StartupHostedService>(x => x
    //    .GetServices<IHostedService>()
    //    .OfType<StartupHostedService>()
    //    .First());
            // services.AddMvc().AddNewtonsoftJson();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
    // using Microsoft.Extensions.FileProviders;
    // using System.IO;
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
           System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Images")),
        RequestPath = "/Images"
    });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors(builder => builder
      .AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader());
      
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

          
        }
    }
    public class TimeSpanToStringConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return TimeSpan.Parse(value);
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
