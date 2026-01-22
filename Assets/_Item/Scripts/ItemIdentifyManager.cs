using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


namespace ItemSystem.ItemConfiguration
{
    public class ItemIdentifyManager : Singleton<ItemIdentifyManager>
    {
        // [SerializeField] List<ItemData> RegistryItems;
        [SerializeField] private string ITEMS_DATA_PATH;
        [SerializeField] private string ABILITIES_DATA_PATH;
        [SerializeField] private string WEAPONS_DATA_PATH;

        private Dictionary<string, ItemData> ItemsData = new();
        private Dictionary<string, ArmourReference> ArmourRefs = new();
        private Dictionary<int ,WeaponRef> WeaponHolder = new Dictionary<int, WeaponRef>();

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
            LoadAll();
        }

        public ItemData GetItemByID(string ID)
        {
            if(string.IsNullOrEmpty(ID))
                return null;

            if (ItemsData.ContainsKey(ID))
                return ItemsData[ID];
                
            Debug.Log("Non existing item ID :" + ID);
            return null;
        }

        public bool TryGetItemByID(string _id, out ItemData item) => ItemsData.TryGetValue(_id, out item);

        private void LoadAll()
        {
            ItemData[] _items = Resources.LoadAll<ItemData>(ITEMS_DATA_PATH);
            foreach (ItemData item in _items)
            {
                if (!ItemsData.ContainsKey(item.ID))
                    ItemsData.Add(item.ID, item);
                else
                {
                    Debug.Log("Exist 2 item with the same ID :" + item.ID + " - " + item.Name);
                }
            }

            ItemData[] _abilities = Resources.LoadAll<ItemData>(ABILITIES_DATA_PATH);
            foreach (ItemData item in _abilities)
            {
                if (!ItemsData.ContainsKey(item.ID))
                    ItemsData.Add(item.ID, item);
                else
                {
                    Debug.Log("Exist 2 item with the same ID :" + item.ID + " - " + item.Name);
                }
            }

            WeaponRef[] WeaponData = Resources.LoadAll<WeaponRef>(WEAPONS_DATA_PATH);
            foreach(WeaponRef weaponRef in WeaponData)
            {
                if(!WeaponHolder.ContainsKey(weaponRef.Id))
                    WeaponHolder.Add(weaponRef.Id ,weaponRef);
                else
                {
                    Debug.Log("Exist 2 weapon with the same ID :" + weaponRef.Id + " - " + weaponRef.Name);
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

        public WeaponRef GetWeaponFromId(int _id) 
        {
            if(WeaponHolder.ContainsKey(_id))
                return WeaponHolder[_id];

            Debug.Log("Non existing weapon ID reference");
            return null;
        }

        public bool TryGetWeaponFromId(int _id ,out WeaponRef weaponRef)
        {
            return WeaponHolder.TryGetValue(_id ,out weaponRef);
        }

        public List<ItemData> GetAllNormalItem() => ItemsData.Values.ToList();
    }
}
