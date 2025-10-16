using System.Collections;
using UnityEngine;

public class CaptureAudio : MonoBehaviour
{
    private AudioClip soundClip;
    private string mic;
    private float[] audioData;
    private bool isRecording;
    private int sampleRate = 44100;
    private int maxRecordingLength = 30;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

    // Update is called once per frame
    void Update()
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

    void StartRecording()
    {
        soundClip = Microphone.Start(mic, false, maxRecordingLength, sampleRate);
        isRecording = true;
        Debug.Log("Recording started...");

        audioData = new float[sampleRate * (int)maxRecordingLength];

        Invoke("StopRecording", maxRecordingLength);
    }

    void StopRecording()
    {
        if (!isRecording) return;

        Microphone.End(mic);
        isRecording = false;
        Debug.Log("Recording stopped.");

        soundClip.GetData(audioData, 0);
        ConvertSoundClip(audioData);
        Save();
        //CancelInvoke("StopRecording");

        // wait for 5 seconds before playing back the audio
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            StartCoroutine(PlayAudioAfterDelay(audioSource, soundClip, 5f));
        }

    }

    IEnumerator PlayAudioAfterDelay(AudioSource audioSource, AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.clip = clip;
        audioSource.Play();
    }

    void ConvertSoundClip(float[] data)
    {
        float amp = 0f;
        for (int i = 0; i < data.Length; i++)
        {
            amp = Mathf.Max(amp, Mathf.Abs(data[i]));
        }
    }

    void Save()
    {

    }

    private void OnDestroy()
    {
       if (isRecording)
        {
            Microphone.End(mic);
        }
    }
}
