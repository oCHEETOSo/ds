using System;

namespace ProductionAccountingSystem.Models
{
    // Класс для фиксации фактического использования сырья на производстве
    public class MaterialLog
    {
        // Уникальный номер транзакции списания
        public int Id { get; set; }
        
        // Идентификатор этапа (связь с ProductionStage)
        public int StageId { get; set; }
        
        // Идентификатор материала (связь с Material)
        public int MaterialId { get; set; }
        
        // Фактическое количество израсходованного сырья
        public double Quantity { get; set; }
        
        // Точная дата и время проведения технологической операции
        public DateTime Timestamp { get; set; }
    }
}
