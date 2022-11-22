using ScriptableObjectArchitecture;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Configurations")]
    [SerializeField] private string interactableTag;

    [Header("Broadcasting Events")]
    [SerializeField] private BoolGameEvent interactionRequestEvent;

    private Interactable _interactable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(interactableTag))
        {
            var interactable = collision.GetComponent<Interactable>();
            this._interactable = interactable;
        }

        this.interactionRequestEvent.Raise((_interactable != null));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(interactableTag))
        {
            this._interactable = null;
        }

        this.interactionRequestEvent.Raise((_interactable != null));
    }

    public void EnableInteractable()
    {
        if(this._interactable != null)
        {
            this._interactable.Interact();
        }
    }
}
