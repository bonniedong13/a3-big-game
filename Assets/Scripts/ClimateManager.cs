// ClimateManager.cs
// Attach to: an empty object (e.g. "Main Scripts")
// Changes the sky color (and optional background / weather particles)
// depending on how high the player has climbed.

// ClimateManager.cs
// Attach to: an empty object (e.g. "Main Scripts")
// Changes the sky color (and optional background / weather particles)
// depending on how high the player has climbed.

using UnityEngine;

public class ClimateManager : MonoBehaviour
{
    [System.Serializable]
    public class Zone
    {
        public string name = "Foothills";
        public float minElevation;               // world Y where this zone starts
        public Color skyColor = Color.cyan;
        public GameObject backgroundObject;      // optional, e.g. background1_0
        public ParticleSystem weather;           // optional, e.g. snow (Play On Awake OFF)
    }

    [SerializeField] private Transform player;
    [SerializeField] private Camera targetCamera;   // set Background Type to Solid Color
    [SerializeField] private Zone[] zones;          // order them lowest to highest
    [SerializeField] private float fadeSpeed = 2f;

    private int _currentZone = -1;
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

        _targetColor = targetCamera.backgroundColor;
        UpdateZone();
    }

    private void Update()
    {
        UpdateZone();

        // Smoothly blend toward the current zone's color.
        targetCamera.backgroundColor = Color.Lerp(
            targetCamera.backgroundColor, _targetColor, fadeSpeed * Time.deltaTime);
    }

    private int FindZoneIndex()
    {
        int index = 0;
        for (int i = 0; i < zones.Length; i++)
        {
            if (player.position.y >= zones[i].minElevation) index = i;
        }
        return index;
    }

    private void UpdateZone()
    {
        int index = FindZoneIndex();
        if (index == _currentZone) return;

        _currentZone = index;
        _targetColor = zones[index].skyColor;
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
