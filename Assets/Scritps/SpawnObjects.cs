using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    int randomIndex;
    Coroutine coroutine;
    public ScoreManager scoreManager;

    [Header("Objects to Spawn")]
    public GameObject[] paperObjects;
    public GameObject[] glassObjects;
    public GameObject[] plasticAndMetalObjects;
    public GameObject[] organicObjects;
    public GameObject[] electricObjects;
    GameObject objectToSpawn;
    private int selectedCategory;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;
    public Transform selectedSpawnPoint;


    [Header("Values")]
    public float minSpawnTime;
    public float maxSpawnTime;
    public float spawnCheckRadius;
    public float spawnTime;

    [Header("Despawn Objects")]
    public float minDespawnTime;
    public float maxDespawnTime;


    
    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        coroutine = StartCoroutine(SpawnObject());


    }

    private void Update()
    {
        DoSpawnCoroutine();
        
    }

    void DoSpawnCoroutine()
    {
        if (scoreManager.isGameOver) return;
        else
        {
            if (coroutine != null) return;
            else
            {
                coroutine = StartCoroutine(SpawnObject());
            }
        }
    }

    IEnumerator SpawnObject()
    {
        
        SetRandomCooldown();
        SelectSpawnPoint();
        SelectObjectInCategory();


        yield return new WaitForSecondsRealtime(spawnTime);
        

        if (selectedSpawnPoint != null && objectToSpawn != null)
        {
            if (!IsOccupied(selectedSpawnPoint.position))
            {
                
                GameObject spawnObject = Instantiate(objectToSpawn, selectedSpawnPoint.position, Quaternion.identity);
                DespawnObject despawnObject = spawnObject.AddComponent<DespawnObject>();
            }


        }

         
        

        objectToSpawn = null;
        RemoveSelectedSpawnPoint();
        coroutine = null;
        
    }

    public void SetRandomCooldown()
    {
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    

    

    #region SpawnLocationSelection
    public Transform SelectSpawnPoint()
    {

        int randomIndex = Random.Range(0, spawnPoints.Length);

        selectedSpawnPoint = spawnPoints[randomIndex];

        randomIndex = -1;

        return selectedSpawnPoint;

    }


    public void RemoveSelectedSpawnPoint()
    {
        selectedSpawnPoint = null;
    }
    #endregion

    #region SpawnObjectSelection
    private void SelectCategory()
    {
        int randomNum = Random.Range(1, 6);
        Debug.Log(randomNum);
        switch (randomNum)
        {
            case 1:
                selectedCategory = 1;
                break;
            case 2:
                selectedCategory = 2;
                break;
            case 3:
                selectedCategory = 3;
                break;
            case 4: 
                selectedCategory = 4;
                break;
            case 5:
                selectedCategory = 5;
                break;
        }

        //randomNum = -1;
    }

    public GameObject SelectObjectInCategory()
    {
        SelectCategory();
        int randomIndex;
        switch (selectedCategory)
        {
            case 1:
                randomIndex = Random.Range(0, paperObjects.Length );
                objectToSpawn = paperObjects[randomIndex];
               
                break;

            case 2:

                randomIndex = Random.Range(0, glassObjects.Length );
                objectToSpawn = glassObjects[randomIndex];
                
                break;

            case 3:

                randomIndex = Random.Range(0, plasticAndMetalObjects.Length);
                objectToSpawn = plasticAndMetalObjects[randomIndex];
                
                break;

            case 4:
                randomIndex = Random.Range(0, organicObjects.Length);
                objectToSpawn = organicObjects[randomIndex];

                break;
            case 5:
                randomIndex = Random.Range(0, electricObjects.Length);
                objectToSpawn = electricObjects[randomIndex];

                break;
        }

        return objectToSpawn;
    }
    #endregion

    private bool IsOccupied(Vector3 position)
    {
        
        Collider[] colliders = Physics.OverlapSphere(position, spawnCheckRadius);

        
        return colliders.Length > 0;
    }
}
