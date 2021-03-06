using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]

    [SerializeField] float health = 100f;
    [SerializeField] int enemyScore = 150;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float deathVolume = 0.75f;

    [Header("Projectile")]
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject laserPrefab;   
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootVolume = 0.25f;





    // Start is called before the first frame update
    void Start()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
       shotCounter-=Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(
                   laserPrefab,
                   transform.position,
                   Quaternion.identity) as GameObject;
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position,shootVolume);
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            if (damageDealer != null)
            {
                Destroy(other.gameObject);
                ProcessHit(damageDealer);
            }
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(
               deathVFX,
               transform.position,
               transform.rotation) as GameObject;
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathVolume);
        Destroy(explosion, durationOfExplosion);
        FindObjectOfType<GameSession>().AddScore(enemyScore);
    }
}
