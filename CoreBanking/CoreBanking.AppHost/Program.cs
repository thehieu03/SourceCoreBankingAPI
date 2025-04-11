var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithImageTag("latest")
    .WithVolume("corebaking-db", "/var/lib/postgresql/data")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithPgAdmin(rbuilder =>
    {
        rbuilder.WithImageTag("latest");
    });

var coreBakingDb = postgres.AddDatabase("corebanking-db", "corebanking");


var migrationServices = builder.AddProject<Projects.CoreBanking_MigrationServices>("corebanking-migrationservices");
builder.AddProject<Projects.CoreBaking_API>("corebaking-api")
    .WithReference(coreBakingDb)
    .WaitFor(postgres)
    .WaitForCompletion(migrationServices);


builder.Build().Run();
