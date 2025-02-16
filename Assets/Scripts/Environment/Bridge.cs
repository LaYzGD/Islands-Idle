using System;
using UnityEngine;

public class Bridge : MonoBehaviour, IBind<BridgeData>
{
    [SerializeField] private BoxCollider _wallCollider;
    [SerializeField] private Animator _animator;
    [SerializeField] private string _showTriggerName = "Show";
    [SerializeField] private TriggerZone _zone;

    private BridgeData _bridgeData;

    public string ID { get => ObjectID; set => ObjectID = value; }
    public string ObjectID;

    public void Bind(BridgeData data)
    {
        ID = data.ID;
        _bridgeData = data;
        if (ID == null || ID == string.Empty)
        {
            ID = $"P1{UnityEngine.Random.value}P2{UnityEngine.Random.value}P3{UnityEngine.Random.value}P4{UnityEngine.Random.value}P5{UnityEngine.Random.value}";
            _bridgeData.ID = ID;
        }
        if (_bridgeData.IsOpened)
        {
            ShowBridge();
        }
    }

    public void ShowBridge()
    {
        _animator.SetTrigger(_showTriggerName);
        _wallCollider.gameObject.SetActive(false);
        _bridgeData.IsOpened = true;
        _zone.gameObject.SetActive(false);
    }
}

[Serializable]
public class BridgeData : ISaveable
{
    public string ID { get => ObjectID; set => ObjectID = value; }
    public string ObjectID;
    public bool IsOpened;
}
