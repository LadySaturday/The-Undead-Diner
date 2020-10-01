using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;
public class Timers : MonoBehaviour
{
    public int maxTime;
    public int inc_dec;//increasing or decreasing
    // Start is called before the first frame update
    void Start()
    {
        animate();   
    }

    private void animate()
    {
        LeanTween.scaleX(gameObject, inc_dec, maxTime);
    }

    private void OnDestroy()
    {
        Destroy(gameObject.transform.parent);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
