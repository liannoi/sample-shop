using System;
using System.Linq;
using Autofac;
using Infrastructure.Application.Core.BusinessServices;
using Shop.Application.Entities;
using Shop.Autofac.ConsoleTests.DependencyInjection;

namespace Shop.Autofac.ConsoleTests
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Setup.
            var containerConfig = new ConsoleContainerConfig();

            containerConfig.Container.Resolve<IBusinessService<GoodDto>>().Select().ToList()
                .ForEach(e => Console.WriteLine($"{e.GoodName} {e.ManufacturerName}"));
        }
    }
}