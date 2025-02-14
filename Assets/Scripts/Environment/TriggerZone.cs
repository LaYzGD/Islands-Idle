using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    [SerializeField] private UnityEvent _onEnterEvent;
    [SerializeField] private UnityEvent _onExitEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterCore core))
        {
            _onEnterEvent?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.TryGetComponent(out CharacterCore core))
        {
            _onExitEvent?.Invoke();
        }
    }
}
