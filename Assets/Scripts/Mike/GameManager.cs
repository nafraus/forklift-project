using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    
    [SerializeField]
    private List<Team> teams = new List<Team>(); //List of Team objects

    void Start()
    {
        for (int i = 0; i < teams.Count - 1; i++)
        {
            teams[i] = new Team();
            teams[i].SetID(i);
            //teams[i].SetPlayers(); //IDK how we are getting players set up
        }
    }
}
