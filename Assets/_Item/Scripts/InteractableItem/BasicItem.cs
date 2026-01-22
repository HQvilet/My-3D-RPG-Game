using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using ItemSystem.ItemConfiguration;
using MEC;
using Unity.VisualScripting;
using UnityEngine;

public class BasicItem : MonoBehaviour
{
    public ItemData _itemData;
    [SerializeField] bool rotate;
    [SerializeField] float aliveTime = 0.7f;

    void Start()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = _itemData.Sprite;
        transform.DORotate(new Vector3(0, 360, 0), 8f, RotateMode.WorldAxisAdd)
            .SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear);

        Collider collider = GetComponent<Collider>();
        collider.enabled = false;
        Timing.RunCoroutine(MyUtils.WaitToAction(aliveTime, () =>
        {
            collider.enabled = true;
        }));
    }

    public void SetData(ItemData data)
    {
        _itemData = data;
    }

    private void OnDestroy() 
    {
        transform.DOKill();
    }

}
