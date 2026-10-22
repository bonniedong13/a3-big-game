using TMPro;
using UnityEngine;
 
public class ClimateManager : MonoBehaviour
{
    [System.Serializable]
    public class Zone
    {
        public string name = "Sunny";
        public float minElevation;               
        public Color skyColor = Color.cyan;
        public GameObject backgroundObject;      
        public ParticleSystem weather;           
    }
 
    [SerializeField] private Transform player;
    [SerializeField] private Camera targetCamera;   
    [SerializeField] private Zone[] zones;          
    [SerializeField] private float fadeSpeed = 2f;
    [SerializeField] private TMP_Text zoneLabel;    
 
    private int currentZone = -1;
    private float _startY;
    private Color _targetColor;
 
    private void Start()
    {
        if (targetCamera == null) targetCamera = Camera.main;
        if (targetCamera == null || player == null || zones.Length == 0)
        {
            Debug.LogWarning("ClimateManager is missing a camera, player or zones.");
            enabled = false;
            return;
        }
 
        _startY = player.position.y;
        _targetColor = targetCamera.backgroundColor;
        UpdateZone();
    }
 
    private void Update()
    {
        UpdateZone();
        
        targetCamera.backgroundColor = Color.Lerp(
            targetCamera.backgroundColor, _targetColor, fadeSpeed * Time.deltaTime);
    }
 
    private int FindZoneIndex()
    {
        int index = 0;
        for (int i = 0; i < zones.Length; i++)
        {
            if (player.position.y - _startY >= zones[i].minElevation) index = i;
        }
        return index;
    }
 
    private void UpdateZone()
    {
        int index = FindZoneIndex();
        if (index == currentZone) return;
 
        currentZone = index;
        _targetColor = zones[index].skyColor;
        if (zoneLabel != null) zoneLabel.text = zones[index].name;
        ShowOnlyZone(index);
    }
 
    private void ShowOnlyZone(int activeIndex)
    {
        for (int i = 0; i < zones.Length; i++)
        {
            bool isActive = (i == activeIndex);
 
            if (zones[i].backgroundObject != null)
            {
                zones[i].backgroundObject.SetActive(isActive);
            }
            if (zones[i].weather != null)
            {
                if (isActive) zones[i].weather.Play();
                else zones[i].weather.Stop();
            }
        }
    }
}
