using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHitBox : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "TestNPC")
            Debug.Log("hit");
    }

}
