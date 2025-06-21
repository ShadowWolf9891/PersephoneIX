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
        if (isAutomatic)
        {
            if (Input.GetMouseButton(0))
            {
                if(currentnCooldown <= 0f)
                {
                    onGunShoot?.Invoke();
                    currentnCooldown = fireCooldown;
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (currentnCooldown <= 0f)
                {
                    onGunShoot?.Invoke();
                    currentnCooldown = fireCooldown;
                }
            }
        }

        currentnCooldown -= Time.deltaTime;
    }
}
