using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    [Header("Team Information")]
    [SerializeField]
    private int teamID = 0; //So that each team has a different score

    [SerializeField]
    private int score = 0;

    [Header("Players")]
    [SerializeField]
    private List<GameObject> players; //Should be switched to whatever the player object ends up being (not just gameobjects)


    public void SetID(int id)
    {
        teamID = id;
    }

    public void SetPlayers(List<GameObject> playersToAdd)
    {
        players = playersToAdd;
    }

    public void AddScore(int amount)
    {
        score += amount;
    }

    public void RemoveScore(int amount)
    {
        score -= amount;
    }

    public int GetScore()
    {
        return score;
    }
}
