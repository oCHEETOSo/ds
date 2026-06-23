using System;

namespace ProductionAccountingSystem.Models
{
    // Класс-справочник для описания этапов технологического процесса
    public class ProductionStage
    {
        // Уникальный идентификатор производственного этапа
        public int Id { get; set; }
        
        // Название цеха или этапа (например, "Штамповка")
        public string Name { get; set; } = string.Empty;
        
        // Подробное описание выполняемых на этапе технологических операций
        public string Description { get; set; } = string.Empty;
    }
}
