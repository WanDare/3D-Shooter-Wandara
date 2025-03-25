using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerControls controls { get; private set; }
    public PlayerAim aim { get; private set; }
    public PlayerMovement movement { get; private set; }
    public PlayerWeaponController weapon { get; private set; }
    public PlayerWeaponVisuals weaponVisuals { get; private set; }
    public PlayerInteraction interaction { get; private set; }


    private void Awake()
    {
        controls = new PlayerControls();

        aim = GetComponent<PlayerAim>();
        if (aim == null)
        {
            Debug.LogError("PlayerAim component missing on " + gameObject.name);
        }

        movement = GetComponent<PlayerMovement>();
        if (movement == null)
        {
            Debug.LogError("PlayerMovement component missing on " + gameObject.name);
        }

        weapon = GetComponent<PlayerWeaponController>();
        if (weapon == null)
        {
            Debug.LogError("PlayerWeaponController component missing on " + gameObject.name);
        }

        weaponVisuals = GetComponent<PlayerWeaponVisuals>();
        if (weaponVisuals == null)
        {
            Debug.LogError("PlayerWeaponVisuals component missing on " + gameObject.name);
        }

        interaction = GetComponent<PlayerInteraction>();
        if (interaction == null)
        {
            Debug.LogError("PlayerInteraction component missing on " + gameObject.name);
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
