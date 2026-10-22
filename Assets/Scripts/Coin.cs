// Coin.cs
// Attach to: Coin prefab (needs a SpriteRenderer + Collider2D)
// Click it to collect it. If the player is too slow, it disappears.using UnityEngine;
 
using UnityEngine;
 
public class Coin : MonoBehaviour
{
    // Static, like Clickable.Clicks, so any script (shop, UI) can read Coin.Coins.
    public static int Coins = 0;
 
    public int value = 1;
    public float lifetime = 4f;
 
    [SerializeField] private AudioClip collectSound;
    [SerializeField] private GameObject collectEffectPrefab;   // optional particle burst
 
    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
 
    private void Update()
    {
        // Ride down with the footholds so the coin stays on its step.
        transform.position += Vector3.down * StepScroller.Speed * Time.deltaTime;
    }
 
    private void OnMouseDown()
    {
        Coins += value;
 
        // PlayClipAtPoint keeps playing after this object is destroyed.
        // (An AudioSource on a destroyed object would cut the sound off.)
        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        }
        if (collectEffectPrefab != null)
        {
            Instantiate(collectEffectPrefab, transform.position, Quaternion.identity);
        }
 
        Destroy(gameObject);
    }
}
