using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;


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

    private AudioClip soundClip;
    private string mic;
    private float[] audioData;
    private bool isRecording;
    private int sampleRate = 44100;
    private int maxRecordingLength = 10;

    void Start()
    {
        if (Microphone.devices.Length == 0)
        {
            Debug.LogError("No microphone devices found.");
            return;
        }

        mic = Microphone.devices[0];
        Debug.Log("Using microphone: " + mic);
    }

    void Update() //Once UI is done, this method can be deleted
    {
        if (Input.GetKeyDown(KeyCode.R) && !isRecording)
        {
            StartRecording();
        }
        else if (Input.GetKeyDown(KeyCode.R) && isRecording)
        {
            StopRecording();
        }
    }

    void StartRecording() // call this on button press
    {
        soundClip = Microphone.Start(mic, false, maxRecordingLength, sampleRate);
        isRecording = true;
        Debug.Log("Recording started...");

        audioData = new float[sampleRate * (int)maxRecordingLength];

        Invoke("StopRecording", maxRecordingLength);
    }

    void StopRecording() //auto called, call to stop recording early
    {
        if (!isRecording) return;

        Microphone.End(mic);
        isRecording = false;
        Debug.Log("Recording stopped.");

        soundClip.GetData(audioData, 0);
        ConvertSoundClip(audioData);
        Save();
        

        //audio recording testing code
        /* // wait for 5 seconds before playing back the audio
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            StartCoroutine(PlayAudioAfterDelay(audioSource, soundClip, 5f));
        }*/

    }

    IEnumerator PlayAudioAfterDelay(AudioSource audioSource, AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.clip = clip;
        audioSource.Play();
    } //used to test recording

    void ConvertSoundClip(float[] data) //need this explained
    {
        float amp = 0f;
        for (int i = 0; i < data.Length; i++)
        {
            amp = Mathf.Max(amp, Mathf.Abs(data[i]));
        }
    }

    void Save() //auto called when recording stops
    {
        StartCoroutine(SendToAudD());
    }

    private void OnDestroy()
    {
        if (isRecording)
        {
            Microphone.End(mic);
        }
    }

    //below this doesn't need to be called externally
    private IEnumerator SendToAudD()
    {
        //convert audioclip to wav bytes
        byte[] wavData = ConvertAudioClipToWav(soundClip);

        //form data
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", wavData, "recording.wav", "audio/wav");
        form.AddField("api_token", apiKey);
        form.AddField("return", "apple_music,spotify");

        //send request
        //Documentation for this is on Teams
        using (UnityWebRequest www = UnityWebRequest.Post("https://api.audd.io/", form))
        {
            //prevents the app from freezing while waiting for response
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                ProcessAudDResponse(www.downloadHandler.text);
            }
            else
            {
                Debug.LogError($"AudD API error: {www.error}");
            }
        }
    }
    private void ProcessAudDResponse(string json)
    {
        Debug.Log($"AudD Response: {json}");

        //parse json response
        AudDResponse response = JsonUtility.FromJson<AudDResponse>(json);

        if (response.status == "success" && response.result != null)
        {
            Debug.Log($"Song found: {response.result.title} by {response.result.artist}");

           //Here we will call anything requiring the information provided by AudD by passing in response variable
        }
        else
        {
            Debug.LogError("No song recognized or api error");
        }
    }
    private byte[] ConvertAudioClipToWav(AudioClip clip)
    {//create a memory stream to hold the WAV file data in memory
        using (MemoryStream stream = new MemoryStream())
        {
            // Use BinaryWriter for easy writing of binary data to the stream
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                // ===== WAV FILE HEADER =====

                // RIFF chunk descriptor
                writer.Write(new char[4] { 'R', 'I', 'F', 'F' }); // RIFF header signature
                writer.Write(36 + clip.samples * 2); // Overall file size - 36 bytes for header + audio data size
                writer.Write(new char[4] { 'W', 'A', 'V', 'E' });// WAVE format identifier

                //format subchuck
                writer.Write(new char[4] { 'f', 'm', 't', ' ' }); // "fmt " chunk header
                writer.Write(16); // Size of format subchunk (16 for PCM)
                writer.Write((ushort)1); // Audio format (1 = PCM)
                writer.Write((ushort)clip.channels); // Number of channels (1 = mono, 2 = stereo)
                writer.Write(clip.frequency); // Sample rate (Hz)
                writer.Write(clip.frequency * clip.channels * 2); // Byte rate (sample rate * channels * bytes per sample)
                writer.Write((ushort)(clip.channels * 2)); // Block align (channels * bytes per sample)
                writer.Write((ushort)16); // Bits per sample (16 bits = 2 bytes)

                //data subchuck
                writer.Write(new char[4] { 'd', 'a', 't', 'a' });// "data" chunk header
                writer.Write(clip.samples * 2); // Size of audio data in bytes

                // ===== AUDIO DATA =====

                // Create array to hold all audio samples (including all channels)
                float[] samples = new float[clip.samples * clip.channels];

                // Extract the raw audio data from the AudioClip
                clip.GetData(samples, 0);

                // Convert each float sample (-1.0 to 1.0) to 16-bit integer (-32768 to 32767)
                foreach (float sample in samples)
                {
                    writer.Write((short)(sample * short.MaxValue));
                }
            }
            // Convert the memory stream to a byte array containing the complete WAV file
            return stream.ToArray();
        }
    }
}
