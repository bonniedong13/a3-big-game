// CameraShake.cs
// Attach to: Main Camera
// Briefly jiggles the camera for game juice (e.g. when the player lands).
// The shake is added on top of the camera's position each frame and then
// removed, so it still works while ClimbingGameManager scrolls the camera.

using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public void Shake(float duration, float strength)
    {
        StopAllCoroutines();
        StartCoroutine(ShakeRoutine(duration, strength));
    }

    private IEnumerator ShakeRoutine(float duration, float strength)
    {
        Vector3 lastOffset = Vector3.zero;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            // Shake fades out toward the end.
            float fade = 1f - (elapsed / duration);
            Vector3 newOffset = (Vector3)(Random.insideUnitCircle * strength * fade);

            transform.position += newOffset - lastOffset;
            lastOffset = newOffset;
            yield return null;
        }

        transform.position -= lastOffset;
    }
}
