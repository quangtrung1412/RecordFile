using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordFile;
//true 2 load trang khi thay đổi
// true 1 có hoặc ko kko bát buộ 
IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
.SetBasePath(Path.Combine(AppContext.BaseDirectory))
.AddJsonFile("appsettings.json", true, true)
.AddEnvironmentVariables();
var configuration = configurationBuilder.Build();

var host = Host.CreateDefaultBuilder();
host.ConfigureServices((_, services) =>
{
    services.AddSingleton<IApp, AppMain>();
    services.AddSingleton<IFileService, FileService>();
});

var app = host.Build();

var main = app.Services.GetService<IApp>();
main!.Run();