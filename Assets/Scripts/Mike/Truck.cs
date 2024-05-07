using UnityEngine;

public class Truck : MonoBehaviour
{
    [Header("Truck Info")]
    [SerializeField]
    private int truckID; //Truck ID should be same as team ID.


    private bool playerInside = false;

    void Start()
    {
        //Generate order list
    }

    private void FixedUpdate()
    {
        CheckOrderCompletion();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if it's the player, set playerInside to true
        //if not, Add Box item to truck list

    }

    private void OnCollisionExit(Collision collision)
    {
        //if it's the player, set playerInside to false
        //if not, remove Box item from truck list
    }

    //Checks to see if truck list only has the boxes required for the order
    //Will not run if player is inside truck (don't want to trap player)
    private void CheckOrderCompletion()
    {
        //If player not in truck
            //Check if the only boxes in the truck are the ones in the order
                //If so, get all box values then add it to team score
    }
}
