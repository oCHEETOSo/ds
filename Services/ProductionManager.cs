using System;
using System.Collections.Generic;
using System.Linq;
using ProductionAccountingSystem.Models;

namespace ProductionAccountingSystem.Services
{
    // Сервисный класс, реализующий бизнес-логику и хранение данных в памяти
    public class ProductionManager
    {
        // Внутренние коллекции (репозитории) для хранения объектов
        private readonly List<Material> _materials = new();
        private readonly List<ProductionStage> _stages = new();
        private readonly List<MaterialLog> _logs = new();

        // Автоинкрементные счетчики для генерации уникальных ID
        private int _materialIdCounter = 1;
        private int _stageIdCounter = 1;
        private int _logIdCounter = 1;

        // Метод динамического добавления записей в справочник материалов
        public Material AddMaterial(string name, decimal unitPrice, string unit)
        {
            var material = new Material 
            { 
                Id = _materialIdCounter++, 
                Name = name, 
                UnitPrice = unitPrice, 
                Unit = unit 
            };
            _materials.Add(material);
            return material;
        }

        public IEnumerable<Material> GetMaterials() => _materials;
        public Material? GetMaterialById(int id) => _materials.FirstOrDefault(m => m.Id == id);

        // Метод динамического добавления технологических этапов
        public ProductionStage AddStage(string name, string description)
        {
            var stage = new ProductionStage 
            { 
                Id = _stageIdCounter++, 
                Name = name, 
                Description = description 
            };
            _stages.Add(stage);
            return stage;
        }

        public IEnumerable<ProductionStage> GetStages() => _stages;
        public ProductionStage? GetStageById(int id) => _stages.FirstOrDefault(s => s.Id == id);

        // Пункт 4.2: Учет расхода с обязательной валидацией связей
        public bool LogMaterialUsage(int stageId, int materialId, double quantity)
        {
            // Проверка: существуют ли в справочниках такие ID этапа и материала
            if (GetStageById(stageId) == null || GetMaterialById(materialId) == null)
            {
                return false; // Блокировка записи при некорректных ID
            }

            // Добавление проверенной транзакции в лог списания ресурсов
            _logs.Add(new MaterialLog
            {
                Id = _logIdCounter++,
                StageId = stageId,
                MaterialId = materialId,
                Quantity = quantity,
                Timestamp = DateTime.Now
            });
            return true;
        }

        public IEnumerable<MaterialLog> GetLogs() => _logs;
    }
}
