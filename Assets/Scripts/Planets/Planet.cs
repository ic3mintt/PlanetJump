using System;
using System.Collections;
using UnityEngine;

public class Planet : MonoBehaviour, IPoolable
{
    private Camera camera;
    private Coroutine screenPresenceCheckCoroutine;
        
    public event Action<Planet> EndLife; //invokes only if it instantiated by manager
    
    [SerializeField] private float screenCheckDelay;

    //is it right that a planet decides what to do after condition in if?
    //but it is one task planet does
    //actually i don't know how to do it by another way
    /*
    private IEnumerator ScreenPresenceCheck()
    {
        while (true)
        {
            var upCircleYPositionRelativeToCamera =
                camera.WorldToViewportPoint(positionRandomizer.GetUpCirclePosition(transform));
            Debug.Log(upCircleYPositionRelativeToCamera);
            //[0;1] relative to screen
            
            if (upCircleYPositionRelativeToCamera <= 0)
            {
                EndLife?.Invoke(this);
            }
            yield return new WaitForSeconds(screenCheckDelay);
        }
    }
    */
    public void Show()
    {
        gameObject.SetActive(true);        
        //screenPresenceCheckCoroutine = StartCoroutine(ScreenPresenceCheck());

        //change sprite
        //a planet should not change colour by myself
        //it should do a manager
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}