using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Stay In Camera Bounds")]
public class StayInCameraBoundsBehaviour : FlockBehaviour
{
    public float boundaryBuffer = 5f; // How far agents can go beyond the camera bounds

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogWarning("Main camera not found!");
            return Vector2.zero;
        }

        // Get camera world bounds
        Vector3 screenBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 screenTopRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        // Calculate bounds with buffer
        float minX = screenBottomLeft.x - boundaryBuffer;
        float maxX = screenTopRight.x + boundaryBuffer;
        float minY = screenBottomLeft.y - boundaryBuffer;
        float maxY = screenTopRight.y + boundaryBuffer;

        Vector2 agentPosition = agent.transform.position;
        Vector2 move = Vector2.zero;

        // Check if the agent is out of bounds and calculate a move back towards the center of the bounds
        if (agentPosition.x < minX)
            move.x = minX - agentPosition.x;
        else if (agentPosition.x > maxX)
            move.x = maxX - agentPosition.x;

        if (agentPosition.y < minY)
            move.y = minY - agentPosition.y;
        else if (agentPosition.y > maxY)
            move.y = maxY - agentPosition.y;

        return move;
    }
}
