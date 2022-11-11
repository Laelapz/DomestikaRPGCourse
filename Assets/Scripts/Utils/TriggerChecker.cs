using UnityEngine;
using UnityEngine.Events;

public class TriggerChecker : MonoBehaviour
{

    public UnityEvent onTriggerEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onTriggerEnter.Invoke();
    }
}
