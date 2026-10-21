
using UnityEngine;

public class Foothold : MonoBehaviour
{
    private ClimbingGameManager gameManager;

    public void Initialize(ClimbingGameManager manager)
    {
        gameManager = manager;
    }

    private void OnMouseDown()
    {
        if (gameManager != null)
        {
            gameManager.TryClimb(this);
        }
    }
}