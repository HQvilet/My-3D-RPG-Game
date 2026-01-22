using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MEC;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class DamageVisualization : MonoBehaviour
{
    [SerializeField] Transform damageText;
    [SerializeField] AnimationCurve sizeOverLifeTime;
    [SerializeField] AnimationCurve alphaOverLifeTime;
    [SerializeField] float randomPositionRange;
    [SerializeField] List<Color> colorRange;

    Queue<TextMeshProUGUI> textMeshPool = new(20);
    Vector3 sizeProjection = new Vector3(-1,1,1);
    public void CreateVisualizeDamage(Vector3 position ,float damage, DmgType type)
    {
        if(damage <= 0)
            return;
        
        var textMesh = CreateDamageTextMesh(position + Vector3.up * 1.3f + Random.insideUnitSphere * randomPositionRange, Quaternion.identity);
        textMesh.SetText((type == DmgType.HEAL ? "+": "") + Mathf.Ceil(damage).ToString());
        textMesh.color = DamageTypeToColor(type);
        float startSize = Random.Range(0.8f, 1.2f);
        float startSpeed = Random.Range(0.009f, 0.013f);
        Timing.RunCoroutine(MyUtils.ProgressTickToAction(1f,
            (t) =>
            {
                textMesh.transform.position += Vector3.up * 0.01f;
                textMesh.transform.LookAt(CameraCaching.Instance.mainCamera.transform, Vector3.up);
                textMesh.transform.localScale = sizeProjection * sizeOverLifeTime.Evaluate(t) * startSize;
            },
            onFinish: () =>
            {
                AddToPool(textMesh);
            }
        ).CancelWith(gameObject));
    }

    Color DamageTypeToColor(DmgType type)
    {
        switch (type)
        {
            case DmgType.HEAL:
                return colorRange[0];
            case DmgType.PHYSIC:
                return colorRange[1];
            case DmgType.MAGIC:
                return colorRange[2];
            default:
                return Color.white;
        }
    }

    void AddToPool(TextMeshProUGUI textMesh)
    {
        textMesh.gameObject.SetActive(false);
        textMeshPool.Enqueue(textMesh);
    }

    TextMeshProUGUI CreateDamageTextMesh(Vector3 position, Quaternion quaternion)
    {
        if(textMeshPool.Count <= 0)
        {
            var textMesh = Instantiate(damageText ,position + Random.insideUnitSphere * randomPositionRange ,Quaternion.identity ,this.transform).GetComponent<TextMeshProUGUI>();
            AddToPool(textMesh);
        }
        TextMeshProUGUI _poolingTextMesh = textMeshPool.Dequeue();
        _poolingTextMesh.transform.position = position;
        _poolingTextMesh.gameObject.SetActive(true);
        return _poolingTextMesh;
    }

}
