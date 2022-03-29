using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource audioSource;
    [SerializeField]
    private AudioClip jumpAudio, hurtAudio, collectionAudio, fallAudio;
    // Update is called once per frame
    private void Awake()
    {
        instance = this;
    }
    public void JumpAudio()
    {
        audioSource.clip = jumpAudio;
        audioSource.Play();
    }

    public void HurtAudio()
    {
        audioSource.clip = hurtAudio;
        audioSource.Play();
    }

    public void CollectionAudio()
    {
        audioSource.clip = collectionAudio;
        audioSource.Play();
    }

    public void FallAudio()
    {
        audioSource.clip = fallAudio;
        audioSource.Play();
    }
}
