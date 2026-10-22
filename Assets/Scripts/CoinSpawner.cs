using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private Transform coinParent; // optional, keeps the Hierarchy tidy
    [SerializeField] private float spawnInterval = 2.5f;
    [SerializeField] private float heightAboveStep = 0.6f;

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= spawnInterval)
        {
            _timer = 0f;
            SpawnCoinOnStep();
        }
    }

    private void SpawnCoinOnStep()
    {
        Foothold[] steps = FindObjectsByType<Foothold>(FindObjectsSortMode.None);
        Foothold[] candidates = new Foothold[steps.Length];
        int count = 0;

        foreach (Foothold step in steps)
        {
            if (IsGoodSpot(step)) candidates[count++] = step;
        }

        if (count == 0) return;

        Foothold chosen = candidates[Random.Range(0, count)];
        Vector3 position = chosen.transform.position + Vector3.up * heightAboveStep;

        // Don't stack a second coin on the same step.
        Collider2D hit = Physics2D.OverlapCircle(position, 0.3f);
        if (hit != null && hit.GetComponent<Coin>() != null) return;

        Instantiate(coinPrefab, position, Quaternion.identity, coinParent);
    }

    // A step is a good spot if it is visible and not far below the player.
    private bool IsGoodSpot(Foothold step)
    {
        Vector3 view = Camera.main.WorldToViewportPoint(step.transform.position);
        bool onScreen = view.x > 0.05f && view.x < 0.95f && view.y > 0.1f && view.y < 0.95f;
        bool notBehindPlayer = step.transform.position.y > player.position.y - 0.5f;
        return onScreen && notBehindPlayer;
    }
}