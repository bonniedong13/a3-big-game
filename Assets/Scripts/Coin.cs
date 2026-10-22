
using UnityEngine;
 
public class Coin : MonoBehaviour
{
    public static int Coins = 0;
 
    public int Value = 1;
    public float Lifetime = 4f;
 
    [SerializeField] private AudioClip _collectSound;
    [SerializeField] private GameObject _collectEffectPrefab;   
 
    private void Start()
    {
        Destroy(gameObject, Lifetime);
    }
 
    private void OnMouseDown()
    {
        Coins += Value;
        
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
