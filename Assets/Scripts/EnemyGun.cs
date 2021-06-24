using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public ParticleSystem flashEffect; // �ѱ� ȭ�� ȿ��

    private AudioSource shotAudio; // �� �Ҹ� �����
    public AudioClip shotClip; // �߻� �Ҹ�

    private void Awake()
    {
        shotAudio = GetComponent<AudioSource>();
    }

    public void Fire()
    {
        // ����Ʈ�� �Ҹ�
        flashEffect.Play();
        shotAudio.PlayOneShot(shotClip);
    }
}
