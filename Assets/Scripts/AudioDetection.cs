using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioDetection : MonoBehaviour
{
    public int sampleWindow = 64;
    private AudioClip microphoneClip;
    private string microphoneName;
    private bool isMicrophoneInitialized = false;
    
    void Start()
    {
        MicrophoneToAudioClip();
    }

    void Update()
    {
        
    }
    
    public void MicrophoneToAudioClip()
    {
        // Check if microphone exists
        if (Microphone.devices.Length == 0)
        {
            Debug.LogError("No microphone detected!");
            return;
        }
        
        microphoneName = Microphone.devices[0];
        microphoneClip = Microphone.Start(microphoneName, true, 20, AudioSettings.outputSampleRate);
        
        Debug.Log("Microphone started: " + microphoneName);
        
        // Start coroutine to wait for microphone initialization
        StartCoroutine(WaitForMicrophoneInit());
    }
    
    IEnumerator WaitForMicrophoneInit()
    {
        // Wait until the microphone starts recording
        while (!(Microphone.GetPosition(microphoneName) > 0))
        {
            yield return null;
        }
        
        // Wait a bit more to ensure we have enough samples
        yield return new WaitForSeconds(0.1f);
        
        isMicrophoneInitialized = true;
        Debug.Log("Microphone initialized and ready!");
    }

    public float GetLoudnessFromMicrophone()
    {
        // Return 0 if microphone isn't ready yet
        if (!isMicrophoneInitialized || microphoneClip == null)
            return 0;
            
        // Get current position of the microphone recording
        int micPosition = Microphone.GetPosition(microphoneName);
        
        // Make sure we have enough samples to analyze
        if (micPosition < sampleWindow)
            return 0;
            
        return getLoudnessFromClip(micPosition, microphoneClip);
    }
    
    public float getLoudnessFromClip(int clipPosition, AudioClip clip)
    {
        int startPosition = clipPosition - sampleWindow;

        if (startPosition < 0)
            return 0;

        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPosition);

        float totalLoudness = 0;

        for (int i = 0; i < sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }

        return totalLoudness / sampleWindow;
    }
    
    void OnDestroy()
    {
        // Clean up microphone when object is destroyed
        if (microphoneName != null && Microphone.IsRecording(microphoneName))
        {
            Microphone.End(microphoneName);
        }
    }
}