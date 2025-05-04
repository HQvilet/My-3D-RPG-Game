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

        private Dictionary<int ,ItemData> ItemsData = new Dictionary<int ,ItemData>();

        // public void OnValidate()
        // {
        //     // Resources.LoadAll<ItemDataSO>("dasd");
        //     foreach(ItemData item in RegistryItems)
        //     {
        //         if(!ItemsData.ContainsKey(item.ID))
        //             ItemsData.Add(item.ID ,item);
        //         else
        //             Debug.Log(item.Name);
        //     }
        // }

        protected override void Awake()
        {
            base.Awake();
            LoadAll(ITEMS_DATA_PATH);
        }

        public ItemData GetItemByID(int ID)
        {
            if(ItemsData.ContainsKey(ID))
                return ItemsData[ID];
            Debug.Log("Non existing item ID :" + ID);
            return null;
        }

        public bool TryGetItemByID(int _id ,out ItemData item) =>  ItemsData.TryGetValue(_id ,out item);

        private void LoadAll(string DATA_PATH)
        {
            ItemData[] Items = Resources.LoadAll<ItemData>(DATA_PATH);
            foreach(ItemData item in Items)
            {
                if(!ItemsData.ContainsKey(item.ID))
                    ItemsData.Add(item.ID ,item);
                else
                {
                    Debug.Log("Exist 2 item with the same ID :" + item.ID + " - " + item.Name);
                }
            }
        }
    }
}
