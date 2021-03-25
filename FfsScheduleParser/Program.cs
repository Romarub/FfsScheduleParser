using System;
using System.Configuration;
using System.Linq;
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
                var documentService = scope.Resolve<IDocumentsService>();
                documentService.WriteStatistic(
                    workingShift.Statistic,
                    ConfigurationManager.AppSettings.Get("CsvStatisticFilePath"));
                var simulators = ConfigurationManager.AppSettings["Simulators"]?.Split(',');
                if (simulators == null)
                    throw new NullReferenceException("No simulators found in config file");
                foreach (var simulator in simulators)
                {
                    var fileName = ConfigurationManager.AppSettings.Get(simulator) + "LogsFilePath";
                    documentService.WriteLogs(
                        workingShift.ShiftLogs.Where(log => log.Simulator == simulator),
                        ConfigurationManager.AppSettings.Get(fileName));
                }
            }
        }

        private static void RegisterServices()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<WebClientWrapper>().As<IWebClientWrapper>();
            builder.RegisterType<DocumentsToScvWriter>().As<IDocumentsWriter>();
            builder.RegisterType<TrainingSessionsService>().As<ITrainingSessionsService>();
            builder.RegisterType<DocumentsService>().As<IDocumentsService>();
            builder.RegisterType<WorkingShiftService>().As<IWorkingShiftService>();
            Container = builder.Build();
        }
    }
}
