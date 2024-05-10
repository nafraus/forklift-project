using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public enum BoxType { Red, Blue, Green, Orange };

    [Header("Box Information")]
    [SerializeField]
    private int boxScore = 0;
    public BoxType boxType;

    [Header("Special Values")]
    public int redBoxScoreAmount = 10;
    public int blueBoxScoreAmount = 20;
    public int greenBoxScoreAmount = 30;
    public int orangeBoxScoreAmount = 40;
    public Material[] boxMaterials = new Material[4];
    
    private Renderer boxRenderer;


    //Update box stuff on editor change
    private void OnValidate()
    {
        UpdateBox();
    }


    private void Start()
    {
        UpdateBox();
    }


    //Initialize boxType to random BoxType and then set initial score value
    void UpdateBox()
    {
        boxRenderer = GetComponent<Renderer>();
        switch (boxType)
        {
            case BoxType.Red:
                boxScore = redBoxScoreAmount;
                boxRenderer.material = boxMaterials[0];
                break;

            case BoxType.Blue:
                boxScore = blueBoxScoreAmount;
                boxRenderer.material = boxMaterials[1];
                break;

            case BoxType.Green:
                boxScore = greenBoxScoreAmount;
                boxRenderer.material = boxMaterials[2];
                break;

            case BoxType.Orange:
                boxScore = orangeBoxScoreAmount;
                boxRenderer.material = boxMaterials[3];
                break;

        }
    }

    //Return boxScore for Truck score calculation
    public int GetScoreAmount()
    {
        return boxScore;
    }

    public BoxType GetBoxType()
    {
        return boxType;
    }
}
