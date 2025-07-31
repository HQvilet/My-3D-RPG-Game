// using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using UnityEngine;
public static class MyUtils
{
    static System.Random rnd = new();
    public static Vector3 VectorTranslate(Vector2 _vec2, float height = 0f)
    {
        return new Vector3(_vec2.x, height, _vec2.y);
    }
    public static Vector2 VectorTranslate(Vector3 _vec3, float height = 0f)
    {
        return new Vector2(_vec3.x, _vec3.z);
    }
    public static float VectorToEulerAngle(Vector2 direction)
    {
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90; ;
    }

    public static Vector2 EulerAngleToVector(float angle)
    {
        angle = (angle + 90) * Mathf.Deg2Rad;

        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    public static Vector3 RandomizeVector3()
    {

        return new Vector3(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
    }

    public static Vector3 ModifiyVector(Vector3 vector, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
    }
}

// public class MyCustomLerp : MonoBehaviour
// {
//     public float time;
//     public float TimeValue(Func<float ,float> myFunc)
//     {
//         return myFunc(time);
//     }

//     public async Task StartTimer()
//     {
//         time = 0;
//         while(time < 3)
//             time += Time.deltaTime;
//     }
// }