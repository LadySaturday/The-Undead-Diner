using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutPopulate : MonoBehaviour
{
    public GameObject[] images;
    private Vector3 position = new Vector3(250,-108);
    // Start is called before the first frame update
    void Start()
    {
        populate();
        
    }

    void populate()
    {
        int x = 0;
        foreach (GameObject image in images)
        {
            Instantiate(image, gameObject.transform);
            image.transform.position = position;
            position += new Vector3(500, 0);

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
