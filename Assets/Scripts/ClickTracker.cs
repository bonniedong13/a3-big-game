using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickTracker : MonoBehaviour
{

    TextMeshProUGUI _trackingText;

    
    private void Awake()
    {
        _trackingText = GetComponent<TextMeshProUGUI>();
    }

    public void Update()
    {
        
        _trackingText.text = "Clicks: " + Clickable.Clicks;
    }

}
