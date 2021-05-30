using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    bool spawn = true;

    [SerializeField] float minSpawnDelay=1f;
    [SerializeField] float maxSpawnDelay=5f;
    [SerializeField] Attacker[] attackerPrefabs;
    float rangeOfSpawnDelay;

    IEnumerator Start()
    {

        while (spawn)
        {
            rangeOfSpawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(rangeOfSpawnDelay);
            SpawnAttacker();
        }
    }

    public void StopSpawning()
    {
        spawn = false;
    }


    private void Spawn(int attackerIndex)
    {
        Attacker newAttacker = Instantiate
           (attackerPrefabs[attackerIndex], transform.position, transform.rotation)
           as Attacker;
        newAttacker.transform.parent = transform;
    }

    private void SpawnAttacker()
    {
        bool gameTimerFinished = FindObjectOfType<LevelController>().GetLevelTimerFinished();
        if (!gameTimerFinished)
        {
            Spawn(Random.Range(0, attackerPrefabs.Length));
        }
       
    }

}
