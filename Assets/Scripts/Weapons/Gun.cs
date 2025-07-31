using UnityEngine.Events;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public UnityEvent onGunShoot;
    public float fireCooldown;

    public bool isAutomatic;

    private float currentnCooldown;

    void Start()
    {
        currentnCooldown = fireCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        //if the gun is automatic you can hold down mouse 1 to shoot
        if (isAutomatic)
        {
            if (Input.GetMouseButton(0))
            {
                if(currentnCooldown <= 0f)
                {
                    //invokes everything you set for when the gun shoots
                    onGunShoot?.Invoke();
                    currentnCooldown = fireCooldown;
                }
            }
        }
        //if the gun is semi automatic you can hold down but it will only shoot once
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (currentnCooldown <= 0f)
                {
                    //invokes everything you set for when the gun shoots
                    onGunShoot?.Invoke();
                    currentnCooldown = fireCooldown;
                }
            }
        }

        currentnCooldown -= Time.deltaTime;
    }
}
