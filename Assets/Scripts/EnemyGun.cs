using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public ParticleSystem flashEffect; // �ѱ� ȭ�� ȿ��

    private AudioSource shotAudio; // �� �Ҹ� �����
    public AudioClip shotClip; // �߻� �Ҹ�

    //�÷��̾� ����
    private PlayerHp m_PlayerHp;
    private CameraCtrl m_CamerCtrl;

    private int index = 0;

    private void Awake()
    {
        shotAudio = GetComponent<AudioSource>();
        m_PlayerHp = GameObject.Find("Player").GetComponent<PlayerHp>();
        m_CamerCtrl = GameManager.instance.m_Camera.GetComponent<CameraCtrl>();
    }

    public void Fire()
    {
        // ����Ʈ�� �Ҹ�
        flashEffect.Play();
        shotAudio.PlayOneShot(shotClip);

       if (m_CamerCtrl.m_Targets[index].gameObject == this.transform.parent.gameObject) // Ÿ�� �� ��� ���� �ֵ��� 
       {
           // �÷��̾� ���� ���̰�
           m_PlayerHp.PlayerHit(1);
       } 
    }
}
