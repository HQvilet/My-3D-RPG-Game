using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace ItemSystem.ItemConfiguration
{
    public class ItemPoolManager : Singleton<ItemPoolManager>
    {
        // [SerializeField] List<ItemData> RegistryItems;
        [SerializeField] private string ITEMS_DATA_PATH;

        private Dictionary<string, ItemData> ItemsData = new();
        private Dictionary<string, ArmourReference> ArmourRefs = new();

        protected override void Awake()
        {
            base.Awake();
            LoadAll(ITEMS_DATA_PATH);
        }

        public ItemData GetItemByID(string ID)
        {
            if (ItemsData.ContainsKey(ID))
                return ItemsData[ID];
            Debug.Log("Non existing item ID :" + ID);
            return null;
        }

        public bool TryGetItemByID(string _id, out ItemData item) => ItemsData.TryGetValue(_id, out item);

        private void LoadAll(string DATA_PATH)
        {
            ItemData[] _items = Resources.LoadAll<ItemData>(DATA_PATH);
            foreach (ItemData item in _items)
            {
                if (!ItemsData.ContainsKey(item.ID))
                    ItemsData.Add(item.ID, item);
                else
                {
                    Debug.Log("Exist 2 item with the same ID :" + item.ID + " - " + item.Name);
                }
            }

            ArmourReference[] _armours = Resources.LoadAll<ArmourReference>("Armour");
            foreach (ArmourReference item in _armours)
            {
                if (!ItemsData.ContainsKey(item.ID))
                    ArmourRefs.Add(item.ID, item);
                else
                {
                    Debug.Log("Exist 2 item with the same ID :" + item.ID);
                }
            }
        }

        public ArmourReference GetArmourReferenceByID(string ID)
        {
            if (ArmourRefs.ContainsKey(ID))
                return ArmourRefs[ID];
            Debug.Log("Non existing item ID :" + ID);
            return null;
        }
    }
}
