using BaseInfrastructureMessaging;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Product.Query.Infrastructure.Orm;
using Product.ReadEvent.Consumer.EventeHandlers;
using Product.ReadEvent.Consumer.Service;
using Product.ReadEvent.Domain.Repository;
using Product.ReadEvent.Infrastructure.Repository;
using Product.ReadEvent.Infrastructure.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProductQueryDbContext>();

builder.Services.AddScoped<IProductReadEventQueryRepository, ProductReadEventQueryRepository>();
builder.Services.AddScoped<IProductReadEventCommandRepository, ProductReadEventCommandRepository>();

builder.Services.AddScoped<IMessageBus, MassTransitAdapter>();

builder.Services.AddScoped<GetListProductsHub>();


builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ProductCreatedEventHandler>();
    x.AddConsumer<ProductUpdatedEventHandler>();
    x.AddConsumer<ProductDeletedEventHandler>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri("amqp://guest:guest@localhost:5672/"));
        cfg.ReceiveEndpoint("product-events", e =>
        {
            e.ConfigureConsumers(context);
        });
    });
});

builder.Services.AddMassTransitHostedService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseCors(cors => cors
.AllowAnyMethod()
.AllowAnyHeader()
.AllowAnyOrigin()
);

app.UseCors((g) => g.AllowAnyOrigin());
app.UseCors((g) => g.AllowCredentials());

app.MapHub<NotificationProductHubEvent>("/NotificationProductHub");

//app.UseEndpoints(endpoints =>
//{    
//    endpoints.MapHub<NotificationProductHubEvent>("/NotificationProductHub");
//});


app.Run();
