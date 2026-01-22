using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 20f;
    public bool rotateOnDirection = true;
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }
}
