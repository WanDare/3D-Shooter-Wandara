using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected PlayerWeaponController weaponController;
    protected MeshRenderer mesh;


    [SerializeField] private Material highlightMaterial;
    private Material defaultMaterial;

    private void Start()
    {
        mesh = GetComponentInChildren<MeshRenderer>(); // Ensure mesh is assigned

        if (mesh == null)
        {
            Debug.LogError($"MeshRenderer not found on {gameObject.name}");
            return;
        }

        defaultMaterial = mesh.sharedMaterial;
    }


    protected void UpdateMeshAndMaterial(MeshRenderer newMesh)
    {
        if (newMesh == null)
        {
            Debug.LogError($"UpdateMeshAndMaterial received null mesh on {gameObject.name}");
            return;
        }

        mesh = newMesh;
        defaultMaterial = newMesh.sharedMaterial;
    }


    public virtual void Interaction()
    {
        Debug.Log("Interacted with " + gameObject.name);
    }


    public void HighlightActive(bool active)
    {
        if (mesh == null)
        {
            Debug.LogWarning("MeshRenderer is null when attempting to highlight.");
            return;
        }

        if (active)
            mesh.material = highlightMaterial;
        else
            mesh.material = defaultMaterial;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (weaponController == null)
            weaponController = other.GetComponent<PlayerWeaponController>();

        PlayerInteraction playerInteraction = other.GetComponent<PlayerInteraction>();

        if (playerInteraction == null)
            return;

        playerInteraction.GetInteractables().Add(this);
        playerInteraction.UpdateClosestInteractable();
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        PlayerInteraction playerInteraction = other.GetComponent<PlayerInteraction>();

        if (playerInteraction == null)
            return;
        
        playerInteraction.GetInteractables().Remove(this);
        playerInteraction.UpdateClosestInteractable();
    }
}
