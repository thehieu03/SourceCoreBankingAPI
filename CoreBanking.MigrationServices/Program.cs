using CoreBanking.MigrationServices;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();
//builder.AddNpgsqlDbContext<CoreBankingDbContext>(options =>
//{
//    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
//});
var host = builder.Build();
host.Run();
