using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class soundPistol : MonoBehaviour
{
    [SerializeField]
    AudioSource pistolShot;

    public void Start()
    {
        pistolShot = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void PistolSound()
    {
        pistolShot.Play();
    }
}
