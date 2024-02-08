using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] bool _isShowFPS = false;

    int _frameCount = 0;
    float _prevTime = 0f;
    float _fps = 0f;
    private void OnGUI()
    {
        if (!_isShowFPS) return;

        // _fps = 1f / Time.deltaTime;
        
        float time = Time.realtimeSinceStartup - _prevTime;

        if (time >= 0.5f)
        {
            _fps = _frameCount / time;
            _frameCount = 0;
            _prevTime = Time.realtimeSinceStartup;
        }

        GUILayout.Label(_fps.ToString("000"));
    }

    private void Update()
    {
        _frameCount++;
    }
}
