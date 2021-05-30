using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] GameObject deathVFX;
    GameObject explosionParent;
    const string EXPLOSIONS_PARENT_NAME = "Explosions";


    private void Start()
    {
        CreateExplosionParent();
    }

    private void CreateExplosionParent()
    {
        explosionParent = GameObject.Find(EXPLOSIONS_PARENT_NAME);
        if (!explosionParent)
        {
            explosionParent = new GameObject(EXPLOSIONS_PARENT_NAME);
        }
    }

    public void DealDamage(float damageTaken)
    {
        if (GetComponent<Attacker>())
        {
            damageTaken /=(float) PlayerPrefsController.GetDifficulty();
        }
         health -= damageTaken;
         if (health <= 0)
            {
            TriggerDeathVFX();
            Destroy(this.gameObject);
            }
        
    }

    private void TriggerDeathVFX()
    {
        if (!deathVFX) { return;}
        GameObject explosion = Instantiate(
            deathVFX,
             transform.position,
             transform.rotation) as GameObject;
        explosion.transform.parent= explosionParent.transform;
        Destroy(explosion,1f);
    }
   
}
