using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHitbox : DamageUnit
{
    public float multiplier = 0.1f;
    public int collideDurability;

    public GameObject _contactVFX;
    public GameObject _inPlaceContact;

    void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<EntityComponent>(out EntityComponent entity))
        {
            if(DamageHandler.CanDamageThisEntity(sourceDamage, entity))
            {
                entity.effectModifier.GetDamage(sourceDamage.characterStats.Atk * multiplier, sourceDamage, DmgType.PHYSIC);

                if(_contactVFX)
                    Instantiate(_contactVFX, transform.position, Quaternion.identity)
                        .GetComponent<DamageUnit>()
                        ?.SetDamageData(sourceDamage);

                if(_inPlaceContact)
                {
                    Instantiate(_inPlaceContact, entity.transform.position, Quaternion.identity)
                        .GetComponent<DamageUnit>()
                        ?.SetDamageData(sourceDamage);
                }

                --collideDurability;
                if(collideDurability <= 0)
                {
                    Destroy(this.gameObject);
                }
            }
        }
        else
        {
            // DamageUnit dmgUnit = Instantiate(_destroyVFX, transform.position, Quaternion.identity).GetComponent<DamageUnit>();
            // if(dmgUnit)
            //     dmgUnit.SetDamageData(sourceDamage);
            // Destroy(this.gameObject);
        }
    }
}
