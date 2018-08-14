using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuardController : MonoBehaviour
{
    public List<Transform> targetPositions;
    public int targetPositionIndex = 0;

    public float timeGrannyLastSeen;
    public Boolean followingGranny = false;

    private Seeker seeker;
    private Rigidbody2D rigidbody;

    public Path path;

    public float speed = 2;

    public float nextWaypointDistance = 3;

    private int currentWaypoint = 0;

    public bool reachedEndOfPath;

    public VisionCone vision;

    private bool isDistracted;

    private float distractedTime = 0.0f;

    public float totalDistractedTime;

    public float followTime = 1;

    public void Start()
    {
        

        timeGrannyLastSeen = -100;

        seeker = GetComponent<Seeker>();

        rigidbody = GetComponent<Rigidbody2D>();

        // Start a new path to the targetPosition, call the the OnPathComplete function
        // when the path has been calculated (which may take a few frames depending on the complexity)
        seeker.StartPath(transform.position, targetPositions[0].position, OnPathComplete);
    }

    public void OnPathComplete(Path p)
    {
        Debug.Log("A path was calculated. Did it fail with an error? " + p.error);

        if (!p.error)
        {
            path = p;
            // Reset the waypoint counter so that we start to move towards the first point in the path
            currentWaypoint = 0;
        }
    }

    public void Update()
    {
        if (vision.CanSeePlayer()) timeGrannyLastSeen = Time.time;

        if (path == null)
        {
            // We have no path to follow yet, so don't do anything
            return;
        }

        // Check in a loop if we are close enough to the current waypoint to switch to the next one.
        // We do this in a loop because many waypoints might be close to each other and we may reach
        // several of them in the same frame.
        reachedEndOfPath = false;
        // The distance to the next waypoint in the path
        float distanceToWaypoint;
        while (true)
        {
            // If you want maximum performance you can check the squared distance instead to get rid of a
            // square root calculation. But that is outside the scope of this tutorial.
            distanceToWaypoint = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (distanceToWaypoint < nextWaypointDistance)
            {
                

                // Check if there is another waypoint or if we have reached the end of the path
                if (currentWaypoint + 1 < path.vectorPath.Count)
                {
                    currentWaypoint++;
                }
                else
                {
                    // Set a status variable to indicate that the agent has reached the end of the path.
                    // You can use this to trigger some special code if your game requires that.
                    reachedEndOfPath = true;
                    break;
                }
            }
            else
            {
                break;
            }
        }

        // Slow down smoothly upon approaching the end of the path
        // This value will smoothly go from 1 to 0 as the agent approaches the last waypoint in the path.
        var speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint / nextWaypointDistance) : 1f;

        // Direction to the next waypoint
        // Normalize it so that it has a length of 1 world unit
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        // Multiply the direction by our desired speed to get a velocity
        Vector3 velocity = dir * speed * speedFactor;

        

        if (Time.time - timeGrannyLastSeen < this.followTime)
        {
            followingGranny = true;
            path = null;
            seeker.StartPath(transform.position, GrandmaController.GetInstance().transform.position, OnPathComplete);
        } else if (followingGranny && Time.time - timeGrannyLastSeen >= followTime)
        {
            followingGranny = false;
            path = null;
            seeker.StartPath(transform.position, targetPositions[targetPositionIndex].position, OnPathComplete);
        } else if (isDistracted && reachedEndOfPath)
        {
            velocity = dir * 0;
            if(distractedTime == 0.0f)
            {
                distractedTime = Time.time;
            }
            else if(Time.time - distractedTime >= totalDistractedTime)
            {
                distractedTime = 0.0f;
                isDistracted = false;
                targetPositionIndex--;
                PathToNextMarker();
            }
        } 
        else if (reachedEndOfPath)
        {
            path = null;
            PathToNextMarker();
        }

        // Move the agent
        // Note that SimpleMove takes a velocity in meters/second, so we should not multiply by Time.deltaTime
        GetComponent<Rigidbody2D>().velocity = velocity;
    }

    private void PathToNextMarker()
    {
        targetPositionIndex = ++targetPositionIndex == targetPositions.Count ? 0 : targetPositionIndex;
        seeker.StartPath(transform.position, targetPositions[targetPositionIndex].position, OnPathComplete);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name.Contains("Distraction") && !followingGranny)
        {
            isDistracted = true;
            path = null;
            seeker.StartPath(transform.position, col.transform.position, OnPathComplete);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<GrandmaController>() != null)
        {
            SceneManager.LoadScene("LostScene");
        }
    }
}