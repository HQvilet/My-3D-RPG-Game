using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent : MonoBehaviour
{   
    public event EventHandler<OnEventsEnterDetection> OnPlayerEnter;
    public class OnEventsEnterDetection :EventArgs 
    {
        public Vector3 d_vector;
    }

    void Start()
    {
        OnPlayerEnter += Angl;
    }

    private void Angl(object sender, OnEventsEnterDetection e)
    {
        Debug.Log(e.d_vector);
        Debug.Log(sender);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider){
        OnPlayerEnter?.Invoke(this ,new OnEventsEnterDetection{
            d_vector = collider.transform.position
        });
    }
    

}
