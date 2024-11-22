using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("2 Spawner Max")]
    public GameObject[] itemSpawners;
    public GameObject[] fishSpawners;
    public GameObject[] objects;
    public float maxItems;
    public float minItems;

    public float minEnnemies;
    public float maxEnnemies;

    private float LimitX1, LimitY1, LimitZ1, LimitX2, LimitY2, LimitZ2;
    private float fishLimitX1, fishLimitY1, fishLimitZ1, fishLimitX2, fishLimitY2, fishLimitZ2;

    public GameObject[] ennemy;
    private void Awake()
    {
        
        GetCoords();
        SpawnItems();
        SpawnEnnemies();
    }

    private void GetCoords()
    {
         LimitX1 = itemSpawners[0].gameObject.GetComponent<Transform>().position.x;
         LimitY1 = itemSpawners[0].gameObject.GetComponent<Transform>().position.y;
         LimitZ1 = itemSpawners[0].gameObject.GetComponent<Transform>().position.z;
         LimitX2 = itemSpawners[1].gameObject.GetComponent<Transform>().position.x;
         LimitY2 = itemSpawners[1].gameObject.GetComponent<Transform>().position.y;
         LimitZ2 = itemSpawners[1].gameObject.GetComponent<Transform>().position.z;

         fishLimitX1 = fishSpawners[0].gameObject.GetComponent<Transform>().position.x;
         fishLimitY1 = fishSpawners[0].gameObject.GetComponent<Transform>().position.y;
         fishLimitZ1 = fishSpawners[0].gameObject.GetComponent<Transform>().position.z;
         fishLimitX2 = fishSpawners[1].gameObject.GetComponent<Transform>().position.x;
         fishLimitY2 = fishSpawners[1].gameObject.GetComponent<Transform>().position.y;
         fishLimitZ2 = fishSpawners[1].gameObject.GetComponent<Transform>().position.z;



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
        float randomNumberOfSharks = Random.Range(maxEnnemies, minEnnemies);
        int i = 0;
        
        while (i <= randomNumberOfSharks)
        {
            int randomShark = Random.Range(0, ennemy.Length);
            Vector3 randomSpawnPos = new Vector3(Random.Range(fishLimitX1, fishLimitX2), Random.Range(fishLimitY1, fishLimitY2), Random.Range(fishLimitZ1, fishLimitZ2));
            Instantiate(ennemy[randomShark], randomSpawnPos, Quaternion.Euler(0, 0, 0));
            i++;
        }

    }
}
