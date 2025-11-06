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

    public void DisplaySong(AudDResult result, Texture2D texture)
    {
        DateTime releaseDate = DateTime.Parse(result.release_date);

        Title.text = result.title + "\n\n" + result.album;
        Artist.text = result.artist + "\n" + releaseDate.ToString("MMMM d, yyyy");
        Cover.material.mainTexture = texture;
        //Label.text = result.label;
        //TimeCode.text = result.timecode;
        //SongLink.text = result.song_link;
        SongUI.enabled = true;
    }
}
