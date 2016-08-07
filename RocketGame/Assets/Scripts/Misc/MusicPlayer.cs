using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicPlayer : MonoBehaviour {

    public List<AudioSource> songs;
    private int currentSong;
    private bool shouldPlayMusic;

	// Use this for initialization
	void Start () {
        shouldPlayMusic = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!songs[currentSong].isPlaying && shouldPlayMusic)
        {
            //start new song
            currentSong = getNewSong(currentSong);
            songs[currentSong].Play();
        }
	}

    private int getNewSong(int current)
    {
        if (songs.Count <= 1)
            return 0;
        int newSong = Random.Range(0, songs.Count);
        if (newSong == current)
            return getNewSong(current);
        return newSong;

    }

    public void startMusic()
    {
        shouldPlayMusic = true;
        currentSong = getNewSong(-1);
        songs[currentSong].Play();
    }

    public void stopMusic()
    {
        songs[currentSong].Pause();
        shouldPlayMusic = false;
    }

    public void resumeMusic()
    {
        shouldPlayMusic = true;
        songs[currentSong].UnPause();
    }

    public void shouldPlay(bool music)
    {
        shouldPlayMusic = music;
    }

}
