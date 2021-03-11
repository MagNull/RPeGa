using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;

namespace InventoryScripts
{
    public class Slot : MonoBehaviour, IComparable
    {
        public int Index = 0;
        public Image ItemImage;
        public Text ItemName;
        [SerializeField] private Transform _itemPanelSpawnPoint;
        [SerializeField] private float _throwForce = 1;
        [SerializeField] private Item _item;
        private Inventory _inventory;
        private Button _button;
        private ItemPanel _itemPanel;

        [Inject]
        public void Construct(Inventory inventory, ItemPanel itemPanel)
        {
            _inventory = inventory;
            _itemPanel = itemPanel;
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(ShowItemPanel);
            _inventory.AddSlot(this);
            ItemName.text = $"Slot {Index}";
        }

        public void SetItem(Item item)
        {
            _item = item;
            _item.TakeItem(this);
            ItemName.text = item.Name;
            ItemImage.gameObject.SetActive(true);
            ItemImage.sprite = item.Image;
        }

        public void DeleteItem()
        {
            _item = null;
            ItemName.text = $"Slot {Index}";
            ItemImage.gameObject.SetActive(false);
        }

        private void ShowItemPanel()
        {
            _itemPanel.transform.position = _itemPanelSpawnPoint.position;
            _itemPanel.gameObject.SetActive(true);
            _itemPanel.UnbindButtons();
            _itemPanel.BindButtons(Use, DropItem); 
        }

        private void Use() => _item?.Use();
        
        private void DropItem()
        {
            if (!(_item is null))
            {
                _item.transform.position = _inventory.transform.position + _inventory.transform.forward * 2;
                _item.ThrowOutItem(_inventory.transform.forward, _throwForce);
                _item.gameObject.SetActive(true);
                _inventory.DeleteItem(_item);
                DeleteItem();
            }
        }
        

        public int CompareTo(object obj)
        {
            return Index.CompareTo(((Slot) obj).Index);
        }
    }
}
