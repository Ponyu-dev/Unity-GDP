// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-29
// <file>: GridInventoryPresenter.cs
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using _InventorySystem.Scripts.Inventory;
using _InventorySystem.Scripts.Item;
using _InventorySystem.UI.Scripts.InventorySlot;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace _InventorySystem.UI.Scripts
{
    public sealed class GridInventoryPresenter : IStartable, IDisposable
    {
        private readonly InventorySlotView _prefabView;
        private readonly IBaseInventory _baseInventory;
        private readonly InventoryItemCatalog _inventoryItemCatalog;
        private readonly Transform _container;
        private readonly IObjectResolver _resolver;

        private readonly Dictionary<Guid, IInventorySlotPresenter> _inventorySlotPresenters;

        [Inject]
        public GridInventoryPresenter(IObjectResolver resolver, IBaseInventory baseInventory, InventoryItemCatalog inventoryItemCatalog, InventorySlotView prefabView, Transform container)
        {
            Debug.Log("[GridInventoryPresenter] Constructor");
            _resolver = resolver;
            _baseInventory = baseInventory;
            _inventoryItemCatalog = inventoryItemCatalog;
            _container = container;
            _prefabView = prefabView;

            _inventorySlotPresenters = new Dictionary<Guid, IInventorySlotPresenter>();

            _baseInventory.OnItemAdded += OnItemAdded;
            _baseInventory.OnItemStackChanged += OnItemStackChanged;
            _baseInventory.OnItemRemoved += OnItemRemoved;
        }
        
        //TODO This is a temporary solution. Since I didn't want to do both saving and loading.
        //TODO So on startup I just load the default values.
        public void Start()
        {
            foreach (var itemConfig in _inventoryItemCatalog.GetAllItems())
            {
                var item = itemConfig.Clone;
                _baseInventory.AddItem(item);
            }
        }

        private void OnItemAdded(InventoryItem item)
        {
            if (_inventorySlotPresenters.ContainsKey(item.InstanceId))
                return;
            
            if (!_resolver.TryResolve<IInventorySlotPresenter>(out var presenter))
            {
                Debug.LogError($"[GridInventoryPresenter] OnItemAdded IInventorySlotPresenter is not find!");
                return;
            }
            
            var view = Object.Instantiate(_prefabView, _container);
            presenter.Init(item, view);
            _inventorySlotPresenters.Add(item.InstanceId, presenter);
        }

        private void OnItemStackChanged(InventoryItem item)
        {
            if (_inventorySlotPresenters.TryGetValue(item.InstanceId, out var presenter))
                presenter.UpdateStack(item);
        }

        private void OnItemRemoved(InventoryItem item)
        {
            Debug.Log($"[GridInventoryPresenter] OnItemRemoved {item.InstanceId}");
            if (_inventorySlotPresenters.TryGetValue(item.InstanceId, out var presenter))
                presenter.Dispose();
            _inventorySlotPresenters.Remove(item.InstanceId);
        }

        public void Dispose()
        {
            _resolver?.Dispose();
            
            _baseInventory.OnItemAdded -= OnItemAdded;
            _baseInventory.OnItemStackChanged -= OnItemStackChanged;
            _baseInventory.OnItemRemoved -= OnItemRemoved;
        }
    }
}