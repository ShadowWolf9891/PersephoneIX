using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class DamageGun : MonoBehaviour
{
    public float damage;
    public float bulletRange;
    public Transform cameraRoot;
    public void Start()
    {
        cameraRoot = Camera.main.transform;
    }
    
    public void Shoot()
    {
        Ray gunRay = new Ray(cameraRoot.position, cameraRoot.forward);
        if(Physics.Raycast(gunRay, out RaycastHit hitinfo, bulletRange))
        {
            if (hitinfo.collider.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.Health -= damage;
            }
        }
    }

}
