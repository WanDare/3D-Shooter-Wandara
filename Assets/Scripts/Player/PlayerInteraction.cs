using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private List<Interactable> interactables = new List<Interactable>();
    private Interactable closestInteractable;


    private void Start()
    {
        Player player = GetComponent<Player>();
        player.controls.Character.Interaction.performed += context => InteractWithClosest();
    }

    private void InteractWithClosest()
    {
        closestInteractable?.Interaction();
        interactables.Remove(closestInteractable);

        UpdateClosestInteractable();
    }

    public void UpdateClosestInteractable()
    {
        closestInteractable?.HighlightActive(false); // Disable highlight on the old one if it exists

        closestInteractable = null;
        float closestDistance = float.MaxValue;

        foreach (Interactable interactable in interactables)
        {
            float distance = Vector3.Distance(transform.position, interactable.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestInteractable = interactable;
            }
        }

        if (closestInteractable != null)
        {
            closestInteractable.HighlightActive(true);
        }
        else
        {
            Debug.LogWarning("No interactables in range to highlight.");
        }
    }


    public List<Interactable> GetInteractables() => interactables;
}
