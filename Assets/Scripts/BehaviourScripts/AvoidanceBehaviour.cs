using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidance")]
public class AvoidanceBehaviour : FilteredFlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if (context.Count == 0) // if no neighbors, return no adjustments
            return Vector2.zero;

        //add all points together and average
        Vector2 avoidanceMove = Vector2.zero;
        int nAvoid = 0;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        
        foreach (Transform item in filteredContext)
        {
            Vector3 closestPoint = item.gameObject.GetComponent<Collider2D>().ClosestPoint(agent.transform.position);

            if (Vector2.SqrMagnitude(closestPoint - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                nAvoid++;
                avoidanceMove += (Vector2)(agent.transform.position - closestPoint);
            }
        }
        if (nAvoid > 0)
            avoidanceMove /= nAvoid;

        return avoidanceMove;
    }
}
