using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnObject : MonoBehaviour
{
    SpawnObjects spawnObjects;
    DragAndDrop dragAndDrop;
    float despawnTime;
    float countdownTime;

    void Start()
    {
        dragAndDrop = GetComponent<DragAndDrop>();
        spawnObjects = FindAnyObjectByType<SpawnObjects>();
        despawnTime = UnityEngine.Random.Range(spawnObjects.minDespawnTime, spawnObjects.maxDespawnTime);
        countdownTime = despawnTime;

    }

    private void Update()
    {
        if (!dragAndDrop.isDragging)
        {
            countdownTime -= Time.deltaTime;
            if (countdownTime <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
