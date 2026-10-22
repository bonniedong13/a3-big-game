using UnityEngine;
using UnityEngine.UI;
 
public class ElevationBar : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Image fillImage;
    [SerializeField] private float summitElevation = 60f;   // height above the start that fills the bar
 
    private float _startY;
 
    private void Start()
    {
        _startY = player.position.y;
    }
 
    private void Update()
    {
        float climbed = player.position.y - _startY;
        fillImage.fillAmount = Mathf.Clamp01(climbed / summitElevation);
    }
}