using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEngine.Localization;

public class DragAndDrop : MonoBehaviour
{
    public static DragAndDrop instance;

    DragAndDrop dragAndDrop;
	[SerializeField] private InputAction press, screenPos;

    private Vector3 curScreenPos;
    private Transform spriteTransform;
    private SpriteRenderer objectSprite;
    
    Vector3 originalScale;
    Camera camera;
    public bool isDragging;

    

    public string ptName;
    public string enName;

    private Vector3 WorldPos
    {
        get
        {
            float z = camera.WorldToScreenPoint(transform.position).z;
            return camera.ScreenToWorldPoint(curScreenPos + new Vector3(0, 0, z));
        }
    }
    private bool isClickedOn
    {
        get
        {
            Ray ray = camera.ScreenPointToRay(curScreenPos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if(dragAndDrop != null)
                {

                    return hit.transform == transform;
                }
            }
            return false;
        }
    }
    private void Awake()
    {
        camera = Camera.main;
        screenPos.Enable();
        press.Enable();
        screenPos.performed += context => { curScreenPos = context.ReadValue<Vector2>(); };
        press.performed += _ => { if (isClickedOn) StartCoroutine(Drag()); };
        press.canceled += _ => { isDragging = false; };

        

        objectSprite = GetComponentInChildren<SpriteRenderer>();
        spriteTransform = objectSprite.transform;
        originalScale = new Vector3(spriteTransform.localScale.x, spriteTransform.localScale.y, spriteTransform.localScale.z);

    }
    private void Start()
    {
        dragAndDrop = GetComponent<DragAndDrop>();
    }

    private void Update()
    {
        if (isDragging)
        {
            instance = this;
            ObjectNameManager.instance.SetText(ptName, enName);
        }

        if(!isDragging)
        {
            instance = null;
        }
    }


    private IEnumerator Drag()
    {
        isDragging = true;
        Vector3 offset = transform.position - WorldPos;
        // grab
        GetComponent<Rigidbody>().useGravity = false;
        while (isDragging)
        {
            // dragging
            transform.position = WorldPos + offset;
            yield return null;
        }
        // drop
        GetComponent<Rigidbody>().useGravity = true;
    }

    

    public void TweenLow(float animationSpeed)
    {
        spriteTransform.DOScale(Vector3.zero, animationSpeed).SetEase(Ease.Linear);
    }

    public void TweenHigh(float animationSpeed)
    {
        
       
        spriteTransform.DOScale(originalScale, animationSpeed).SetEase(Ease.Linear);
    }
   
}
