using System.Collections.Generic;
using System.Linq;
using GameEngine.Data;
using SaveSystem.Base;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameEngine.Providers
{
    public class UnitDataProvider : IDataProvider<ISavableData>, IStartable
    {
        private readonly UnitManager _unitManager;
        private readonly UnitPrefabs _unitPrefabs;

        [Inject]
        public UnitDataProvider(UnitManager unitManager, UnitPrefabs unitPrefabs)
        {
            Debug.Log("UnitDataProvider Constructor");
            _unitManager = unitManager;
            _unitPrefabs = unitPrefabs;
        }

        public void Start()
        {
            Debug.Log("UnitDataProvider Start");
            _unitManager.SetupUnits(Object.FindObjectsOfType<Unit>());
        }

        public ISavableData GetDataForSaving()
        {
            Debug.Log("UnitDataProvider GetDataForSaving");
            return ListUnitData.Mapper(_unitManager.GetAllUnits().ToList());
        }

        public void ApplyLoadedData(ISavableData data)
        {
            if (data is not ListUnitData listUnitData) return;
            
            Debug.Log("UnitDataProvider ApplyLoadedData");
            ApplyUnits(listUnitData.Units);
        }
        
        private void ApplyUnits(List<UnitData> units)
        {
            // Создаем словарь для быстрого поиска существующих юнитов по имени
            var existingUnits = _unitManager.GetAllUnits().ToDictionary(u => u.name);

            // Проходим по всем сохраненным юнитам и обновляем или создаем их
            foreach (var unitData in units)
            {
                if (existingUnits.TryGetValue(unitData.Name, out var existingUnit))
                {
                    // Если юнит существует, обновляем его свойства
                    existingUnit.HitPoints = unitData.HitPoints;
                    existingUnit.gameObject.transform.position = unitData.Position.ToVector3();
                    existingUnit.gameObject.transform.eulerAngles = unitData.Rotation.ToVector3();
                    // Удаляем из словаря, чтобы не удалить позже
                    existingUnits.Remove(unitData.Name);
                }
                else
                {
                    // Если юнит не существует, спавним его
                    var unitPrefab = _unitPrefabs.GetPrefabByType(unitData.Type);
                    if (unitPrefab != null)
                    {
                        var newUnit = _unitManager.SpawnUnit(unitPrefab, unitData.Position.ToVector3(), Quaternion.Euler(unitData.Rotation.ToVector3()));
                        newUnit.HitPoints = unitData.HitPoints; // Устанавливаем здоровье нового юнита
                    }
                    else
                    {
                        Debug.LogWarning($"Префаб юнита типа {unitData.Type} не найден.");
                    }
                }
            }

            // Теперь удаляем юниты, которые остались в existingUnits
            foreach (var unit in existingUnits.Values.ToList())
            {
                _unitManager.DestroyUnit(unit);
            }
        }
    }
}