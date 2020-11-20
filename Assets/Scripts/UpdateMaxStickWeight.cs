using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateMaxStickWeight : MonoBehaviour
{
    public Text me;
    void Update()
    {
        me.text="MaxWt: "+GameVariables.stickMaxWeight.ToString();  
    }
}
