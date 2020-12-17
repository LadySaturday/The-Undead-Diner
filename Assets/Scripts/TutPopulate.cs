using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutPopulate : MonoBehaviour
{
    public GameObject[] images;//array to hold all tutorial images
    private Vector3 position = new Vector3(250,-108);//starting position of images
    private int positionX = 500;//int to space out images evenly

    void Start()
    {
        populate();       
    }

    void populate()
    {
        foreach (GameObject image in images)
        {
            Instantiate(image, gameObject.transform);
            image.transform.position = position;
            position += new Vector3(positionX, 0);//the next position

        }

    }

}
