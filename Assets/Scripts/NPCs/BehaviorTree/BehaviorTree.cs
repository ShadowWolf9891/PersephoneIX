using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behavior tree class that contains the root node on creation. Behavior trees are fairly complex, so I suggest researching them
/// if you plan on using them to control AI or NPC actions.
/// </summary>
public class BehaviorTree
{
    private BTNode Root;

    public BehaviorTree(BTNode root)
    { this.Root = root; }

    /// <summary>
    /// Send a tick down the chain of the behavior tree.
    /// </summary>
    public void Tick()
    {
        Root.Tick();
    }
}
