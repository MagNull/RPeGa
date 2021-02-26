using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace InventoryScripts
{
    public class Slot : MonoBehaviour, IComparable
    {
        public int Index = 0;
        public Image ItemImage;
        public Text ItemName;
        [SerializeField] private Item _item;
        private Inventory _inventory;

        [Inject]
        public void Construct(Inventory inventory)
        {
            _inventory = inventory;
        }

        private void Awake()
        {
            _inventory.AddSlot(this);
            ItemName.text = $"Slot {Index}";
        }

        public void SetItem(Item item)
        {
            _item = item;
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

        public void Use() => _item?.Use();

        public int CompareTo(object obj)
        {
            return Index.CompareTo(((Slot) obj).Index);
        }
    }
}
