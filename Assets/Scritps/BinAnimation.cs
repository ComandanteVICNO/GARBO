using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BinAnimation : MonoBehaviour
{
    public Animator animator;
    public DragAndDrop dragAndDrop;
    public GameObject dragObject;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (dragObject != null) return;
        else
        {
            animator.SetBool("isOpen", false);
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        
        dragAndDrop = other.GetComponent<DragAndDrop>();
        animator.SetBool("isOpen", true);
        dragObject = other.GameObject();

    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("isOpen", false);
    }
}
