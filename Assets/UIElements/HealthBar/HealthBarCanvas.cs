using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarCanvas : MonoBehaviour
{
    public static HealthBarCanvas Instance{get; private set;}

    private void Awake()
    {
        Instance = this;
    }
}
