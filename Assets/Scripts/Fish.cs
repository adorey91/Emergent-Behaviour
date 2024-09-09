using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private float swimmingSpeed = 2f;
    private Vector3 targetDirection;

    private void Start()
    {
        targetDirection = Random.insideUnitCircle.normalized;
    }

    private void Update()
    {
        transform.position += targetDirection * swimmingSpeed * Time.deltaTime;

            
    }
    
    private void OnCollisionEnter2D(Collision2D other) 
    {
        Debug.Log(other.gameObject.name);
        if(other.gameObject.name == "GroundTile" || other.gameObject.tag == "Ground")
        {
            Debug.Log("Changing Direction");
            ChangeDirection();
        }
            else
            Debug.Log(other.gameObject.name + ", " + other.gameObject.tag);
    }

    private void ChangeDirection()
    {
        targetDirection = Random.insideUnitCircle.normalized;
    }
}
