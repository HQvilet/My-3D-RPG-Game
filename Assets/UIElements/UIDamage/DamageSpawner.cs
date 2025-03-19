using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageSpawner : Singleton<DamageSpawner>
{
    [SerializeField]  Transform damageText;

    public void VisualizeDamage(Vector3 position ,float damage)
    {
        // Vector3 a = CameraCaching.mainCamera.WorldToScreenPoint(position);
        var o = Instantiate(damageText ,position ,Quaternion.identity ,this.transform);
        o.GetComponent<TextMeshProUGUI>().SetText(damage.ToString());
        // o.transform.LookAt(CameraCaching.mainCamera.transform);

        Destroy(o.gameObject ,1f);
    }
}
