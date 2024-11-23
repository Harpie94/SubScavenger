using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("2 Limits Max")]
    [Header("Items will spawn between the 2 GameObjects below:")]
    public GameObject[] itemSpawnLimits;
    

    [Space(20)]
    [Header("2 Limits Max")]
    [Header("Fish will spawn between the 2 GameObjects below:")]
    public GameObject[] fishSpawnLimits;


    [Space(20)]
    [Header("No limit to the number of items")]
    [Header("Put Prefabs of items here")]
    public GameObject[] objects;
    public float minItems;
    public float maxItems;

    [Space(20)]
    [Header("No limit to the number of fishes spawners")]
    [Header("Put Prefabs of fishes / Fishes spawner here. More Spawners = Heavier impact on FPS")]
    public GameObject[] fishSpawners;
    public float minFish;
    public float maxFish;

    [Space(20)]
    [Header("No limit to the number of enemies that spawns")]
    [Header("Put Prefabs of enemies")]
    public GameObject[] enemies;
    public float minEnemies;
    public float maxEnemies;




    private float LimitX1, LimitY1, LimitZ1, LimitX2, LimitY2, LimitZ2;
    private float fishLimitX1, fishLimitY1, fishLimitZ1, fishLimitX2, fishLimitY2, fishLimitZ2;

    private void Awake()
    {
        
        GetCoords();
        SpawnItems();
        SpawnEnnemies();
    }

    private void GetCoords()
    {
         LimitX1 = itemSpawnLimits[0].gameObject.GetComponent<Transform>().position.x;
         LimitY1 = itemSpawnLimits[0].gameObject.GetComponent<Transform>().position.y;
         LimitZ1 = itemSpawnLimits[0].gameObject.GetComponent<Transform>().position.z;
         LimitX2 = itemSpawnLimits[1].gameObject.GetComponent<Transform>().position.x;
         LimitY2 = itemSpawnLimits[1].gameObject.GetComponent<Transform>().position.y;
         LimitZ2 = itemSpawnLimits[1].gameObject.GetComponent<Transform>().position.z;

         fishLimitX1 = fishSpawnLimits[0].gameObject.GetComponent<Transform>().position.x;
         fishLimitY1 = fishSpawnLimits[0].gameObject.GetComponent<Transform>().position.y;
         fishLimitZ1 = fishSpawnLimits[0].gameObject.GetComponent<Transform>().position.z;
         fishLimitX2 = fishSpawnLimits[1].gameObject.GetComponent<Transform>().position.x;
         fishLimitY2 = fishSpawnLimits[1].gameObject.GetComponent<Transform>().position.y;
         fishLimitZ2 = fishSpawnLimits[1].gameObject.GetComponent<Transform>().position.z;



    }

    private void SpawnItems() 
    {
        float randomNumberOfItems = Random.Range(maxItems, minItems);
        int i = 0;
        while (i <= randomNumberOfItems)
        {
            int randomItem = Random.Range(0, objects.Length);
            Vector3 randomSpawnPos = new Vector3 (Random.Range(LimitX1, LimitX2), Random.Range(LimitY1, LimitY2), Random.Range(LimitZ1, LimitZ2));
            int randomXRotation = Random.Range(0, 360);
            int randomYRotation = Random.Range(0, 360);
            int randomZRotation = Random.Range(0, 360);
            Instantiate(objects[randomItem], randomSpawnPos, Quaternion.Euler(randomXRotation, randomYRotation, randomZRotation));
            i++;
        }

    }

    private void SpawnEnnemies()
    {
        float randomNumberOfSharks = Random.Range(maxEnemies, minEnemies);
        int i = 0;
        
        while (i <= randomNumberOfSharks)
        {
            int randomShark = Random.Range(0, enemies.Length);
            Vector3 randomSpawnPos = new Vector3(Random.Range(fishLimitX1, fishLimitX2), Random.Range(fishLimitY1, fishLimitY2), Random.Range(fishLimitZ1, fishLimitZ2));
            Instantiate(enemies[randomShark], randomSpawnPos, Quaternion.Euler(0, 0, 0));
            i++;
        }

        float randomNumberOfFish = Random.Range(maxFish, minFish);
        i = 0;
        while (i <= randomNumberOfFish)
        {
            int randomFish = Random.Range(0, fishSpawners.Length);
            Vector3 randomSpawnPos = new Vector3(Random.Range(fishLimitX1, fishLimitX2), Random.Range(fishLimitY1, fishLimitY2), Random.Range(fishLimitZ1, fishLimitZ2));
            Instantiate(fishSpawners[randomFish], randomSpawnPos, Quaternion.Euler(0, 0, 0));
            i++;
        }

    }
}
