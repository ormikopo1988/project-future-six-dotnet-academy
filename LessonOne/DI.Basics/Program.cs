﻿using Autofac;
using System;
using System.Linq;
using System.Reflection;

namespace DI.Basics
{
    class Program
    {
        static IContainer container;

        static void Main(string[] args)
        {
            var exit = false;

            while (!exit)
            {
                Console.WriteLine();
                Console.WriteLine("1 - Stage 1");
                Console.WriteLine("2 - Stage 2");
                Console.WriteLine("3 - Stage 3");
                Console.WriteLine("0 - Exit");
                Console.WriteLine();
                Console.Write("Select demo: ");

                var choice = Console.ReadLine();

                if (choice == "0")
                {
                    exit = true;
                }
                else
                {
                    var orderInfo = new OrderInfo
                    {
                        CustomerName = "John Doe",
                        Email = "john125@gmail.com",
                        Product = "Laptop",
                        Price = 1200,
                        CreditCard = "1234567890"
                    };

                    Console.WriteLine();
                    Console.WriteLine("Order Processing:");
                    Console.WriteLine();

                    switch (choice)
                    {
                        case "1":
                            #region stage 1

                            var commerce1 = new Stage1.Commerce();
                            commerce1.ProcessOrder(orderInfo);

                            #endregion

                            break;
                        case "2":
                            #region stage 2

                            var commerce2 =
                                new Stage2.Commerce(
                                    new Stage2.BillingProcessor(),
                                    new Stage2.CustomerProcessor(
                                        new Stage2.CustomerRepository(),
                                        new Stage2.ProductRepository()),
                                    new Stage2.Notifier());

                            commerce2.ProcessOrder(orderInfo);

                            #endregion

                            break;
                        case "3":
                            #region stage 3

                            var builder = new ContainerBuilder();

                            builder.RegisterType<Stage3.Commerce>();
                            builder.RegisterType<Stage3.Notifier>().As<Stage3.INotifier>();

                            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                                .Where(t => t.Name.EndsWith("Processor") && t.Namespace.EndsWith("Stage3"))
                                .As(t => t.GetInterfaces().FirstOrDefault(
                                    i => i.Name == "I" + t.Name));

                            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                                .Where(t => t.Name.EndsWith("Repository") && t.Namespace.EndsWith("Stage3"))
                                .As(t => t.GetInterfaces().FirstOrDefault(
                                    i => i.Name == "I" + t.Name));

                            builder.RegisterType<Stage3.Logger>().As<Stage3.ILogger>();

                            container = builder.Build();

                            var commerce3 = container.Resolve<Stage3.Commerce>();

                            commerce3.ProcessOrder(orderInfo);

                            #endregion

                            break;
                    }

                    Console.WriteLine();
                    Console.WriteLine("Press 'Enter' for menu.");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }
    }
}
