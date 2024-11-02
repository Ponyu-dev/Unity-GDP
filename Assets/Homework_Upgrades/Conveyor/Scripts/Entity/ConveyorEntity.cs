using Entities;
using UnityEngine;

namespace Homework_Upgrades.Conveyor.Scripts.Entity
{
    public class ConveyorEntity : MonoEntityBase
    {
        [SerializeField] private ConveyorModel _model;
        
        private void Awake()
        {
            //Add(new ConveyorStorageComponent(_model.LoadStorageCapacity));            
            //Add(new ConveyorUnloadStorageComponent(_model.UnloadStorageCapacity));            
            //Add(new ConveyorProduceTimeComponent(_model.ProduceTime));            
        }
    }
}