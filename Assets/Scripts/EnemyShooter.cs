using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public Transform gunPivot; // �� ��ġ�� ������
    public Transform handMount; // ���� ������, ���� ��ġ�� ����

    private Animator enemyAnimator;  // �ִϸ����� ������Ʈ

    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
    }

    // �ִϸ������� IK ����
    private void OnAnimatorIK(int layerIndex)
    {
        /*
        // �� �Ǻ� ��ġ = �÷��̾�(IK ����� �ȴ�) �ִϸ����� �������� �Ȳ�ġ
        gunPivot.position = enemyAnimator.GetIKHintPosition(AvatarIKHint.RightElbow);

        // �÷��̾� �� ��ġ�� ȸ�� ����ġ
        enemyAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        enemyAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

        // IK���: �÷��̾� �� , ��ǥ: ���� ������ ��ġ
        enemyAnimator.SetIKPosition(AvatarIKGoal.RightHand, handMount.position);
        enemyAnimator.SetIKRotation(AvatarIKGoal.RightHand, handMount.rotation);
        */
    }
}