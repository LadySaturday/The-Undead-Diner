using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//This script is to make Barista move what the player clicks on a specific item
//First draft
public class BaristaController : MonoBehaviour
{

    public float speed = 5.0f;//Player speed (will be changeable later)
    public GameObject target;//position of target destination
    public Queue<GameObject> targetQueue = new Queue<GameObject>();//queue of target positions
    private Vector2 position;//Barista's current position
    public GameObject[] numberArray;
    private Queue<GameObject> numberQueue;
    GameObject[] tempNumberQueue ;
    private Animator anim;


    //hands
    public bool isHoldingOrder = false;
    public bool leftHandIsFull = false;
    public bool rightHandIsFull = false;
    private GameObject leftHandObject;
    private GameObject rightHandObject;


    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();

        leftHandObject = GameObject.FindGameObjectWithTag("LeftHand");
        rightHandObject = GameObject.FindGameObjectWithTag("RightHand");
    }

    // Update is called once per frame
    void Update()
    {

        moveToTarget();
        checkHands();

    }
        void checkHands()
        {
            if (leftHandObject.transform.childCount > 0)
                leftHandIsFull = true;
            else
                leftHandIsFull = false;
            if (rightHandObject.transform.childCount > 0)
                rightHandIsFull = true;
            else
                rightHandIsFull = false;


            anim.SetBool("leftHandIsFull", leftHandIsFull);
            anim.SetBool("rightHandIsFull", rightHandIsFull);
        }

    public void removeItems()
    {
        if (leftHandObject.transform.childCount > 0)
            Destroy(leftHandObject.transform.GetChild(0).gameObject);

        if (rightHandObject.transform.childCount > 0)
            Destroy(rightHandObject.transform.GetChild(0).gameObject);
    }
    

    void moveToTarget()
    {
        if (targetQueue.Count > 0)//is there a target position?
        {
            target = targetQueue.Peek();//go to the first position in the queue

            float step = speed * Time.deltaTime;
            anim.SetFloat("Speed", 1);

            if (target.transform.position.x - gameObject.transform.position.x < 0)//if barista is facing away
            {
                Vector3 lTemp = transform.localScale;
                lTemp.x = -0.3972589f;
                gameObject.transform.localScale = lTemp;//change local scale so she's facing the right way
            }
            else
            {
                Vector3 lTemp = transform.localScale;
                lTemp.x = 0.3972589f;
                gameObject.transform.localScale = lTemp;
            }

            // move sprite towards the target location
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);

            if (Vector2.Distance(transform.position, target.transform.position) < 0.01f)//Only do a thing once Barista has arrived
            {
                anim.SetFloat("Speed", 0);
                
                ItemReached();//check what to do about the item

            }
        }
    }
    

    void ItemReached()
    {
        
        if (target.gameObject.transform.parent.name == "MixingCupIdle")
        { 
            if (leftHandIsFull && rightHandIsFull)
                target.gameObject.transform.parent.GetComponent<Animator>().SetBool("handsAreFull", true);
            else
                target.gameObject.transform.parent.GetComponent<Animator>().SetBool("handsAreFull", false);
        }               
        if (target.gameObject.transform.parent.GetComponent<Animator>().GetBool("canSetTrigger"))
        {

            target.gameObject.transform.parent.GetComponent<Animator>().SetTrigger("targetReached"); //animate dem bad boys
        }
        

        target = null;

        onDequeue();//Remove position from the list
    }

   

    //onQueue will add the next number to the end of the queue, and instantiate number

    Vector3 setPosition(GameObject parent)
    {
        if(parent.transform.childCount>0)
        {
           return parent.transform.GetChild(0).transform.position;//get positioner position
        }
        else
        {
            return parent.transform.position;
        }

 

    }

    //onDequeue will  remove the first number, and shuffle the rest
    public void onQueue(GameObject t)
    {
        GameObject num;
        targetQueue.Enqueue(t);
        num=Instantiate(numberArray[targetQueue.Count-1], 
           setPosition(t), 
            Quaternion.identity);
       

    }

    void onDequeue()
    {
        targetQueue.Dequeue();
        tempNumberQueue = targetQueue.ToArray();//indexable version of the queue
        //need to shuffle the stuff
        GameObject num;
        GameObject[] q=GameObject.FindGameObjectsWithTag("numberedQueue");

        foreach (GameObject number in q){
            Destroy(number);
        }
       


        //instantiate new stuff
        for (int x = 0; x < targetQueue.Count ; x++)
        {
           num= Instantiate(numberArray[x], setPosition(tempNumberQueue[x]),Quaternion.identity);
        }

    }
    
}
