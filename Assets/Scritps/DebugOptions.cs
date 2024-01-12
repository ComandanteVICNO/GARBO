using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugOptions : MonoBehaviour
{
    public GameObject plasticObject;
    public GameObject batteryObject;
    public GameObject metalObject;

    [Header("GameObject Array")]
    public GameObject[] paperObjects;
    public GameObject[] glassObjects;

    public Transform paperTransform;
    public Transform plasticTransform;
    public Transform batteryTransform;
    public Transform glassTransform;
    public Transform metalTransform;

    public float spawnCheckRadius = 0.5f; // Adjust the radius as needed

    public void SpawnPaper()
    {
        if (!IsOccupied(paperTransform.position))
        {
            int index = Random.Range(0, paperObjects.Length);

            Instantiate(paperObjects[index], paperTransform.position, Quaternion.identity);
            
        }
    }

    public void SpawnPlastic()
    {
        if (!IsOccupied(plasticTransform.position))
        {
            Instantiate(plasticObject, plasticTransform.position, Quaternion.identity);
        }
    }

    public void SpawnBattery()
    {
        if (!IsOccupied(batteryTransform.position))
        {
            Instantiate(batteryObject, batteryTransform.position, Quaternion.identity);
        }
    }

    public void SpawnGlass()
    {
        if (!IsOccupied(glassTransform.position))
        {
            int index = Random.Range(0, paperObjects.Length);

            Instantiate(glassObjects[index], glassTransform.position, Quaternion.identity);
        }
    }

    public void SpawnMetal()
    {
        if (!IsOccupied(metalTransform.position))
        {
            Instantiate(metalObject, metalTransform.position, Quaternion.identity);
        }
    }

    private bool IsOccupied(Vector3 position)
    {
        // Check for colliders in the specified radius
        Collider[] colliders = Physics.OverlapSphere(position, spawnCheckRadius);

        // If there are colliders (other objects) within the radius, consider the position occupied
        return colliders.Length > 0;
    }
}
