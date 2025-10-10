using System.Collections;
using UnityEngine;


[System.Serializable]
public class AudDResponse
{
    public string status;
    public AudDResult result;
}

[System.Serializable]
public class AudDResult
{
    public string artist;
    public string title;
    public string album;
    public string release_date;
    public string label;
    public string timecode;
    public string song_link;
    public AppleMusic apple_music;
    public Spotify spotify;
}

[System.Serializable]
public class AppleMusic
{
    public string url;
}

[System.Serializable]
public class Spotify
{
    public string url;
}
public class AudD : MonoBehaviour
{
    [Header("API Settings")]
    public string apiKey = ""; // Ask me for the key, It will not be pushed
    public int recordingLength = 10; // Don't go longer than 15 seconds

    private AudioClip recordedClip;
    public bool isRecording = false;

    private void Start()
    {
        
    }
    public void StartRecording() //Call this on button press
    {

    }
    public void StopRecording() //Will be called after recordingLength is reached or when canceled by
        //the user
    {

    }
    //below this doesn't need to be called externally
    private IEnumerator SendToAudD()
    {
        return null;
    }
    private void ProcessAudDResponse(string json)
    {

    }
    private byte[] ConvertAudioClipToWav()
    {
        return null;
    }
    //testing purposes only, will be rewritten to be displayed to the user.
    private void OnSongRecognized()
    {

    }

}
