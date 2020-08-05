using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOrder : MonoBehaviour
{
    private GameObject GameManager;
    private void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameController");
    }
    public void checkOrder(int leftOrRight)
    {

        GameManager.GetComponent<DoubleOrderManager>().CheckOrder(leftOrRight);
    }
}
