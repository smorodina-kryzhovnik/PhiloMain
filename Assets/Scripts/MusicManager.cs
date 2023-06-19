using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    static MusicManager instance;
    public static MusicManager Instance
    {
        get { return instance; }
    }
    private AudioSource audiosource;
    public AudioClip[] tracks;
    public float volume;
    float playTimer;
    float tracksPlayed;
    bool[] beenPlayed;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        beenPlayed = new bool[tracks.Length];
        if (!audiosource.isPlaying)
            ChangeTrack(Random.Range(0, tracks.Length));
    }

    void Update()
    {
        audiosource.volume = volume;
        if (audiosource.isPlaying)
        {
            playTimer += 1 * Time.deltaTime;
        }
        if (!audiosource.isPlaying || playTimer >= audiosource.clip.length)
            ChangeTrack(Random.Range(0, tracks.Length));
        if (tracksPlayed == tracks.Length)
            ResetShuffle();
    }

    public void ChangeTrack(int trackNumber)
    {
        if (!beenPlayed[trackNumber])
        {
            playTimer = 0;
            tracksPlayed++;
            beenPlayed[trackNumber] = true;
            audiosource.clip = tracks[trackNumber];
            audiosource.Play();
        }
        else
            audiosource.Stop();
    }

    void ResetShuffle()
    {
        tracksPlayed = 0;
        for (int i = 0; i < tracks.Length; i++)
            beenPlayed[i] = false;
    }
}