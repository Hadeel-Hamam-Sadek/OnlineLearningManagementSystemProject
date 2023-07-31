using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineLearningManagementSystemProject.Models;
using OnlineLearningManagementSystemProject.Services;

namespace OnlineLearningManagementSystemProject
{
    public class Program
    {
        private static object asp;

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<LMSContext>(options =>
            {
                options.UseSqlServer("Data Source=.;Initial Catalog=OnlineLearningManagement;Integrated Security=True");
            });

            // usermanger and signinmanger and rolemaneger
            builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<LMSContext>();






            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<ICoursesRepository, CoursesRepository>();
            builder.Services.AddScoped<IStaffRepository, StaffRepository>();
            builder.Services.AddScoped<IStudentCourseRepository, StudentCourseRepository>();
            builder.Services.AddScoped<IStaffCourseRepository, StaffCourseRepository>();
            builder.Services.AddScoped<IUploadStaffFileRepository, UploadStaffFileRepository>();
            builder.Services.AddScoped<IdentityRepository, DentityRepository>();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(1);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            }
);
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthentication();//

            app.UseAuthorization();

            app.UseSession();




            //asp.UseAuthentication();
            //asp.UseAuthorization();



            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=login}/{id?}");

            app.Run();
        }
    }
}