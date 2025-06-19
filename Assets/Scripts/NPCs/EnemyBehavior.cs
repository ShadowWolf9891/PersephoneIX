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

    [SerializeField]
    private float rotationSpeed = 5f;


    private BTBlackboard bb;
    private NavMeshAgent agent;

    private GameObject player;

	private void Awake()
	{
        player = GameObject.FindGameObjectWithTag("Player");
		bb = bt.rootNode.GetBlackboard();
        bb.Set<bool>("CanSeePlayer", false);
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
            Debug.Log("Enemy can see the player.");
        }
        else if (!CanSeeTarget(player))
        {
            bb.Set<bool>("CanSeePlayer", false);
		}
    }

    bool CanSeeTarget(GameObject target)
    {
        return VisionUtility.CanSeeTarget(this.gameObject, target, 180f, 100, layerMask);
    }

    public void MoveToPlayer()
    {
        agent.SetDestination(player.transform.position);
        RotateToFacePlayer();
	}

	void RotateToFacePlayer()
	{
		Vector3 direction = (player.transform.position - transform.position).normalized;
		direction.y = 0f; // Ignore vertical difference to keep upright

		if (direction.magnitude > 0.1f)
		{
			Quaternion lookRotation = Quaternion.LookRotation(direction);
			transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
		}
	}

}
