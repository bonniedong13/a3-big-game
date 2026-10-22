using UnityEngine;

public class SimpleElevationTrigger : MonoBehaviour
{
    public SimpleSky skyManager;
    public float[] heights = new float[] { 10f, 25f, 50f, 75f, 100f, 150f };
    private int index = 0;

    void Update()
    {
        if (skyManager == null || index >= heights.Length) return;
        
        if (transform.position.y >= heights[index])
        {
            skyManager.NextSky();
            index++;
        }
    }
}