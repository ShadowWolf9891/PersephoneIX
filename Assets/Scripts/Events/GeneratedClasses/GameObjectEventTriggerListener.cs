using UnityEngine;

[AddComponentMenu("Events/Listeners/GameObjectEventTriggerListener")]
public class GameObjectEventTriggerListener : GameEventListener<UnityEngine.GameObject>
{
    public override void OnEventRaised(GameObject value)
    {
        if (value == gameObject)
        {
            base.OnEventRaised(value);
        }
    }
}