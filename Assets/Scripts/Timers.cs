using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;
public class Timers : MonoBehaviour
{
    public int maxTime;
    public int inc_dec;//increasing or decreasing
    private GameObject zombie;

    private void LateUpdate()
    {
        outOfTime();
    }
    // Start is called before the first frame update

    void Start()
    {
        animate();

        if(gameObject.transform.position.x<0)//left
            zombie = GameObject.FindGameObjectWithTag("ZombieL");
        else
            zombie = GameObject.FindGameObjectWithTag("ZombieR");//this sucks, will redo this later
    }

    private void animate()
    {
        LeanTween.scaleX(gameObject, inc_dec, maxTime);
    }

    private void OnDestroy()
    {
        Destroy(gameObject.transform.parent);
    }

    private void outOfTime()
    {
        if (!LeanTween.isTweening(gameObject))
        {
            try
            {
                zombie.GetComponent<DoubleOrderManager>().ZombieFailed();
                Destroy(gameObject);
            }
            catch (System.NullReferenceException)
            {
                Debug.Log("2 timers expired simultaneously");
            }
        }


    }
}
