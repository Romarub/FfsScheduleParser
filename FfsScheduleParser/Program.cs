using System;
using System.Configuration;
using Autofac;
using FfsScheduleParser.Infrastructure;
using FfsScheduleParser.Services;

namespace FfsScheduleParser
{
    class Program
    {
        private static IContainer Container { get; set; }
        static void Main()
        {
            RegisterServices();
            using (var scope = Container.BeginLifetimeScope())
            {
                var workingShift = scope.Resolve<IWorkingShiftService>().CreateShiftWithConfigParams(DateTime.Today);
                scope.Resolve<IStatisticService>().WriteStatisticToCsv(
                    workingShift.Statistic,
                    ConfigurationManager.AppSettings.Get("CsvFilePath"));
            }
        }

        private static void RegisterServices()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<WebClientWrapper>().As<IWebClientWrapper>();
            builder.RegisterType<TrainingSessionsService>().As<ITrainingSessionsService>();
            builder.RegisterType<StatisticService>().As<IStatisticService>();
            builder.RegisterType<WorkingShiftService>().As<IWorkingShiftService>();
            Container = builder.Build();
        }
    }
}
