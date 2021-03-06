using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ProductivityTools.DateTimeTools;
using ProductivityTools.GetTask3.API.Controllers;
using ProductivityTools.GetTask3.App.Commands;
using ProductivityTools.GetTask3.App.Queries;
using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.Infrastructure;
using ProductivityTools.GetTask3.Infrastructure.Repositories;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {


        }

        private TaskCommands GTaskApp
        {
            get
            {
                var serviceCollection = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
                serviceCollection.ConfigureInfrastructureServices();
                var serviceProvider = serviceCollection.BuildServiceProvider();

                var taskrepository = serviceProvider.GetService<ITaskUnitOfWork>();
                var dateTime = serviceProvider.GetService<IDateTimePT>();
                var ts = new TaskCommands(taskrepository, dateTime);
                return ts;
            }
        }


        //private GTaskAppQuery GTaskAppQuery
        //{
        //    get
        //    {
        //        var serviceCollection = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
        //        serviceCollection.ConfigureInfrastructureServices();
        //        var serviceProvider = serviceCollection.BuildServiceProvider();

        //        var taskrepository = serviceProvider.GetService<ITaskRepository>();
        //        var ts = new GTaskAppQuery(taskrepository);
        //        return ts;
        //    }
        //}
    }
}