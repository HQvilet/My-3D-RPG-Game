using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityComponentSystem : Singleton<EntityComponentSystem>
{
    [SerializeField] EntityComponent playerComponent;
    public EntityComponent GetPlayerComponent() => playerComponent;

    protected override void Awake()
    {
        base.Awake();
        
    }

    void Start()
    {
        playerComponent = Player.Instance.GetComponent<EntityComponent>();
    }




}
