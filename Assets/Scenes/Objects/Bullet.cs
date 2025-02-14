using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Rigidbody rb;
    [SerializeField] private float speed = 1;

    private void Awake(){
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        rb.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    private void  OnTriggerEnter(Collider collider){
        if(collider.gameObject.TryGetComponent(out Enemy enemy)){
            enemy.DealDamage(1f);
        }

    }

}
