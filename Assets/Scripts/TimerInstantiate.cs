using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using System;

public class TimerInstantiate : MonoBehaviour
{

    public GameObject timer;
    private GameObject timerClone;
    public GameObject canvas;
    public Vector3 position = new Vector3(0, 2);//position of timer
 

   

    public void instantiateTimer()
    {
        timerClone = Instantiate(timer, canvas.transform);
        timerClone.SetActive(true);
        timerClone.transform.position = gameObject.transform.position+position;

    }

    public void destroyTimer()
    {
        Destroy(timerClone);
    }

    

    //method to pause the timer for 3 seconds 
    public void addTime()
    {
        LeanTween.pause(gameObject);
         StartCoroutine(Resume(false, 3));
    }

    IEnumerator Resume(bool status, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        LeanTween.resume(gameObject);//resume the animation
    }
}
