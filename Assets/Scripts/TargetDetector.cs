using Cinemachine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

[RequireComponent (typeof(SphereCollider))]
public class TargetDetector : MonoBehaviour
{
    [SerializeField, TagField] string _tag;
    [SerializeField] float _angle = 45f;
    [SerializeField] SphereCollider _sphereCollider;
    Transform _targetTransform;
    public bool IsTargetDetected { get; private set; }
    public Transform TargetTransform { get; private set; }

    private void Awake()
    {
        TargetTransform = FindAnyObjectByType<PlayerController>().transform;
        Debug.Log(TargetTransform.position);
    }

    private void SearchTarget()
    {
        Vector3 dir = _targetTransform.position - this.transform.position;
        float targetAngle = Vector3.Angle(this.transform.forward, dir);

        if (targetAngle < _angle)
        {
            if (Physics.Raycast(this.transform.position, dir, out RaycastHit hit, _angle))
            {
                Debug.DrawRay(transform.position, dir, Color.red, _angle);

                if (hit.collider.CompareTag(_tag))
                {
                    IsTargetDetected = true;
                    TargetTransform = _targetTransform;
                    Debug.Log("Target Detected!");
                }
                else
                {
                    IsTargetDetected = false;
                    Debug.Log("Target Missing");
                }
            }
        }
        else Debug.Log("?");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(_tag))
        {
            _targetTransform = other.transform;
            SearchTarget();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _targetTransform = null;
    }

    private void OnDrawGizmosSelected()
    {
        Handles.DrawSolidArc(transform.position, transform.up, Quaternion.Euler(0f, -_angle, 0f) * transform.forward, _angle * 2, _sphereCollider.radius * 2);
    }
}
