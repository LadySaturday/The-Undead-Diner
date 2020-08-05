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
    private Vector2 position;//Nicks current position
    
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
        /*
         * there exists a bug here where the mixing cup is clickble 
         * so the target becomes null and causes an error when it's destroyed
         * 
         * 
         * */
        if (targetQueue.Count > 0)//is there a target position?
        {
            target = targetQueue.Peek();//go to the first position in the queue

            float step = speed * Time.deltaTime;
            anim.SetFloat("Speed", 1);

            if (target.transform.position.x - gameObject.transform.position.x < 0)//if nick is facing away
            {
                Vector3 lTemp = transform.localScale;
                lTemp.x = -0.35f;
                gameObject.transform.localScale = lTemp;//change local scale so she's facing the right way
            }
            else
            {
                Vector3 lTemp = transform.localScale;
                lTemp.x = 0.35f;
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

    private bool doubleClickCheck()
    {
        if (targetQueue.Count > 1)
        {
            if (targetQueue.ElementAt(0) == targetQueue.ElementAt(1))
            {
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }

    void ItemReached()
    {


        /*  if (target.gameObject.transform.parent.name == "BrainSlushReadyToGrab(Clone)")
          {
              if (doubleClickCheck())
              {
                  target.gameObject.transform.parent.GetComponent<Animator>().SetTrigger("doubleTargetReached");
                  target = null;

                  targetQueue.Dequeue();//Remove position from the list
                  targetQueue.Dequeue();//Remove position from the list
                  return;
              }
              else
              {
                  target.gameObject.transform.parent.GetComponent<Animator>().SetTrigger("targetReached"); //animate dem bad boys
              }
          }
          else
          {
              target.gameObject.transform.parent.GetComponent<Animator>().SetTrigger("targetReached"); //animate dem bad boys
          }*/

        target.gameObject.transform.parent.GetComponent<Animator>().SetTrigger("targetReached"); //animate dem bad boys

        target = null;

        targetQueue.Dequeue();//Remove position from the list
    }

}
