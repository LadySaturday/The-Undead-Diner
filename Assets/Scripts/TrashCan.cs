using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    private BaristaController barista;
    // Start is called before the first frame update
    void Start()
    {
        barista = GameObject.FindGameObjectWithTag("Player").GetComponent<BaristaController>();
    }

    void removeObjectsFromHands()
    {
        Debug.Log("remove");
        barista.removeItems();
    }
}
