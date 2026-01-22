using System;
using EditorAttributes;
using UnityEditor;
using UnityEngine;

namespace ItemSystem.ItemConfiguration
{
    [CreateAssetMenu(menuName = "Item/ItemData")]
    public class ItemData : ScriptableObject
    {
        
        [ReadOnly][SerializeField] private string _id;
        public string ID => _id;
        public string Name;

        [AssetPreview(100,100)] public Sprite Sprite;

        [TextArea(15,7)]
        public string Description;
        public bool IsStackable;

        protected virtual void OnValidate()
        {
// #if UNITY_EDITOR
            if (string.IsNullOrEmpty(_id))
            {
                _id = Guid.NewGuid().ToString();
                // EditorUtility.SetDirty(this);
            }
// #endif
        }
    }
}
