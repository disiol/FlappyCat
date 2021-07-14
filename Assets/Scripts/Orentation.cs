using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orentation : MonoBehaviour
{
    public string orientations;

    // Start is called before the first frame update
    void Start()
    {
        
        if (orientations == "Landscape")
        {
            Screen.orientation = ScreenOrientation.Landscape;
        }
        else
        {
            Screen.orientation = ScreenOrientation.Portrait;
 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
