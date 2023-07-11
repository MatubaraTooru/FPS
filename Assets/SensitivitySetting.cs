using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SensitivitySetting : MonoBehaviour
{
    [SerializeField] float _maxValue = 1.0f;
    [SerializeField] float _minValue = 0.1f;
    [SerializeField] InputActionAsset _actionAsset;
    [SerializeField] Slider _slider;
    // Start is called before the first frame update
    void Start()
    {
        _slider.maxValue = _maxValue;
        _slider.minValue = _minValue;
        _slider.value = _maxValue / 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Test()
    {
        _actionAsset.a
    }
}
