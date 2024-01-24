using UnityEngine;
using UnityEngine.SceneManagement;

public class Musica : MonoBehaviour
{
    private double goalTime;
    private double musicDuration;

    private int audioToggle = 0;
    private int clip = 0;

    [SerializeField] private AudioClip[] musicClip;
    [SerializeField] private AudioSource[] musicSource;

    private void Start()
    {
        goalTime = AudioSettings.dspTime;
        DontDestroyOnLoad(gameObject);
    }
    private void PlayScheduledClip()
    {

        musicSource[audioToggle].clip = musicClip[audioToggle];
        musicSource[audioToggle].PlayScheduled(goalTime);

        musicDuration = (double)musicClip[audioToggle].samples / musicClip[audioToggle].frequency;
        goalTime += musicDuration;

        audioToggle = 1 - audioToggle;    }
    private void Update()
    {

        if (AudioSettings.dspTime > goalTime - 2)
        {
            PlayScheduledClip();
            ClipUpdate();
        }
    }
    private void ClipUpdate()
    {
        clip++;

        if(clip == musicClip.Length)
        {
            clip = 0;
        }
    }

}
