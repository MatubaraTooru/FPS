using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    [SerializeField]
    float _maxHP = 100;
    float _currentHP;
    public float CurrentHP { get => _currentHP; set => _currentHP = value; }
    void Start()
    {
        _currentHP = _maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
