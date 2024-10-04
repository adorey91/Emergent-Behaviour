using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Flock : MonoBehaviour
{
    public FlockAgent fishPrefab;
    public List<FlockAgent> allFish = new List<FlockAgent>();
    public FlockBehaviour behaviour;
    private int minSpawn = 10;
    private int maxSpawn = 100;
    [Range(10, 100)] public int startingCount = 50;
    private const float fishDensity = 0.03f;

    [Range(1f, 100f)] public float driveFactor = 10f;
    [Range(1f, 100f)] public float maxSpeed = 5f;
    [Range(1f, 10f)] public float neighborRadius = 1.5f;
    [Range(0f, 1f)] public float avoidanceRadiusMultiplier = 0.5f;

    private float squareMaxSpeed;
    private float squareNeighborRadius;
    private float squareAvoidanceRadius;

    [SerializeField] private Slider flockSliderSpawn;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip sfxSplash;
    [SerializeField] private AudioClip sfxYoink;


    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    private void Start()
    {
        flockSliderSpawn.maxValue = maxSpawn;
        flockSliderSpawn.minValue = minSpawn;
        flockSliderSpawn.value = startingCount;
        Debug.Log(this.name + ", " + flockSliderSpawn.value);
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;


        SpawnFish(startingCount);
    }

    private void Update()
    {
        foreach (FlockAgent agent in allFish)
        {
            List<Transform> context = GetNearbyObjects(agent);
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
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
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
        int currentCount = allFish.Count;
        for (int i = 0; i < count; i++)
        {
            FlockAgent newFish = Instantiate(
                fishPrefab,
                Random.insideUnitCircle * startingCount * fishDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
                );
            
            newFish.name = "Fish " + currentCount + i;
            newFish.Initialize(this);
            allFish.Add(newFish);
        }
    }

    public void UpdateFishCount(int newCount)
    {

        // If we need to add more fish
        if (newCount > allFish.Count)
        {
            sfxSource.PlayOneShot(sfxSplash);
            int fishToSpawn = newCount - allFish.Count;
            SpawnFish(fishToSpawn);
        }
        // If we need to remove fish
        else if (newCount < allFish.Count)
        {
            sfxSource.PlayOneShot(sfxYoink);
            int fishToRemove = allFish.Count - newCount;
            for (int i = 0; i < fishToRemove; i++)
            {
                FlockAgent agentToRemove = allFish[allFish.Count - 1]; // Remove from the end
                allFish.Remove(agentToRemove);
                Destroy(agentToRemove.gameObject);
            }
        }
    }
}
