using UnityEngine;

public class IKController : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] Transform _rightHand;
    [SerializeField] Transform _leftHand;

    private void OnAnimatorIK()
    {
        if (_rightHand != null)
        {
            _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            _animator.SetIKPosition(AvatarIKGoal.RightHand, _rightHand.position);
            _animator.SetIKRotation(AvatarIKGoal.RightHand, _rightHand.rotation);
        }
        if (_leftHand != null)
        {
            _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            _animator.SetIKPosition(AvatarIKGoal.LeftHand, _leftHand.position);
            _animator.SetIKRotation(AvatarIKGoal.LeftHand, _leftHand.rotation);
        }
    }
}
