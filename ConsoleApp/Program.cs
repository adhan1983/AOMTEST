using AOMTEST.ExtensionsMethods;
using AOMTEST.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();

var serviceProvider = serviceCollection.
            AddingDependenciasService().
            BuildServiceProvider();

var securityService = serviceProvider.GetRequiredService<ISecurityService>();

var lstISIn= new List<string>() { "123456789", "987466566", "23598989894" };

await securityService.InsertListSecutiryService(lstISIn);

Console.WriteLine("Success!");
