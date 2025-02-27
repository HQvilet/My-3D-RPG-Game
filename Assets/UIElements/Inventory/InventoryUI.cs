using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject SlotPref;
    [SerializeField] private Button openCloseButton;

    public List<SlotUnit> Slots = new List<SlotUnit>(InventoryData.INVENTORY_CAPACITY);

    public bool IsHoverUI;

    public void Start()
    {
        openCloseButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(!gameObject.activeSelf);
            // Time.timeScale = gameObject.activeSelf ? 0 : 1;
        });
        int i = 0;
        foreach(ItemStack item in InventoryManager.Instance.inventoryData.Items)
        {
            var slot = Instantiate(SlotPref ,transform).GetComponent<SlotUnit>();
            slot.SetSlotData(item);
            
            slot.index = i++;
            Slots.Add(slot);
        }
    }

    public void Update()
    {
        IsHoverUI = EventSystem.current.IsPointerOverGameObject();
        GetHoveringUIElements();
    }
    
    List<RaycastResult> raycastResults = new List<RaycastResult>();
    public void GetHoveringUIElements()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        
        EventSystem.current.RaycastAll(pointerEventData ,raycastResults);
        foreach(RaycastResult castInfo in raycastResults)
        {
            if(castInfo.gameObject.TryGetComponent(out SlotUnit slot))
            {
                slot.ColorTest();
            }
        }

    }

}
