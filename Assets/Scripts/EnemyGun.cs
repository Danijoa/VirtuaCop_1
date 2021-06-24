using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public ParticleSystem flashEffect; // 醚备 拳堪 瓤苞

    private AudioSource shotAudio; // 醚 家府 犁积扁
    public AudioClip shotClip; // 惯荤 家府

    private void Awake()
    {
        shotAudio = GetComponent<AudioSource>();
    }

    public void Fire()
    {
        // 捞棋飘客 家府
        flashEffect.Play();
        shotAudio.PlayOneShot(shotClip);
    }
}
