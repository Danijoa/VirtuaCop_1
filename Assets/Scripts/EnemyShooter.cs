using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public Transform gunPivot; // 총 배치의 기준점
    public Transform leftHandMount; // 총의 손잡이, 손이 위치할 지점
    public Transform rightHandMount; 

    private Animator enemyAnimator;  // 애니메이터 컴포넌트

    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
    }

    // 애니메이터의 IK 갱신
    private void OnAnimatorIK(int layerIndex)
    {
        // 총 피봇 위치 = 플레이어(IK 대상이 된다) 애니메이터 포지션의 팔꿈치
        gunPivot.position = enemyAnimator.GetIKHintPosition(AvatarIKHint.RightElbow);

        // 플레이어 손 위치와 회전 가중치
        enemyAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        //enemyAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

        // IK대상: 플레이어 손 , 목표: 총의 손잡이 위치
        enemyAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandMount.position);
        //enemyAnimator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandMount.rotation);

        enemyAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        //enemyAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

        enemyAnimator.SetIKPosition(AvatarIKGoal.RightHand, rightHandMount.position);
        //enemyAnimator.SetIKRotation(AvatarIKGoal.RightHand, rightHandMount.rotation);

    }
}
