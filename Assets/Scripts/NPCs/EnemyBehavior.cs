using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    BehaviorTree bt;
    [SerializeField]
    LayerMask layerMask;
    private BTBlackboard bb;
    private Rigidbody rb;

    private GameObject player;

	private void Awake()
	{
        player = GameObject.FindGameObjectWithTag("Player");
		bb = bt.rootNode.GetBlackboard();
        bb.Set<bool>("CanSeePlayer", false);
        bt.rootNode.ResetStatus();
        rb = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void Update()
    {
        bt.Tick(this.gameObject);

		CheckVision();
    }

    void CheckVision()
    {
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
        Vector3 playerLocation = player.transform.position;
        rb.MovePosition(playerLocation);
        Debug.Log("Moving to Player");
    }
    
}
