using UnityEngine;

public class KeyboardClimb : MonoBehaviour
{
    [SerializeField] private ClimbingPlayer player;
    [SerializeField] private float rowTolerance = 0.5f;  

    private void Update()
    {
        if (player == null || player.IsMoving) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) ClimbToward(-1);
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) ClimbToward(1);
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)
                 || Input.GetKeyDown(KeyCode.Space)) ClimbToward(0);
    }

    private void ClimbToward(int direction)
    {
        Foothold[] steps = FindObjectsByType<Foothold>(FindObjectsSortMode.None);
        float rowY = FindNextRowHeight(steps);
        if (rowY == float.MaxValue) return;

        Foothold best = null;
        float bestScore = float.MaxValue;

        foreach (Foothold step in steps)
        {
            if (Mathf.Abs(step.transform.position.y - rowY) > rowTolerance) continue;

            float score = ScoreStep(step, direction);
            if (score < bestScore)
            {
                bestScore = score;
                best = step;
            }
        }
        
        if (best != null) best.SendMessage("OnMouseDown", SendMessageOptions.DontRequireReceiver);
    }
    
    private float FindNextRowHeight(Foothold[] steps)
    {
        float playerY = player.transform.position.y;
        float lowest = float.MaxValue;

        foreach (Foothold step in steps)
        {
            float y = step.transform.position.y;
            if (y > playerY + 0.1f && y < lowest) lowest = y;
        }
        return lowest;
    }
    
    private float ScoreStep(Foothold step, int direction)
    {
        float dx = step.transform.position.x - player.transform.position.x;
        float score = Mathf.Abs(dx);

        if (direction != 0 && Mathf.Sign(dx) != direction) score += 100f;
        return score;
    }
}
