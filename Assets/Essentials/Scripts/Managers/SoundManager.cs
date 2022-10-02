using UnityEngine;

public class SoundManager : MSingleton<SoundManager>
{
    [Header("References")]
    public AudioClip jumpClip;
    public AudioClip dashClip;
    public AudioClip fallClip;
    public AudioClip collectClip;
    public AudioClip lightningClip;
    public AudioClip explosionClip;
    public AudioClip flowerPotClip;
    public AudioClip dieClip;

    public AudioSource PlaySound(AudioClip clip, bool looping = false, float lifeTime = -1f)
    {
        if (!SaveManager.Instance.SoundOn)
            return null;

        GameObject soundGameObject = new GameObject(clip.name);
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.playOnAwake = false;

        if (looping)
        {
            audioSource.loop = true;
            audioSource.Play();

            if (lifeTime > 0f)
            {
                Destroy(soundGameObject, lifeTime);
            }
        }
        else
        {
            audioSource.PlayOneShot(clip);
            Destroy(soundGameObject, clip.length);
        }

        return audioSource;
    }
}
