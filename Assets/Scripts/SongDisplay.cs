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
        Title.text = result.title + "\n\n" + result.album;

        if (result.release_date != null && !result.release_date.Equals(""))
        {
            DateTime releaseDate = DateTime.Parse(result.release_date);
            Artist.text = result.artist + "\n" + releaseDate.ToString("MMMM d, yyyy");
        }
        else
        {
            Artist.text = result.artist;
        }
        
        Cover.material.mainTexture = texture;
        //Label.text = result.label;
        //TimeCode.text = result.timecode;
        //SongLink.text = result.song_link;
        SongUI.enabled = true;
    }

    public void Hide()
    {
        SongUI.enabled = false;
    }
}
