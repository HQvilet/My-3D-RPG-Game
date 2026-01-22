using System.Collections;
using System.Collections.Generic;
using ItemSystem.ItemConfiguration;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemDescription : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemNameTextMesh;
    [SerializeField] Image itemSprite;
    [SerializeField] TextMeshProUGUI itemDescriptionTextMesh;
    
    public void SetItemDisplayData(ItemData item)
    {
        if(item == null)
        {
            itemSprite.color = new Color(1,1,1,0);
            itemDescriptionTextMesh.text = string.Empty;
            itemNameTextMesh.text = string.Empty;
            return;
        }
        itemSprite.sprite = item.Sprite;
        itemDescriptionTextMesh.text = item.Description;
        itemNameTextMesh.text = item.Name;
    }
}
