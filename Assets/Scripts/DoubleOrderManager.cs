

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoubleOrderManager : MonoBehaviour
{
    //The purpose of this script is to act as the order system. Randomize orders. See if the player has the right order. 
    //If they do, destroy what they're holding. Spawn the next order

    //Reference to products and to player
    public GameObject[] products;//ALL products being served
    public GameObject[] productImages;//ALL products being served
    public GameObject[] orderBackgrounds;
    int numberOfOrderItems;//before an order is created, determine the number of items in that order
    private GameObject orderVisual;
    private GameObject orderBg;

    private GameObject leftHandObject;
    private GameObject rightHandObject;
    private GameObject[] playerHolding = new GameObject[2];//the objects the player is holding
    private GameObject[] zombieOrders;
    //public GameObject zTimer;//reference to the zombie countdown timer
    private int numZombiesFailed;//the number of zombies the player didn't feed


    private bool isThereAnOrder;

    private Vector3 imageOffset = new Vector3(0.9f, -10);//the offset for the items in the order visual


    System.Random rnd = new System.Random();//new random, for orders

    private float delayTime;//a random time to wait between orders appearing.

    private int maxOrderNumber=3;//max number of items in an order
    private bool makingOrder;

    void Start()
    {
        delayTime = 4;//Start up time

        leftHandObject = GameObject.FindGameObjectWithTag("LeftHand");

        rightHandObject = GameObject.FindGameObjectWithTag("RightHand");

        if(gameObject.name=="RightZombie")
            StartCoroutine(ZombieOrder(false, delayTime));
        else
            StartCoroutine(ZombieOrder(false, 5));

        orderVisual = gameObject.transform.GetChild(1).gameObject;

        
        
    }
    private void Update()
    {
        checkHands();
    }

    private void checkHands()
    {
        if (leftHandObject.transform.childCount > 0)
            playerHolding[0] = leftHandObject.transform.GetChild(0).gameObject;
        else
            playerHolding[0] = null;

        if (rightHandObject.transform.childCount > 0)
            playerHolding[1] = rightHandObject.transform.GetChild(0).gameObject;
        else
            playerHolding[1] = null;
    }
    //we gotta build the order, wait, nd then show it. 


    IEnumerator ZombieOrder(bool status, float delayTime)
    {

        yield return new WaitForSeconds(delayTime);
         rnd = new System.Random();


        imageOffset.y = -0.70f;
        numberOfOrderItems = rnd.Next(1, maxOrderNumber + 1);//between 1 and 3 items per order  
        zombieOrders = new GameObject[numberOfOrderItems];//temp order

        orderBg = Instantiate(orderBackgrounds[numberOfOrderItems - 1], orderVisual.transform);//Spawn correct background for number of items

        for (int x = 0; x < numberOfOrderItems; x++)
        {
            int y = rnd.Next(products.Length);//choose any number corresponing to a product
            zombieOrders[x] = Instantiate(productImages[y],
            orderBg.transform.position + imageOffset,
            Quaternion.identity,
            orderBg.transform);

            imageOffset.y += -0.95f;//places next order visual lower
        }

        
        makingOrder = false;

    }




    public void ClearOrder()//player picks up wrong thing. taps garbage. removes items.
    {
        for (int itemNumber = 0; itemNumber < playerHolding.Length; itemNumber++)
        {
            Destroy(playerHolding[itemNumber]);
            playerHolding[itemNumber] = null;
        }

    }


    

    public void CheckOrder()
    {
        if(zombieOrders.Length>0)
        for (int zombieOrderNumber = 0; zombieOrderNumber<=zombieOrders.Length-1; zombieOrderNumber++)//increases to test every item in Zombie's order
        {
            for (int playerHandNumber = 0; playerHandNumber < 2; playerHandNumber++)//increases to test every item in Player's hand
            {
                if (playerHolding[playerHandNumber] != null)//player doesn't have anything in that hand? Don't test it
                {
                    if (zombieOrders[ zombieOrderNumber] != null)
                    {
                        if (playerHolding[playerHandNumber].tag == zombieOrders[ zombieOrderNumber].tag)//checks both hands against every item in the zombie's order
                        {
                            
                            Destroy(playerHolding[playerHandNumber].gameObject);
                            playerHolding[playerHandNumber] = null;
                            Destroy(zombieOrders[ zombieOrderNumber].gameObject);
                            zombieOrders[ zombieOrderNumber] = null;//that item becomes null

                        }
                    }

                }
            }
        }
        ///////THE BUG IS CAUSED BY A COUBLE CLIIIIIIIICKCKKCKCKCKCKKCK
        



        
            isThereAnOrder = false;//do we need to create a new order?
            for (int x = 0; x <= zombieOrders.Length-1; x++)
            {
                if (zombieOrders[x] != null)
                {
                    isThereAnOrder = true;//will only be turned "true" if an item in the Zombie's order still exists. Otherwise, a new order will be made
                }
            }
            if (isThereAnOrder == false)
            {
                foreach (Transform child in orderVisual.transform)
                {
                    if (child.tag != "DontDestroy")
                        Destroy(child.gameObject);
                }

                delayTime = rnd.Next(1, 4);//wait before new order appears
                                           // isThereAnOrder = true;
            if (!makingOrder)
            {
                StartCoroutine(ZombieOrder(false, delayTime));

                makingOrder = true;
            }
                

            }
        
        

    }



    /*
    public void ZombieFailed(int leftOrRight)//did a zombie's timer run out? (Invoked by animation event)
    {
        numZombiesFailed++;//after 3 failed zombies, it's game over
        int itemNum = 0;
        if (numZombiesFailed > 2)
        {
            SceneManager.LoadScene(2);//temporary
        }

        if (leftOrRight == 0)
            Zombie[leftOrRight].GetComponent<Zombie_Controller>().Anim("Exit", true);
       

        foreach (Transform child in OrderVisuals[leftOrRight].transform)
            {

            

            Destroy(child.gameObject);

            if (itemNum < 3)
            {
                Destroy(zombieOrders[leftOrRight, itemNum]);
                zombieOrders[leftOrRight, itemNum] = null;
            }
                
                itemNum++;
            }

        StartCoroutine(ZombieOrder(false, delayTime, leftOrRight));

    }
    */
}
