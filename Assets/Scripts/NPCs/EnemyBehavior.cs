using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyBehaviorTree;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    BehaviorTree bt;
    [SerializeField]
    LayerMask layerMask;

	[Header("Patrol Settings")]
	[SerializeField] List<Transform> patrolPoints = new();
	[SerializeField] float timeAtEachPatrolPoint = 2f;


	[Header("Search Settings")]
	[SerializeField] float searchRadius = 5f;
	[SerializeField] int numberOfSearchPoints = 3;
	[SerializeField] float timeAtEachPoint = 2f;

    private BTBlackboard bb;
    private NavMeshAgent agent;
    private GameObject player;
	private float rotationSpeed = 0.05f;

	List<Vector3> searchPoints = new();
	private bool alertedToPlayer = false;
	private float waitTimer;
	private int currentPointIndex;
	private Vector3 lastKnownPlayerPosition;


	private void Awake()
	{
        player = GameObject.FindGameObjectWithTag("Player");
		bb = bt.rootNode.GetBlackboard();
        bb.Set<bool>("CanSeePlayer", false);
        bb.Set<bool>("LostPlayer", false);
        bt.rootNode.ResetStatus();
        agent = GetComponent<NavMeshAgent>();
		
	}

    // Update is called once per frame
    void Update()
    {
        bt.Tick(this.gameObject);
        CheckVision();
    }

    void CheckVision()
    {
        if(player == null) { return; }
		if (CanSeeTarget(player))
        {
            bb.Set<bool>("CanSeePlayer", true);
            alertedToPlayer = true;
            //Debug.Log("Enemy can see the player.");
        }
        else if (!CanSeeTarget(player) && alertedToPlayer)
        {
            bb.Set<bool>("LostPlayer", true);
            bb.Set<bool>("CanSeePlayer", false);
			agent.updateRotation = true;
			waitTimer = 0f;
			currentPointIndex = 0;
			lastKnownPlayerPosition = player.transform.position;
			// Generate search points around the last seen location
			searchPoints = GenerateSearchPoints(lastKnownPlayerPosition, searchRadius, numberOfSearchPoints);
			if (searchPoints.Count > 0)
			{
				agent.SetDestination(searchPoints[0]);
			}
			else
			{
				Debug.Log("No valid search points found — skipping search.");
				EndSearch();
			}
		}
    }

	

	bool CanSeeTarget(GameObject target)
    {
        return VisionUtility.CanSeeTarget(this.gameObject, target, 180f, 100, layerMask);
    }

    public void MoveToPlayer()
    {
        agent.SetDestination(player.transform.position);
        //RotateToFacePlayer();
	}

	void RotateToFacePlayer()
	{
		//agent.updateRotation = false; //Manually update rotation
		//Vector3 direction = (player.transform.position - transform.position).normalized;
		//direction.y = 0f;

		//Quaternion lookRotation = Quaternion.LookRotation(direction);
		//transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.fixedDeltaTime);
		
	}

    private void Search()
    {
		if (!alertedToPlayer || searchPoints.Count == 0)
			return;

		if (agent.remainingDistance <= agent.stoppingDistance)
		{
			waitTimer += Time.deltaTime;

			if (waitTimer >= timeAtEachPoint)
			{
				currentPointIndex++;

				if (currentPointIndex < searchPoints.Count)
				{
					agent.SetDestination(searchPoints[currentPointIndex]);
					waitTimer = 0f;
				}
				else
				{
					EndSearch();
				}
			}
		}
	}

    public void Patrol()
    {
		if (alertedToPlayer || patrolPoints.Count == 0)
			return;

		if (agent.remainingDistance <= agent.stoppingDistance)
		{
			waitTimer += Time.deltaTime;

			if (waitTimer >= timeAtEachPatrolPoint)
			{
				currentPointIndex++;
				waitTimer = 0f;

				if (currentPointIndex >= patrolPoints.Count)
				{
					currentPointIndex = 0;
				}

				agent.SetDestination(patrolPoints[currentPointIndex].position);
			}
		}
	}
	bool TryGetNavMeshPoint(Vector3 targetPosition, float maxDistance, out Vector3 navMeshPosition)
	{
		NavMeshHit hit;
		if (NavMesh.SamplePosition(targetPosition, out hit, maxDistance, NavMesh.AllAreas))
		{
			navMeshPosition = hit.position;
			return true;
		}

		navMeshPosition = Vector3.zero;
		return false;
	}
	List<Vector3> GenerateSearchPoints(Vector3 origin, float radius, int count)
	{
		List<Vector3> points = new List<Vector3>();

		for (int i = 0; i < count; i++)
		{
			Vector2 randomCircle = Random.insideUnitCircle * radius;
			Vector3 candidate = origin + new Vector3(randomCircle.x, 0f, randomCircle.y);

			if (TryGetNavMeshPoint(candidate, 2f, out Vector3 validPoint))
			{
				points.Add(validPoint);
			}
			else
			{
				Debug.LogWarning($"Could not find NavMesh point near {candidate}");
			}
		}

		return points;
	}
	private void EndSearch()
	{
		alertedToPlayer = false;
		bb.Set<bool>("LostPlayer", false);
		bb.Set<bool>("CanSeePlayer", false);
		waitTimer = 0f;
		currentPointIndex = 0;

		if (patrolPoints.Count > 0)
		{
			agent.SetDestination(patrolPoints[0].position);
		}
	}
}
