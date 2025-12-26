using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.Controls.AxisControl;


[System.Serializable]
public class SoundClass
{
    public string soundName;
    public AudioClip soundClip;
}


public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource effectSource;
    [SerializeField] List<SoundClass> soundClasses;
    private float sfxVolume;
    private float musicVolume;
    bool initialized;

    public static SoundManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
       // initialize();
    }
   public void SetVolumes(float musicVol,float sfxVol )
    {
        musicSource.volume = musicVol;
        effectSource.volume = sfxVol;

        musicVolume = musicVol;
        sfxVolume = sfxVol;
    }
    public void initialize()
    {
        if (initialized) return;
        initialized = true;
        GameObject gameObjectEffectSource = new GameObject();
        gameObjectEffectSource.transform.parent = transform;
        gameObjectEffectSource.name = "EffectSource";
        gameObjectEffectSource.AddComponent<AudioSource>();
        effectSource = gameObjectEffectSource.GetComponent<AudioSource>();

        GameObject gameObjectMusicSource = new GameObject();
        gameObjectMusicSource.transform.parent = transform;
        gameObjectMusicSource.name = "MusicSource";
        gameObjectMusicSource.AddComponent<AudioSource>();
        musicSource = gameObjectMusicSource.GetComponent<AudioSource>();
        musicSource.playOnAwake = false;

    }
    public void PlaySound(AudioClip cliop)
    {
        if (cliop == null) 
        {
            ThrowError();
            return;
        }
        _PlaySound(cliop);
    }
    public void PlayMusic(AudioClip cliop)
    {
        if (cliop == null)
        {
            ThrowError();
            return;
        }
        _PlayMusic(cliop);
    }
    public void PlayMusic(string clipname)
    {
        AudioClip clip = FindSoundByGivenName(clipname);
        if (clip == null)
        {
            ThrowError();
            return;
        }
        _PlayMusic(clip);
    }

    public void PlaySound(string clipname)
    {
        AudioClip clip = FindSoundByGivenName(clipname);
        if (clip == null)
        {
            ThrowError();
            return;
        }
        _PlaySound(clip);
    }
    private AudioClip lastClip;
    private int lastClipFrame = -1;

    private void _PlaySound(AudioClip clip)
    {
        if (clip == lastClip && Time.frameCount == lastClipFrame)
            return;

        lastClip = clip;
        lastClipFrame = Time.frameCount;
        effectSource.PlayOneShot(clip);
    }
    private void _PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true ;
        musicSource.Play();
    }
    private AudioClip FindSoundByGivenName(string clipname)
    {
        foreach (var soundClass in soundClasses)
        {
            if (soundClass.soundName == clipname)
            { 
                return soundClass.soundClip;
                
            }
        }
        return null;
    }
    void ThrowError()
    {
        Debug.LogError("SoundManager: Clip Or Name is Invalid");
    }
}
