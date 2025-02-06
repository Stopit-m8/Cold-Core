
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------------------- Audio Source --------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXsource;

    
[Header("------------------- Audio Clip --------------")]

    public AudioClip background;
    public AudioClip death;
    public AudioClip walking;
    public AudioClip shooting;
    public AudioClip jumping;
    public AudioClip bulletImpact;
    public AudioClip getHit;
    public AudioClip heal;
    public AudioClip tankExplode;
    public AudioClip cannonBulletimp;
    public AudioClip Tankshoot;

    public AudioClip Victory;

    public void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip) { SFXsource.PlayOneShot(clip); }

}
