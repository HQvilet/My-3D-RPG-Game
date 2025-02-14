using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour 
{

    public static Player Instance{get; set;}


    private void Awake(){
        Instance = this;
    }

    private void Update()
    {
        
    }
    private void FixedUpdate()
    {

    }
}