using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script is to make Barista move what the player clicks on a specific item
//First draft
public class Click : MonoBehaviour
{
    private GameObject Barista;
    private BaristaController baristaController;
    private Queue<GameObject> targetQueue;


    // Start is called before the first frame update
    void Start()
    {
        //Get reference to Barista
        Barista = GameObject.FindGameObjectWithTag("Player");
        baristaController = Barista.GetComponent<BaristaController>();
        targetQueue = baristaController.targetQueue;
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
           baristaController.onQueue(hit.collider.gameObject);//Add that product's position to the queue
            
        }


    }
}
