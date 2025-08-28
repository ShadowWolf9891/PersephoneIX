using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Trigger the end scream behavior when the scream animation ends.
/// </summary>
public class ScreamSMB : StateMachineBehaviour
{
	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		var enemy = animator.GetComponentInParent<EnemyBehavior>();
		if (enemy != null)
			enemy.EndScream();
	}
}
