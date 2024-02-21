using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletHoleRemover : MonoBehaviour
{
    [SerializeField, Tooltip("弾痕の最大保持数")]
    private int _holeCount = 10;
    /// <summary>シーン上に存在する弾痕を保持しておくキュー</summary>
    private Queue<GameObject> _bulletHoles = new();

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
