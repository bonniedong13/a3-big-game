// CoinSpawner.cs
// Attach to: an empty object (e.g. under the "Main Scripts" object)
// Instantiates the coin prefab at a random on-screen spot every few seconds.

using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private float spawnInterval = 2.5f;
    [SerializeField] private Transform coinParent;   // optional empty object to keep the Hierarchy tidy

    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnCoin();
        }
    }

    private void SpawnCoin()
    {
        // Pick a random point inside the camera's view (viewport is 0..1 on both axes),
        // so coins always appear on screen even as the camera scrolls up.
        Vector3 viewportPoint = new Vector3(Random.Range(0.1f, 0.9f), Random.Range(0.2f, 0.8f), 0f);
        Vector3 spawnPosition = Camera.main.ViewportToWorldPoint(viewportPoint);
        spawnPosition.z = 0f;

        Instantiate(coinPrefab, spawnPosition, Quaternion.identity, coinParent);
    }
}
