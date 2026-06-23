using System;
using System.Linq;
using ProductionAccountingSystem.Services;

namespace ProductionAccountingSystem.Services
{
    // Класс, отвечающий за калькуляцию затрат и построение отчетов
    public class ReportGenerator
    {
        private readonly ProductionManager _manager;

        // Внедрение зависимости через конструктор класса
        public ReportGenerator(ProductionManager manager)
        {
            _manager = manager;
        }

        // Генерация комплексного отчета по производственным расходам предприятия
        public void GenerateCostReportByStages()
        {
            Console.WriteLine("\n=======================================================");
            Console.WriteLine("ОТЧЕТ ПО ПРОИЗВОДСТВЕННЫМ РАСХОДАМ (ПО ЭТАПАМ)");
            Console.WriteLine("=======================================================");

            var logs = _manager.GetLogs();
            var stages = _manager.GetStages();

            // Пункт 5.2: Перебор этапов и фильтрация данных с помощью LINQ
            foreach (var stage in stages)
            {
                Console.WriteLine($"\nЭтап: {stage.Name} ({stage.Description})");
                Console.WriteLine(new string('-', 55));
                
                // Пункт 5.3: Настройка ровной табличной сетки в окне консоли
                Console.WriteLine($"{"Материал",-20} | {"Кол-во",-10} | {"Цена/ед",-10} | {"Сумма",-10}");
                Console.WriteLine(new string('-', 55));

                // Использование LINQ для фильтрации логов текущего этапа
                var stageLogs = logs.Where(l => l.StageId == stage.Id);
                decimal stageTotalCost = 0;

                foreach (var log in stageLogs)
                {
                    var material = _manager.GetMaterialById(log.MaterialId);
                    if (material == null) continue;

                    // Математический расчет стоимости списанной позиции сырья
                    decimal cost = (decimal)log.Quantity * material.UnitPrice;
                    stageTotalCost += cost;

                    // Форматированный вывод строки данных с выравниванием по левому краю
                    Console.WriteLine($"{material.Name,-20} | {log.Quantity,-6} {material.Unit,-3} | {material.UnitPrice,-10:C} | {cost,-10:C}");
                }

                Console.WriteLine(new string('-', 55));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Итого по этапу: {stageTotalCost:C}");
                Console.ResetColor();
            }

            // Агрегация общих затрат по всей информационной системе
            decimal totalProductionCost = logs.Sum(log => 
                (decimal)log.Quantity * (_manager.GetMaterialById(log.MaterialId)?.UnitPrice ?? 0));
            
            Console.WriteLine("\n=======================================================");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"ОБЩИЕ РАСХОДЫ НА ПРОИЗВОДСТВО: {totalProductionCost:C}");
            Console.ResetColor();
            Console.WriteLine("=======================================================");
        }
    }
}
