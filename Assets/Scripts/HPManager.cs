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

    public float HP { get; private set; }

    void Start()
    {
        HP = _maxHP;

        if (_hpSlider != null)
        {
            _hpSlider.maxValue = _maxHP;
            _hpSlider.value = HP;
        }
    }
    private void Update()
    {
        if (_hpSlider != null) _hpSlider.value = HP;
    }
    public void GetDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            this.gameObject.SetActive(false);
            Debug.Log("Dead");
        }
    }
}
