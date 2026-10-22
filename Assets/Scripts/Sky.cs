using UnityEngine;

public class SimpleSky : MonoBehaviour
{
    public SpriteRenderer skyRenderer; 
    public Sprite[] skySprites;        
    private int currentIndex = 0;

    public void NextSky()
    {
        if (skySprites.Length == 0) return;
        
        currentIndex = (currentIndex + 1) % skySprites.Length;
        skyRenderer.sprite = skySprites[currentIndex];
    }
}