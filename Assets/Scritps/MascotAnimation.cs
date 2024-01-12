using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MascotAnimation : MonoBehaviour
{

    public RectTransform backImage;
    public RectTransform eyeImage;
    public RectTransform backRefPoint;
    public RectTransform eyeRefPoint;

    public float rotationSpeed;
    public float floatSpeed;
    public float floatHeight;


    public float minAnimationWaitTime;
    public float maxAnimationWaitTime;

    public Animator eyeAnimator;
    public AnimationClip eyeAnimation;

    bool canAnimate = true;
    bool wasTimeSelected = false;
    bool wasActivated = false;

    [Header("TweenAnimation")]
    public float transitionTime;

    public float yDerivation;

    
    Vector2 backPositionUp;
    Vector2 backPositionDown;

    
    Vector2 eyePositionUp;
    Vector2 eyePositionDown;
    public Ease firstEase;
    public Ease animationEase;
    private void Start()
    {

        CheckPositions();

        FloatUp(1, firstEase);
    }
    // Update is called once per frame
    void Update()
    {
        OutAnimation();
        
        HandleAnimation();
        CheckPositions();



    }
    //tween

    public void CheckPositions()
    {
        

        backPositionUp = new Vector2(backRefPoint.position.x, backRefPoint.position.y + yDerivation);
        backPositionDown = new Vector2(backRefPoint.position.x, backRefPoint.position.y - yDerivation);


        eyePositionUp = new Vector2(eyeRefPoint.position.x, eyeRefPoint.position.y + yDerivation);
        eyePositionDown = new Vector2(eyeRefPoint.position.x, eyeRefPoint.position.y - yDerivation);
    }

    public void FloatUp(int multy, Ease ease)
    {
        backImage.DOMove(backPositionUp, transitionTime * multy).SetEase(ease);
        eyeImage.DOMove(eyePositionUp, transitionTime * multy).SetEase(ease).OnComplete(() => FloatDown(2, animationEase));

        Debug.Log(transitionTime * multy);
    }

    public void FloatDown(int multy, Ease ease)
    {
        backImage.DOMove(backPositionDown, transitionTime * multy).SetEase(ease);
        eyeImage.DOMove(eyePositionDown, transitionTime * multy).SetEase(ease).OnComplete(() => FloatUp(2, animationEase));

        Debug.Log(transitionTime * multy);
    }


    public void OutAnimation()
    {
        backImage.Rotate(Vector3.forward, -(rotationSpeed * Time.deltaTime));
        
    }


    public void HandleAnimation()
    {
        if (!canAnimate) return;
        if (wasTimeSelected) return;
        
        float TimeBewteenAnimation = UnityEngine.Random.Range(minAnimationWaitTime, maxAnimationWaitTime);
        wasTimeSelected = true;
        {
            StartCoroutine(Animation(TimeBewteenAnimation));
        }

    }

    IEnumerator Animation(float time)
    {
        canAnimate = false;

        yield return new WaitForSecondsRealtime(time);

        eyeAnimator.SetBool("canAnimate", true);

        yield return new WaitForSecondsRealtime(eyeAnimation.length);

        eyeAnimator.SetBool("canAnimate", false);
        canAnimate = true;
        wasTimeSelected = false;
    }

}
