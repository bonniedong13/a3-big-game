using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for changing scenes

public class MainMenuController : MonoBehaviour
{
    [Header("Background Settings")]
    [SerializeField] private Transform backgroundSprite;
    [SerializeField] private Vector3 startingPosition = new Vector3(20f, 0f, 0f); // Off-screen right
    [SerializeField] private Vector3 centerPosition = new Vector3(0f, 0f, 0f);    // Centered screen
    [SerializeField] private float slideDuration = 2.0f;                          // Time in seconds to slide

    [Header("Scene Management")]
    [SerializeField] private string nextSceneName; // Name of the scene to load

    private bool isTransitioning = false;

    private void Start()
    {
        // Force the background to its starting off-screen position when the game starts
        if (backgroundSprite != null)
        {
            backgroundSprite.position = startingPosition;
        }
    }

    // Call this function via your UI Button's OnClick event
    public void StartGameSequence()
    {
        Debug.Log("Button was clicked successfully!");

        // Prevent clicking multiple times
        if (isTransitioning) return;
        isTransitioning = true;

        // Start the smooth movement and scene swap sequence
        StartCoroutine(MoveBackgroundAndLoadScene());
    }

    private IEnumerator MoveBackgroundAndLoadScene()
    {
        float elapsedTime = 0f;

        // Smoothly move from startingPosition to centerPosition over time
        while (elapsedTime < slideDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / slideDuration;

            // Use SmoothStep to give it a nice "slow down as it hits center" feel
            t = Mathf.SmoothStep(0f, 1f, t);

            backgroundSprite.position = Vector3.Lerp(startingPosition, centerPosition, t);
            yield return null; // Wait for the next frame
        }

        // Ensure it lands exactly on the center position
        backgroundSprite.position = centerPosition;

        // Wait an optional brief moment at the center before switching scenes
        yield return new WaitForSeconds(0.2f);

        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }
}
