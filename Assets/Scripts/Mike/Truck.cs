using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{
    public ScoreManager scoreManager;

    [Header("Truck Info")]
    [SerializeField]
    private int truckID; //Truck ID should be same as team ID.

    [SerializeField]
    private int orderLength = 3;

    [SerializeField]
    private List<Box.BoxType> order;

    [SerializeField]
    private List<Box.BoxType> boxTypesInside = new List<Box.BoxType>();

    private List<GameObject> boxGameObjectsInside = new List<GameObject>();

    [SerializeField]
    private int scoreToAward = 0;

    private bool playerInside = false;

    void Start()
    {
        GenerateOrder();
    }

    //Adds boxtype to boxesInside if box. If player, makes playerInside true
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered truck");
            playerInside = true;
            return;
        }

        else if (other.CompareTag("Box"))
        {
            Debug.Log("Added box to insideTruck");
            boxGameObjectsInside.Add(other.gameObject);
            Box box = other.GetComponent<Box>();
            boxTypesInside.Add(box.GetBoxType());
            scoreToAward += box.GetScoreAmount();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left truck");
            playerInside = false;
            return;
        }

        else if (other.CompareTag("Box"))
        {
            boxGameObjectsInside.Remove(other.gameObject);
            Box box = other.GetComponent<Box>();
            boxTypesInside.Remove(box.GetBoxType());
            scoreToAward -= box.GetScoreAmount();
        }
        
    }

    public void SetTruckOrderLength(int num)
    {
        orderLength = num;
    }

    //Generates order list of boxTypes
    private void GenerateOrder()
    {
        order.Clear();

        for (int i = 0; i < orderLength; i++)
        {
            //Jesus, look at this bad boy. Totally didn't google how to do this
            order.Add((Box.BoxType)Random.Range(0, System.Enum.GetValues(typeof(Box.BoxType)).Length));
        }
    }

    //Checks to see if truck list only has the boxes required for the order
    //Will not run if player is inside truck (don't want to trap player)
    private void CheckOrderCompletion()
    {
        if (boxTypesInside.Count == order.Count)
        {
            //Sort the lists, then check if they match
            boxTypesInside.Sort();
            order.Sort();

            bool correctOrder = true;
            for (int i = 0; i < boxTypesInside.Count; i++)
            {
                if (boxTypesInside[i] != order[i])
                {
                    correctOrder = false;
                    break;
                }
            }

            if (correctOrder)
            {
                OrderCompleted();
            }

            else
            {
                Debug.Log("Order is incorrect");
            }
        }

        else
        {
            Debug.Log("Count of boxes doesn't match");
        }
    }

    private void OrderCompleted()
    {
        Debug.Log($"Order is complete!\nScore to award: {scoreToAward}");

        //Call scoremanager's score thing with truck id and scoreToAward

        //Wipe inside list and destroy all boxes inside
        scoreToAward = 0;
        boxTypesInside.Clear();
        foreach (GameObject go in boxGameObjectsInside)
        {
            Destroy(go);
        }

        //Make new order
        GenerateOrder();
    }

    //Changes Color of truck when mouse is over it
    private void OnMouseOver()
    {
        //Too much effort to do this with multiple meshes, tbh
        //Don't debug log this shit, it gets called every frame
    }

    //When Player clicks on truck, check for order completion
    private void OnMouseDown()
    {
        Debug.Log("Checking Order Completion");
        if (playerInside)
        {
            Debug.Log("Player is inside truck. Will not check order");
            return;
        }

        CheckOrderCompletion();
    }


}
