using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //put on any enemy to give them starting health

    [SerializeField] private float startingHealth;
    private float health;

    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            Debug.Log(health);

            if (health <=0f)
            {
                //destroys the parent object so that the shot can be recognized by the script and completely gets rid of the object
                Destroy(transform.parent.gameObject);
            }
            
        }
    }

    void Start()
    {
        health = startingHealth;
    }

}
