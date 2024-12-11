using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    public List<FlockAgent> agents = new List<FlockAgent>();
    public LayerMask waterMask;
    public FlockBehaviour behaviour;
    private int minSpawn = 0;
    private int maxSpawn = 100;
    [Range(10, 500)] public int startingCount = 250;
    private const float agentDensity = 0.08f;

    [Range(1f, 100f)] public float driveFactor = 10f;
    [Range(1f, 10f)] public float maxSpeed = 5f;
    [Range(1f, 10f)] public float neighborRadius = 1.5f;
    [Range(0f, 1f)] public float avoidanceRadiusMultiplier = 0.5f;

    private float squareMaxSpeed;
    private float squareNeighborRadius;
    private float squareAvoidanceRadius;

    [SerializeField] private Slider flockSliderSpawn;

    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    private void Start()
    {
        flockSliderSpawn.maxValue = maxSpawn;
        flockSliderSpawn.minValue = minSpawn;
        flockSliderSpawn.value = startingCount;
        //Debug.Log(this.name + ", " + flockSliderSpawn.value);

        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        SpawnFish(startingCount);
    }

    private void Update()
    {
        foreach (FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);

            //FOR DEMO ONLY
            //agent.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / 6f);

            Vector2 move = behaviour.CalculateMove(agent, context, this);
            move *= driveFactor;

            if (move.sqrMagnitude > squareMaxSpeed)
                move = move.normalized * maxSpeed;

            agent.Move(move);
        }
    }

    private List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        // New Line
        //Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius, flockMask);
        // Old Line
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius, ~waterMask);
        startingCount = (int)flockSliderSpawn.value;
        foreach (Collider2D c in contextColliders)
        {
            if (c != agent.AgentCollider)
                context.Add(c.transform);
        }
        return context;
    }




    public void SpawnFish(int count)
    {
        int currentCount = agents.Count;
        for (int i = 0; i < count; i++)
        {
            FlockAgent newFish = Instantiate(
                agentPrefab,
                Random.insideUnitCircle * startingCount * agentDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
                );
            
            newFish.name = agentPrefab.name + currentCount + i;
            newFish.Initialize(this);
            agents.Add(newFish);
        }
    }

    public void UpdateFishCount(int newCount)
    {
        // If we need to add more agents
        if (newCount > agents.Count)
        {
            Actions.OnPlaySFX?.Invoke("AddFish");
            int fishToSpawn = newCount - agents.Count;
            SpawnFish(fishToSpawn);
        }
        // If we need to remove agents
        else if (newCount < agents.Count)
        {
            Actions.OnPlaySFX?.Invoke("RemoveFish");
            int fishToRemove = agents.Count - newCount;
            for (int i = 0; i < fishToRemove; i++)
            {
                FlockAgent agentToRemove = agents[agents.Count - 1]; // Remove from the end
                agents.Remove(agentToRemove);
                Destroy(agentToRemove.gameObject);
            }
        }
    }
}
