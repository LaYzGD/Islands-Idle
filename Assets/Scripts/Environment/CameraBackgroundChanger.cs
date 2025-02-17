using System.Collections;
using UnityEngine;

public class CameraBackgroundChanger : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Color _baseColor;
    [SerializeField] private Color _caveColor;
    [SerializeField] private float _transitionTime = 1f;

    private Coroutine _coroutine;
    private bool _isRunning;

    public void EnterCave()
    {
        if (_isRunning)
        {
            StopCoroutine(_coroutine);
            _isRunning = false;
        }

        _coroutine = StartCoroutine(ChangeColorCoroutine(_baseColor, _caveColor));
    }

    public void ExitCave()
    {
        if (_isRunning)
        {
            StopCoroutine(_coroutine);
            _isRunning = false;
        }

        _coroutine = StartCoroutine(ChangeColorCoroutine(_caveColor, _baseColor));
    }
    
    private IEnumerator ChangeColorCoroutine(Color startColor, Color targetColor)
    {
        var timer = 0f;
        _isRunning = true;

        while (timer < _transitionTime)
        {
            timer += Time.deltaTime;
            _mainCamera.backgroundColor = Color.Lerp(startColor, targetColor, timer / _transitionTime);
            yield return null;
        }

        _mainCamera.backgroundColor = targetColor;
        _isRunning = false;
    }
}
