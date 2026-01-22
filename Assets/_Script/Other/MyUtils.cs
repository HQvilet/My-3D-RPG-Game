using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using MEC;
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

        return new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }

    public static Vector3 ModifyVector(Vector3 vector, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
    }

    public static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    public static float GetDistance(Vector3 from, Vector3 to)
    {
        return (from - to).magnitude;
    }

    public static float GetDistance(Transform from, Transform to)
    {
        return (from.position - to.position).magnitude;
    }

    public static Vector3 GetPlanePosition(Vector3 vector) => new Vector3(vector.x, 0, vector.z);

    public static Vector3 GetPlaneDirection(Vector3 from, Vector3 to) => new Vector3(to.x - from.x, 0, to.z - from.z);

    public static void SmoothLerp()
    {
        float time = 3f;
        float t = 0f;
        t += Time.deltaTime;
        Vector3.Lerp(Vector3.zero, Vector3.one, t/time);
    }

    public static IEnumerator<float> WaitToAction(float duration, System.Action action)
    {
        yield return Timing.WaitForSeconds(duration);
        action.Invoke();
    }

    public static IEnumerator<float> ProgressTickToAction(float duration, System.Action<float> action, System.Action onFinish = null)
    {
        float t = 0f;
        while(t <= duration)
        {
            t += Time.deltaTime;
            action.Invoke(t/duration);
            yield return 0; 
        }
        onFinish?.Invoke();
    }

    public static T ListRandChoice<T>(List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }

}
