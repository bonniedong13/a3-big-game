// ElevationBar.cs
// Attach to: the bar object on the Canvas.
// Fills a vertical bar based on how close the player is to the summit.
// Setup: Image Type = Filled, Fill Method = Vertical.
// Anchor the bar to the right edge (shift-click the anchor preset so the
// pivot matches) so it hugs the side at every resolution.

using UnityEngine;
using UnityEngine.UI;

public class ElevationBar : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Image fillImage;
    [SerializeField] private float summitElevation = 120f;

    private float _startElevation;

    private void Start()
    {
        _startElevation = player.position.y;
    }

    private void Update()
    {
        float progress = (player.position.y - _startElevation)
                         / (summitElevation - _startElevation);
        fillImage.fillAmount = Mathf.Clamp01(progress);
    }
}
