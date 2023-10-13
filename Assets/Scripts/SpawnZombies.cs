using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZombie : MonoBehaviour
{
    public GameObject Zombie;
    public float SpawnTime;
    public float minSpawnTime = 5f;
    public float maxSpawnTime = 15f;
    
    void Start()
    {
        
        StartCoroutine("WaitTimeForSpawn");  
    }

    IEnumerator WaitTimeForSpawn()
    {
        while (true)
        {
            SpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(SpawnTime);

            Instantiate(Zombie.gameObject,gameObject.transform.position, Quaternion.identity);
        }
    }
        
}
