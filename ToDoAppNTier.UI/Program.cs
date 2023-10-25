using Microsoft.Extensions.FileProviders;
using ToDoAppNTier.Business.DependencyResolver.Microsoft; 

var builder = WebApplication.CreateBuilder(args);
//Adding DbContext over extenion class and method in business.
builder.Services.AddDependencies();
builder.Services.AddControllersWithViews();
var app = builder.Build();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "node_modules")),
    RequestPath = "/node_modules"
});

app.UseStaticFiles();
app.UseExceptionHandler("/Home/Error");
app.UseStatusCodePagesWithReExecute("/Home/NotFound", "?statusCode={0}");
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
