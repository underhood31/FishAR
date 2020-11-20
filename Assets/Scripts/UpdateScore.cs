using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpdateScore : MonoBehaviour
{
    public Text me;

    void Update()
    {
        me.text="Score: "+GameVariables.score.ToString();
    }
}
