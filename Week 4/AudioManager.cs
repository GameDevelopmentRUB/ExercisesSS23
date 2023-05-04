using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // This AudioMixer contains the group Master and subgroups BGM and SFX
    [SerializeField] AudioMixer mixer;

    // We have different AudioSources for music and sound effects, so we can control them separately
    [SerializeField] AudioSource bgmSource;
    [SerializeField] AudioSource sfxSource;

    // All audio files are of type 'AudioClip', no matter if music or sound effects
    [SerializeField] AudioClip bgmForest;

    [SerializeField] AudioClip sfxPi;
    [SerializeField] AudioClip sfxKa;
    [SerializeField] AudioClip sfxChu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // This will set the clip and interrupt the old one
            bgmSource.clip = bgmForest;
            bgmSource.Play();
        }

        // PlayOneShot on the other hand does not interrupt the current clip
        if (Input.GetKeyDown(KeyCode.Alpha2))
            sfxSource.PlayOneShot(sfxPi);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            sfxSource.PlayOneShot(sfxKa);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            sfxSource.PlayOneShot(sfxChu);

        // This gets the value from AudioMixer parameter "MainVol" (needs to be exposed in the AudioMixer first)
        mixer.GetFloat("MainVol", out float vol);

        // This increases and decreases the volume. Be aware that the volume is in db, which is not on a linear scale
        // To do this correctly, you would need to convert it first, but for the exercises just changing the value is enough
        if (Input.GetKeyDown(KeyCode.O))
            mixer.SetFloat("MainVol", --vol);
        if (Input.GetKeyDown(KeyCode.P))
            mixer.SetFloat("MainVol", ++vol);
    }
}
