using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SongDisplay : MonoBehaviour
{
    [SerializeField] public Canvas SongUI;
    [SerializeField] public Image Cover;
    [SerializeField] public TMP_Text Title;
    [SerializeField] public TMP_Text Artist;

    private void Start()
    {
       SongUI.enabled = false;
    }

    public void DisplaySong(AudDResult result)
    {
        Debug.Log("hi");

        Title.text = result.title + "\n\n" + result.album;
        Artist.text = result.artist + "\n" + result.release_date;
        //Label.text = result.label;
        //TimeCode.text = result.timecode;
        //SongLink.text = result.song_link;
        SongUI.enabled = true;
    }
}
