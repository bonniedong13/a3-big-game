using UnityEngine;

public class MovingStep : MonoBehaviour
{
    public float baseSpeed = 2f;
    public float speedIncreasePerSecond = 0.5f;
    public float maxSpeed = 10f;

    public float recycleY = -6f;
    public float gap = 2f;
    public float topY = 6f;
    public float minX = -10f;
    public float maxX = 10f;

    void Update()
    {
        // All steps use the same gradually increasing speed.
        float speed = Mathf.Min(
            baseSpeed + Time.timeSinceLevelLoad * speedIncreasePerSecond,
            maxSpeed
        );

        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (transform.position.y < recycleY)
        {
            float highestY = FindHighestStepY();

            transform.position = new Vector2(
                Random.Range(minX, maxX),
                Mathf.Max(topY, highestY + gap)
            );
        }
    }

    float FindHighestStepY()
    {
        float highestY = transform.position.y;

        foreach (MovingStep step in FindObjectsByType<MovingStep>(FindObjectsSortMode.None))
        {
            if (step != this && step.transform.position.y > highestY)
                highestY = step.transform.position.y;
        }

        return highestY;
    }
}
