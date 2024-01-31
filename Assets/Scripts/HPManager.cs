using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    [SerializeField]
    private bool _god;

    [SerializeField]
    float _maxHP = 100;

    [SerializeField]
    Slider _hpSlider;
    float _currentHP;

    void Start()
    {
        _currentHP = _maxHP;
        _hpSlider.maxValue = _maxHP;
        _hpSlider.value = _currentHP;
    }
    private void Update()
    {
        _hpSlider.value = _currentHP;
    }
    public void GetDamage(int damage)
    {
        _currentHP -= damage;
        if (_currentHP <= 0)
        {
            Debug.Log("Dead");
        }
    }
}
