// Coin.cs
// Attach to: Coin prefab (needs a SpriteRenderer + Collider2D)
// Click it to collect it. If the player is too slow, it disappears.using UnityEngine;
 
using UnityEngine;
 
public class Coin : MonoBehaviour
{
    // Static, like Clickable.Clicks, so any script (shop, UI) can read Coin.Coins.
    public static int Coins = 0;
 
    public int Value = 1;
    public float Lifetime = 4f;
 
    [SerializeField] private AudioClip _collectSound;
    [SerializeField] private GameObject _collectEffectPrefab;   // optional particle burst
 
    private void Start()
    {
        Destroy(gameObject, Lifetime);
    }
 
    private void OnMouseDown()
    {
        Coins += Value;
 
        // PlayClipAtPoint keeps playing after this object is destroyed.
        // (An AudioSource on a destroyed object would cut the sound off.)
        if (_collectSound != null)
        {
            AudioSource.PlayClipAtPoint(_collectSound, transform.position);
        }
        if (_collectEffectPrefab != null)
        {
            Instantiate(_collectEffectPrefab, transform.position, Quaternion.identity);
        }
 
        Destroy(gameObject);
    }
}
