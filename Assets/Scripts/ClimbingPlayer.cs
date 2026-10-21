
using System;
using System.Collections;
using UnityEngine;

public class ClimbingPlayer : MonoBehaviour
{
    public float climbDuration = 0.35f;

    public bool IsMoving { get; private set; }

    public void ClimbTo(Vector3 destination, Action onComplete)
    {
        if (IsMoving) return;

        StopAllCoroutines();
        StartCoroutine(MoveToPosition(destination, onComplete));
    }

    private IEnumerator MoveToPosition(
        Vector3 destination,
        Action onComplete)
    {
        IsMoving = true;

        Vector3 start = transform.position;
        destination.z = start.z;

        float elapsed = 0f;

        while (elapsed < climbDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / climbDuration);

            // Smooth movement with a small climbing arc.
            float arc = Mathf.Sin(t * Mathf.PI) * 0.5f;

            transform.position = Vector3.Lerp(
                start, destination, t
            ) + Vector3.up * arc;

            yield return null;
        }

        transform.position = destination;
        IsMoving = false;

        onComplete?.Invoke();
    }
}