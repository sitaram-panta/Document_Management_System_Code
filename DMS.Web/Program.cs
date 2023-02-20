

using DMS.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DMSWebContextConnection") ?? throw new InvalidOperationException("Connection string 'DMSWebContextConnection' not found.");
//var connectionString = builder.Configuration.GetConnectionString("DMSWebContextConnection") ?? throw new InvalidOperationException("Connection string 'DMSWebContextConnection' not found.");

builder.Services.AddDbContext<DMSDbContext>(options =>
   options.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=DMS;Integrated Security=true")
   );

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<DMSDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
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
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider( Path.Combine(builder.Environment.ContentRootPath, "wwwroot")),RequestPath = "/Documentupload/fileupload"
});
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
