using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerInstantiate : MonoBehaviour
{

    public GameObject timer;
    private GameObject timerClone;
    public GameObject canvas;
    private Vector3 position = new Vector3(0, 2);//position of timer


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
}
