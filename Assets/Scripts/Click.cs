using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script is to make Barista move what the player clicks on a specific item
//First draft
public class Click : MonoBehaviour
{
    private GameObject Barista;


    // Start is called before the first frame update
    void Start()
    {
        //Get reference to Barista
        Barista = GameObject.FindGameObjectWithTag("Player");

    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))//when clicked
        {
                clicked();
        }

    }


    void clicked()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider != null)
        {
            //decide what to do with the object hit. 
            if (hit.collider.tag == "Moveable")
            {
                //will decide what to do about animations once the target is reached
                 Barista.GetComponent<BaristaController>().targetQueue.Enqueue(hit.collider.gameObject);//Add that product's position to the queue
            }

        }


    }
}
