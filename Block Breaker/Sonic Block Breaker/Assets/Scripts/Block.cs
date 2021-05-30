using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //config params
    [SerializeField] private AudioClip[] blockSounds;
    [SerializeField] private GameObject blockSparklesVFX;
    [SerializeField] private Sprite[] hitSprites;
    [SerializeField] private float blockVolume=0f;
    
    //cashed reference
    private Level level;
    private GameSession gameStatus;

    //state variables
    [SerializeField] int timesHit;

    private void Start()
    {

        if (tag == "Breakable")
        {
            level = FindObjectOfType<Level>();
            level.CountBlocks();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            HandleHit();
        }

        else
        {
            AudioSource.PlayClipAtPoint(blockSounds[1], Camera.main.transform.position,blockVolume);
        }
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            showNextHitSprite();
        }
    }

    private void showNextHitSprite()
    {
        int spriteIndex = timesHit-1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite is missing from array");
            Debug.LogError("The problemetic game object is "+ gameObject.name);
        }
    }

    private void DestroyBlock()
    {
        gameStatus = FindObjectOfType<GameSession>();
        AudioSource.PlayClipAtPoint(blockSounds[0], Camera.main.transform.position);
        level.BlockDestroyed();
        gameStatus.AddToScore();
        TriggerSparkleVFX();
        Destroy(gameObject);
    }

    private void TriggerSparkleVFX()
    {
        GameObject sparkeles = Instantiate(blockSparklesVFX,transform.position,transform.rotation);
        Destroy(sparkeles, 1f);
    }


}
