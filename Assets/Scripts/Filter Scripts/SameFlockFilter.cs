using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Same Flock")]
public class SameFlockFilter : ContextFilter
{
    public LayerMask flockLayerMask;
    public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {
        flockLayerMask = agent.layerMask;

        List<Transform> filtered = new List<Transform>();
        foreach (Transform item in original)
        {
            // Check if the item's layer is included in the flockLayerMask
            if (((1 << item.gameObject.layer) & flockLayerMask.value) != 0)
            {
                //Debug.Log("Same Flock: " + item.name);
                filtered.Add(item);
            }
        }
        return filtered;
    }
}
