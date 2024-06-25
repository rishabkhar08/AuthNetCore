using AuthNetCore.AuthorizationRequirements;
using Microsoft.AspNetCore.Authorization;

namespace AuthNetCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddAuthentication("MyCookieAuthIdentity").AddCookie("MyCookieAuthIdentity", options =>
            {
                options.Cookie.Name = "MyCookieAuthIdentity";
                options.ExpireTimeSpan = TimeSpan.FromSeconds(100);
                options.AccessDeniedPath = "/AccessDenied";
            });

            builder.Services.AddSingleton<IAuthorizationHandler, HRManagerProbationRequirementHandler>();
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin_Policy", policy =>
                {
                    policy.RequireClaim("Admin");
                });
                options.AddPolicy("HRDepartment_Policy", policy =>
                {
                    policy.RequireClaim("Department", "HR");
                });
                options.AddPolicy("HRManager_Policy", policy =>
                {
                    policy.RequireClaim("Department", "HR");
                    policy.RequireClaim("Manager");
                    policy.AddRequirements(new HRManagerProbationRequirement(6));
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
