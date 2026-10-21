
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClimbingGameManager : MonoBehaviour
{
    [Header("References")]
    public GameObject footholdPrefab;
    public ClimbingPlayer player;
    public Camera mainCamera;
    public TMP_Text elevationText;
    public GameObject gameOverPanel;

    [Header("Foothold Generation")]
    public float verticalSpacing = 2f;
    public float horizontalRange = 2.5f;
    public float firstSpawnHeight = 2f;
    public int startingFootholds = 8;
    public float spawnAheadDistance = 12f;

    [Header("Camera")]
    public float cameraScrollSpeed = 0.5f;

    [Header("Screen Edge")]
    [Range(0.01f, 0.15f)]
    public float edgeMargin = 0.04f;

    private readonly List<Foothold> footholds =
        new List<Foothold>();

    private int nextFootholdIndex = 0;
    private float nextSpawnY;
    private float startingHeight;
    private bool gameOver;

    public bool IsGameOver => gameOver;

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        startingHeight = player.transform.position.y;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        // Start spawning above the player.
        nextSpawnY =
            player.transform.position.y + firstSpawnHeight;

        // Create several footholds at the beginning.
        for (int i = 0; i < startingFootholds; i++)
        {
            SpawnFoothold();
        }
    }

    private void Update()
    {
        if (gameOver) return;

        ScrollCamera();
        SpawnAhead();
        UpdateElevation();
        CheckScreenEdges();
        RemoveOldFootholds();
    }

    private void ScrollCamera()
    {
        Vector3 position = mainCamera.transform.position;

        position.y += cameraScrollSpeed * Time.deltaTime;

        mainCamera.transform.position = position;
    }

    private void SpawnAhead()
    {
        float cameraTop =
            mainCamera.transform.position.y
            + mainCamera.orthographicSize;

        float spawnLimit = cameraTop + spawnAheadDistance;

        while (nextSpawnY < spawnLimit)
        {
            SpawnFoothold();
        }
    }

    private void SpawnFoothold()
    {
        float halfWidth =
            mainCamera.orthographicSize * mainCamera.aspect;

        float spawnRange = Mathf.Min(
            horizontalRange,
            Mathf.Max(0f, halfWidth - 0.8f)
        );

        float randomX = Random.Range(
            -spawnRange, spawnRange
        );

        Vector3 position = new Vector3(
            randomX,
            nextSpawnY,
            0f
        );

        GameObject obj = Instantiate(
            footholdPrefab,
            position,
            Quaternion.identity
        );

        Foothold foothold = obj.GetComponent<Foothold>();
        foothold.Initialize(this);

        footholds.Add(foothold);

        nextSpawnY += verticalSpacing;
    }


    public void TryClimb(Foothold clickedFoothold)
    {
        if (gameOver || player.IsMoving)
            return;

        // Skip any destroyed footholds at the front of the queue.
        while (nextFootholdIndex < footholds.Count &&
               footholds[nextFootholdIndex] == null)
        {
            nextFootholdIndex++;
        }

        // Only the exact next foothold can be clicked.
        if (nextFootholdIndex >= footholds.Count)
            return;

        if (clickedFoothold != footholds[nextFootholdIndex])
            return;

        Foothold target = footholds[nextFootholdIndex];
        nextFootholdIndex++;

        player.ClimbTo(target.transform.position, () =>
        {
            if (gameOver) return;

            Destroy(target.gameObject);
        });
    }

    private void UpdateElevation()
    {
        float elevation = Mathf.Max(
            0f,
            player.transform.position.y - startingHeight
        );

        if (elevationText != null)
        {
            elevationText.text =
                "Elevation: "
                + Mathf.FloorToInt(elevation)
                + " m";
        }
    }

    private void CheckScreenEdges()
    {
        Vector3 screenPosition =
            mainCamera.WorldToViewportPoint(
                player.transform.position
            );

        if (screenPosition.x <= edgeMargin ||
            screenPosition.x >= 1f - edgeMargin ||
            screenPosition.y <= edgeMargin ||
            screenPosition.y >= 1f - edgeMargin)
        {
            EndGame();
        }
    }


    private void RemoveOldFootholds()
    {
        float cameraBottom =
            mainCamera.transform.position.y -
            mainCamera.orthographicSize;

        for (int i = 0; i < footholds.Count; i++)
        {
            Foothold foothold = footholds[i];

            if (foothold == null)
                continue;

            // Only remove footholds that have already been passed.
            bool alreadyUsed = i < nextFootholdIndex;

            if (alreadyUsed &&
                foothold.transform.position.y < cameraBottom - 3f)
            {
                Destroy(foothold.gameObject);
                footholds[i] = null;
            }
        }
    }

    private void EndGame()
    {
        if (gameOver) return;

        gameOver = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Debug.Log("Game Over!");
    }
}