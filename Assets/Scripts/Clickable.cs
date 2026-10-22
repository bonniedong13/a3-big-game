using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{

    
    public static int Clicks = 0;

    private void OnMouseDown()
    {
        Clicks += 1;  
    }

}
