using System;
using ProductionAccountingSystem.Services;

namespace ProductionAccountingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // Настройка консоли на работу с кириллицей и валютой
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Создание экземпляров менеджера учета и генератора отчетов
            var manager = new ProductionManager();
            var reportGenerator = new ReportGenerator(manager);

            // Наполнение нормативно-справочной информации (Материалы)
            var steel = manager.AddMaterial("Листовая сталь", 1500m, "кг");
            var paint = manager.AddMaterial("Порошковая краска", 450m, "л");
            var bolts = manager.AddMaterial("Болты М8", 12m, "шт");

            // Наполнение нормативно-справочной информации (Этапы техпроцесса)
            var stage1 = manager.AddStage("Штамповка", "Вырезка деталей из стали под прессом");
            var stage2 = manager.AddStage("Покраска", "Нанесение защитного полимерного слоя");
            var stage3 = manager.AddStage("Сборка", "Финишная сборка готового изделия");

            // Имитация учета: фиксация списания ресурсов на этапах производства
            manager.LogMaterialUsage(stage1.Id, steel.Id, 120.5);
            manager.LogMaterialUsage(stage1.Id, steel.Id, 85.0);
            manager.LogMaterialUsage(stage2.Id, paint.Id, 15.2);
            manager.LogMaterialUsage(stage3.Id, bolts.Id, 200);
            manager.LogMaterialUsage(stage3.Id, bolts.Id, 50);

            // Вызов подсистемы генерации отчетов по затратам
            reportGenerator.GenerateCostReportByStages();

            Console.WriteLine("\nДля завершения работы программы нажмите Enter...");
            Console.ReadLine();
        }
    }
}
