WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

config.AddJsonFile("connectionstrings.json", optional: false);

var umbracoBuilder = builder.CreateUmbracoBuilder();


umbracoBuilder.AddBackOffice()
.AddWebsite()
.AddComposers()
.Build();

WebApplication app = builder.Build();

await app.BootUmbracoAsync();


app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();
