// See https://aka.ms/new-console-template for more information
using BL.Concrete;
using BL.Models;
using DAL.Concrete;
using DAL.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Core.Abstract;
using RabbitMQ.Core.Concrete;
using RabbitMQ.Core.Data;
using RabbitMQ.Core.Entities;
using System.Reflection;

string time = Environment.GetCommandLineArgs()[1];

Console.WriteLine("RabbitMQ.ConsumerConsole Program.cs Açıldı");

var builder = new ConfigurationBuilder().SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)).AddJsonFile("project.json");
//new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("project.json");

Console.WriteLine("project.json okundu.");
var configuration = builder.Build();

var serviceProvider = new ServiceCollection()
    .AddSingleton<IConfiguration>(configuration)
    .AddSingleton<IRabbitMQConfiguration, RabbitMQConfiguration>()
    .AddSingleton<IRabbitMQService, RabbitMQService>()
    .AddSingleton<IObjectConvertFormat, ObjectConvertToFormatManager>()
    .AddSingleton<ISmtpConfiguration, SmtpConfiguration>()
    .AddSingleton<IMailSender, MailSender>()
    .AddSingleton<IDataModel<User>, UsersDataModel>()
    .AddSingleton<IConsumerService, ConsumerManager>()
    .AddSingleton<IVeriGirisiServices, VeriGirisiServices>()
    .AddSingleton<IVeriGirisiRepository, VeriGirisiRepository>()
    .BuildServiceProvider();


Console.WriteLine("serviceProvider ve Dependency injectionlar okundu.");

var consumerService = serviceProvider.GetService<IConsumerService>();
Console.WriteLine("consumerService alındı.");
Console.WriteLine($"consumerService.Start() başladı:{DateTime.Now.ToShortTimeString()}");
consumerService.Start();
Console.WriteLine($"consumerService.Start() bitti: {DateTime.Now.ToShortTimeString()}");
Thread.Sleep(int.Parse(time));
Console.Read();