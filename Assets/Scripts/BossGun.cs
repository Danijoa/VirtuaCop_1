using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGun : MonoBehaviour
{
    public ParticleSystem flashEffect; // �ѱ� ȭ�� ȿ��

    private AudioSource shotAudio; // �� �Ҹ� �����
    public AudioClip shotClip; // �߻� �Ҹ�

    //�÷��̾� ����
    private PlayerHp playerHp;

    private int index = 0;

    private void Awake()
    {
        shotAudio = GetComponent<AudioSource>(); 
        playerHp = GameObject.Find("Player").GetComponent<PlayerHp>();
    }

    public void Fire()
    {
        // ����Ʈ�� �Ҹ�
        flashEffect.Play();
        shotAudio.PlayOneShot(shotClip);

        // �÷��̾� hp ���̱�
        playerHp.PlayerHit(1);

    }
}
