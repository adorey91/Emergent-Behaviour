using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Alignment")]
public class AlignmentBehaviour : FilteredFlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent fish, List<Transform> context, Flock flock)
    {
        if (context.Count == 0) // if no neighbors, maintain current alignment
            return fish.transform.up;

        // add all points together and average
        Vector2 alignmentMove = Vector2.zero;

        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(fish, context);
        foreach (Transform item in filteredContext)
        {
            alignmentMove += (Vector2)item.transform.up;
        }

        alignmentMove /= context.Count;
        return alignmentMove;
    }
}
