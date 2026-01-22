using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnvironmentHelper : Singleton<EnvironmentHelper>
{
    public LayerMask allEntity;
    public LayerMask onlyExcludePlayer;
    public LayerMask onlyEnemy;
    public LayerMask onlyStaticObject;

    public List<EntityComponent> GetAllEnemiesNearby(Vector3 position, float range, bool includePlayer = false)
    {
        Collider[] colliders = Physics.OverlapSphere(position, range, includePlayer ? allEntity : onlyExcludePlayer);
        return colliders
            .Select<Collider, EntityComponent>(collider => collider.GetComponent<EntityComponent>())
            .Where(entity => entity is not null)
            .ToList();
    }

    public EntityComponent GetFirstOrDefaultEnemyNearby(Vector3 position, float range)
    {
        Collider[] colliders = Physics.OverlapSphere(position, range, onlyExcludePlayer);
        foreach(Collider collider in colliders)
        {
            if(collider.TryGetComponent(out EntityComponent _obj))
                return _obj;
        }
        return null;
    }
}
