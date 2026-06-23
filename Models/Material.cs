using System;

namespace ProductionAccountingSystem.Models
{
    // Класс-справочник для учета номенклатуры материалов
    public class Material
    {
        // Уникальный идентификатор материала
        public int Id { get; set; }
        
        // Наименование материального ресурса
        public string Name { get; set; } = string.Empty;
        
        // Финансовая стоимость за единицу (используется высокоточный decimal)
        public decimal UnitPrice { get; set; } 
        
        // Единица измерения базового ресурса (кг, л, шт)
        public string Unit { get; set; } = "шт."; 
    }
}
