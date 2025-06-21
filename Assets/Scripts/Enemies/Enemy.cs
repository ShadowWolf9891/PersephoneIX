using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

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
                Destroy(transform.parent.gameObject);
            }
            
        }
    }

    void Start()
    {
        health = startingHealth;
    }

}
