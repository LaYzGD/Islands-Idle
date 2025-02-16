using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] private BoxCollider _wallCollider;
    [SerializeField] private Animator _animator;
    [SerializeField] private string _showTriggerName = "Show";

    public void ShowBridge()
    {
        _animator.SetTrigger(_showTriggerName);
        _wallCollider.gameObject.SetActive(false);
    }
}
