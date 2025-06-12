using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using EasyBehaviorTree;

namespace EasyBehaviorTreeExample
{
    public class ExampleBehavior : MonoBehaviour
    {
        public BehaviorTree bt; //Assign this to your created behavior tree in the unity editor 
        public TextMeshProUGUI textToChange; //Text to change when something happens.

        private BTBlackboard bb;

        // Start is called before the first frame update
        void Start()
        {
            if (bt == null)
            {
                Debug.LogError("You forgot to assign the behavior tree in the editor!");
            }

            bb = bt.rootNode.GetBlackboard(); //Get the blackboard from the behavior tree

        }

        // Update is called once per frame
        void Update()
        {
            bt.Tick(this.gameObject); //Tick the behavior tree and set scripts attactched to this object as the context.

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SomethingHappened();
			}
            if (Input.GetKeyUp(KeyCode.Space))
            {
                //If we didn't check if space is released, the blackboard value will always be true,
                //even when the game has been closed and reopened.
                SomethingStoppedHappening();
            }

        }

        /// <summary>
        /// The method called by an action node in the behavior tree. Note that it has 0 references,
        /// meaning it is not called directly anywhere in the code.
        /// </summary>
        void DoSomething()
        {
            textToChange.text = "I did something!!";
        }


        //Something happening does not have to be set in the same class as the action node function, but it still needs a reference
        //to whatever blackboard it is changing a value of.

        void SomethingHappened()
        {
            bb.Set<bool>("Something", true); //If something happened, change the blackboard value accordingly.
        }
        void SomethingStoppedHappening()
        {
            bb.Set<bool>("Something", false); //If something stopped happening, change the blackboard value accordingly.
        }

    }
}