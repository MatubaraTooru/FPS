using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletHoleRemover : MonoBehaviour
{
    [SerializeField]
    private int _holeCount = 10;

    private Queue<GameObject> _bulletHoles;
    // Start is called before the first frame update
    void Start()
    {
        _bulletHoles = new();
    }

    // Update is called once per frame
    void Update()
    {
        var obj = FindFirstObjectByType(typeof(GameObject)).GameObject();
        if (obj.CompareTag("BulletHole") && !_bulletHoles.Contains(obj))
        {
            _bulletHoles.Enqueue(obj);
        }
        if (_bulletHoles.Count > _holeCount)
        {
            var temp = _bulletHoles.Dequeue();
            Destroy(temp);
        }
    }
}
