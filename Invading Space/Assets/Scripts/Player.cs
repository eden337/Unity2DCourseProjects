using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Cofniguration params
    [Header("Player")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float padding = 1f;
    [SerializeField] private int health = 200;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0,1)]float deathVolume = 0.75f;

    [Header("Projectile")]
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileFiringPeriod = 1f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootVolume = 0.25f;

    private Coroutine firingCorouting;

    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundries();
        
    }
   
    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if(Input.GetButtonDown("Fire1"))
        {
           firingCorouting = StartCoroutine(FireContinuosly());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCorouting);
        }
    }

    IEnumerator FireContinuosly()
    {
        while (true)
        {
            GameObject laser = Instantiate(
                   laserPrefab,
                   transform.position,
                   Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX,xMin,xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY,yMin,yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            if (damageDealer != null)
            {
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
        health = 0;
        Destroy(gameObject);
        GameObject explosion = Instantiate(
                           deathVFX,
                           transform.position,
                           transform.rotation) as GameObject;
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position,deathVolume);
        Destroy(explosion, durationOfExplosion);
        FindObjectOfType<Level>().LoadGameOver();
    }

    private void SetUpMoveBoundries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    public int GetHealth()
    {
        return health;
    }

}
