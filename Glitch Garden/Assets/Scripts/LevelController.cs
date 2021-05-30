using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] GameObject winLabel,loseCanvas;
    int numberOfAttackers = 0;
    bool levelTimerFinished = false;
    [SerializeField] float waitToLoad = 5f;

    private void Start()
    {
        winLabel.SetActive(false);
        loseCanvas.SetActive(false);
    }

    public bool GetLevelTimerFinished()
    {
        return levelTimerFinished;
    }

    public void AttackerSpawned()
    {
            numberOfAttackers++;
    }

    public void AttackerKilled()
    {
        numberOfAttackers--;
        if(numberOfAttackers<=0 && levelTimerFinished)
        {
            StartCoroutine(HandleWinCondition());
        }
    }

    IEnumerator HandleWinCondition()
    {


        
        winLabel.SetActive(true);
        GetComponent<AudioSource>().Play();
        
        yield return new WaitForSeconds(waitToLoad);
        FindObjectOfType<LevelLoader>().LoadNextScene();
    }

    public void LevelTimerFinished()
    {
        levelTimerFinished = true;
        StopSpawners();
    }

    private void StopSpawners()
    {
        AttackerSpawner[] spawnerArray = FindObjectsOfType<AttackerSpawner>();
        foreach(AttackerSpawner spawner in spawnerArray)
        {
            spawner.StopSpawning();
        }
    }

    public void HandleLoseConditions()
    {
        loseCanvas.SetActive(true);
        Time.timeScale = 0;
    }

  
}
