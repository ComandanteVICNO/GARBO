using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAnimation : MonoBehaviour
{
    private DragAndDrop dragNDrop;
    public float animationSpeed;


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        dragNDrop = other.GetComponent<DragAndDrop>();
        dragNDrop.TweenLow(animationSpeed);

    }

    private void OnTriggerExit(Collider other)
    {

            dragNDrop.TweenHigh(animationSpeed);
            
      
    }
}
