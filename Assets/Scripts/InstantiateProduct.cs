using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InstantiateProduct : MonoBehaviour
{
    //A generic script to instantiate whichever product needs to be instantiated at specific points in the animations
    // Start is called before the first frame update
    public GameObject [] product;
    private GameObject leftHand;
    private GameObject barista;
    private GameObject rightHand;
    private Queue<GameObject> targetQueue;
    private BaristaController baristaController;
    private bool doubleClicked;

    private void Start()
    {
        leftHand = GameObject.FindGameObjectWithTag("LeftHand");
        rightHand = GameObject.FindGameObjectWithTag("RightHand");
        barista = GameObject.FindGameObjectWithTag("Player");

        baristaController = barista.GetComponent<BaristaController>();
        targetQueue = baristaController.targetQueue;

    }

    public void instantiateItemInHand(int position)//position is set in the inspector
    {

        if (!DoubleClick(position))
        {
            if (!baristaController.leftHandIsFull)//left hand isn't full
            {
                Instantiate(product[position], leftHand.transform);//put the object in the left hand

            }
            else if (!baristaController.rightHandIsFull)//right hand isn't full
            {
                Instantiate(product[position], rightHand.transform);//put the object in the right hand

            }
        }
        else
        {
            if (!baristaController.leftHandIsFull)//left hand isn't full
            {
                Instantiate(product[position], leftHand.transform);//put the object in the left hand

            }
            if (!baristaController.rightHandIsFull)//right hand isn't full
            {
                Instantiate(product[position], rightHand.transform);//put the object in the right hand

            }
        }
            
    
    }
   

    

    private void InstantiateCupOfJoeVariation(int position)
    {

        int l = -1;//object in left hand
        int r = -1;//object in right hand

            if (leftHand.transform.childCount>0)
            {
                 l = (int)char.GetNumericValue(leftHand.transform.GetChild(0).tag.ToCharArray().Last());//the step number is written in the object variations tag, obtained through this line
            }
            if (rightHand.transform.childCount > 0)
            {
                 r = (int)char.GetNumericValue(rightHand.transform.GetChild(0).tag.ToCharArray().Last());
            }

            int p = (int)char.GetNumericValue(product[position].tag.ToCharArray().Last());//product we're testing to replace

        if (!DoubleClick(position))
        {
            if (l == p - 1)//is this product the next step variation?
            {
                Destroy(leftHand.transform.GetChild(0).gameObject);
                Instantiate(product[position], leftHand.transform);//if so, replace the old product

            }
            else if (r == p - 1)
            {

                Destroy(rightHand.transform.GetChild(0).gameObject);
                Instantiate(product[position], rightHand.transform);

            }
        }
           
            else
            {
                if (l == p - 1)//is this product the next step variation?
                {
                    Destroy(leftHand.transform.GetChild(0).gameObject);
                    Instantiate(product[position], leftHand.transform);//if so, replace the old product

                }
                 if (r == p - 1)
                {

                    Destroy(rightHand.transform.GetChild(0).gameObject);
                    Instantiate(product[position], rightHand.transform);

                }
            }
       
            
           
        //Example use: 
        //CupOfJoeStep1 is in player's hand. Player clicks CupOfJoeStep2. This script checks for step one. If it exists in the player's hand, it is instantiated.
    }


    public void instantiateItemAtTransformParent(int position)
    {

        Instantiate(product[position], transform.parent);//instantiate 1
        if (DoubleClick(position))
            Instantiate(product[position], transform.parent);//instantiate 2

    }


    private bool DoubleClick(int position)//has there been a double click?? Skip the animation on the second round
    {
        try
        {
            if (targetQueue.Peek().gameObject.tag == product[position].tag)
            {
                targetQueue.Dequeue();
                return true;
            }
        }
        catch (InvalidOperationException)
        {
            //We're not gonna do anything. This is just telling us that the queue is empty.
        }
        return false;
    }

    public void destroyGameObject()
    {
        Destroy(gameObject);

    }
}
