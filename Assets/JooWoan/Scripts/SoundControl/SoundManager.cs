using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton
    public static SoundManager Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Init();
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }
    #endregion

    [SerializeField] private float sfxVolume;
    [SerializeField] private float bgmVolume;

    [SerializeField] private SoundConfig[] bgmArr;
    [SerializeField] private SoundConfig[] sfxArr;

    [SerializeField] private AudioSource bgmPlayer, audioSourceTemplate;
    private AudioSource[] sfxSources = new AudioSource[32];

    private IDictionary<AudioNameTag, SoundConfig> bgmDict = new Dictionary<AudioNameTag, SoundConfig>();
    private IDictionary<AudioNameTag, SoundConfig> sfxDict = new Dictionary<AudioNameTag, SoundConfig>();
    private IDictionary<string, AudioNameTag> audioTagDict = new Dictionary<string, AudioNameTag>();

    private void Init()
    {
        foreach (SoundConfig config in bgmArr)
        {
            bgmDict.Add(config.NameTag, config);
            audioTagDict[config.Clip.name] = config.NameTag;
        }

        foreach (SoundConfig config in sfxArr)
        {
            sfxDict.Add(config.NameTag, config);
            audioTagDict[config.Clip.name] = config.NameTag;
        }

        for (int i = 0; i < sfxSources.Length; i++)
            sfxSources[i] = Instantiate(audioSourceTemplate, transform);
    }

    public static void PlaySFX(AudioNameTag tag)
    {
        AudioSource audioSource = null;

        foreach (AudioSource source in Instance.sfxSources)
        {
            if (source.isPlaying &&
                Instance.audioTagDict[source.clip.name] == tag &&
                source.time <= Instance.sfxDict[tag].MinPlaybackInterval)
            {
                return;
            }
            if (audioSource == null && !source.isPlaying)
                audioSource = source;
        }

        if (audioSource == null)
            return;

        if (!Instance.sfxDict.ContainsKey(tag))
        {
            Debug.LogWarning($"Failed to find clip as : {tag.ToString()}");
            return;
        }

        audioSource.clip    = Instance.sfxDict[tag].Clip;
        audioSource.volume  = Instance.sfxVolume;
        audioSource.Play();
    }

    public static void PlayBGM(AudioNameTag tag)
    {
        if (!Instance.bgmDict.ContainsKey(tag))
            return;

        PauseBGM();
        Instance.bgmPlayer.clip = Instance.bgmDict[tag].Clip;
        Instance.bgmPlayer.volume = Instance.bgmVolume;
        Instance.bgmPlayer.Play();
    }

    public static void PauseBGM()
    {
        if (Instance.bgmPlayer == null)
            return;

        Instance.bgmPlayer.Pause();
    }

    public static void StopBGM()
    {
        if (Instance.bgmPlayer == null)
            return;

        Instance.bgmPlayer.Stop();
    }
    public static void SetVolumeBGM(float volume)
    {
        Instance.bgmVolume = volume;

        if (Instance.bgmPlayer != null)
            Instance.bgmPlayer.volume = Instance.bgmVolume;
    }

    public static void SetVolumeSFX(float volume)
    {
        Instance.sfxVolume = volume;
    }

    public static void SetBgmSpeed(float speed = 1.0f)
    {
        Instance.bgmPlayer.pitch = speed;
    }
}

[System.Serializable]
public class SoundConfig
{
    [SerializeField] AudioNameTag nameTag;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float minPlaybackInterval;

    public AudioNameTag NameTag => nameTag;
    public AudioClip Clip => audioClip;
    public float MinPlaybackInterval => minPlaybackInterval;
}

public enum AudioNameTag
{
    BGM_TITLE,
    BGM_GAME,
    SFX_UIBUTTON,
    SFX_COUNTDOWN,
    SFX_FEVER_TRANSITION,
    SFX_NOTEHIT_DEFAULT
}