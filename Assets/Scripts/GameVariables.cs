using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVariables : MonoBehaviour
{
    public static float stickMaxWeight;
    public static float greenFishWeight;
    public static float yellowFishWeight;
    public static float redFishWeight;
    public static float score;
    void Start()
    {
        stickMaxWeight=10;
        greenFishWeight=7;
        yellowFishWeight=14;
        redFishWeight=21;
        score=0;
    }

   
}
