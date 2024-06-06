using System;
using Oculus.Voice;
using UnityEngine;
using UnityEngine.Events;

public class SpeechToTextManager : MonoBehaviour
{
    public static SpeechToTextManager Instance;
    
    [SerializeField] private AppVoiceExperience _appVoice;

    private AudioClip _currentAudioClip;
    private bool _isRecording = false;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void StartRecording(UnityEvent<string> recognizedTextEvent)
    {
        if(_isRecording) return;
        
        _isRecording = true;
        _appVoice.Deactivate();
        _appVoice.Activate();
        _appVoice.VoiceEvents.OnFullTranscription.RemoveAllListeners();
        _appVoice.VoiceEvents.OnFullTranscription.AddListener(transcription =>
        {
            _isRecording = false;
            recognizedTextEvent.Invoke(transcription);
            _appVoice.Deactivate();
            _appVoice.VoiceEvents.OnFullTranscription.RemoveAllListeners();
        });
    }

    public void StartRecording(UnityEvent<string> recognizedTextEvent, UnityEvent<AudioClip> recognizedAudioEvent)
    {
        if(_isRecording) return;
        
        _isRecording = true;
        _currentAudioClip = Microphone.Start(Microphone.devices[1], false, 180, 44100);
        _appVoice.Deactivate();
        _appVoice.Activate();
        _appVoice.VoiceEvents.OnFullTranscription.RemoveAllListeners();
        _appVoice.VoiceEvents.OnFullTranscription.AddListener(transcription =>
        {
            _isRecording = false;
            recognizedTextEvent.Invoke(transcription);
            _appVoice.Deactivate();
            Microphone.End(Microphone.devices[1]);
            _appVoice.VoiceEvents.OnFullTranscription.RemoveAllListeners();
            recognizedAudioEvent.Invoke(_currentAudioClip);
            _currentAudioClip = null;
        });
    }
    
    public void StartRecording(UnityEvent<string> recognizedTextEvent, UnityEvent<string> partialRecognizedText, UnityEvent<AudioClip> recognizedAudioEvent)
    {
        if(_isRecording) return;
        
        _isRecording = true;
        _currentAudioClip = Microphone.Start(Microphone.devices[1], false, 180, 44100);
        _appVoice.Deactivate();
        _appVoice.Activate();
        
        _appVoice.VoiceEvents.OnPartialTranscription.RemoveAllListeners();
        _appVoice.VoiceEvents.OnPartialTranscription.AddListener(partialTranscrition =>
        {
            partialRecognizedText.Invoke(partialTranscrition);
        });
        
        _appVoice.VoiceEvents.OnFullTranscription.RemoveAllListeners();
        _appVoice.VoiceEvents.OnFullTranscription.AddListener(transcription =>
        {
            _isRecording = false;
            recognizedTextEvent.Invoke(transcription);
            _appVoice.Deactivate();
            Microphone.End(Microphone.devices[1]);
            _appVoice.VoiceEvents.OnFullTranscription.RemoveAllListeners();
            partialRecognizedText.RemoveAllListeners();
            recognizedAudioEvent.Invoke(_currentAudioClip);
            _currentAudioClip = null;
        });
    }
    
    public float CalculateSimilarityPercentage(string source, string target)
    {
        int maxLength = Math.Max(source.Length, target.Length);
        if (maxLength == 0) return 100.0f;

        int distance = LevenshteinDistance(source, target);
        return ((float)(maxLength - distance) / maxLength) * 100;
    }
    
    public int LevenshteinDistance(string source, string target)
    {
        if (string.IsNullOrEmpty(source))
            return string.IsNullOrEmpty(target) ? 0 : target.Length;
        if (string.IsNullOrEmpty(target))
            return source.Length;

        int sourceLength = source.Length;
        int targetLength = target.Length;

        int[,] distance = new int[sourceLength + 1, targetLength + 1];

        for (int i = 0; i <= sourceLength; distance[i, 0] = i++) ;
        for (int j = 0; j <= targetLength; distance[0, j] = j++) ;

        for (int i = 1; i <= sourceLength; i++)
        {
            for (int j = 1; j <= targetLength; j++)
            {
                int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;

                distance[i, j] = Math.Min(
                    Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1),
                    distance[i - 1, j - 1] + cost);
            }
        }

        return distance[sourceLength, targetLength];
    }
    
    public string AudioClipToString(AudioClip clip)
    {
        float[] samples = new float[clip.samples * clip.channels];
        clip.GetData(samples, 0);

        byte[] byteArray = new byte[samples.Length * sizeof(float)];
        Buffer.BlockCopy(samples, 0, byteArray, 0, byteArray.Length);

        return Convert.ToBase64String(byteArray);
    }

    public AudioClip StringToAudioClip(string base64, string name, int channels, int frequency)
    {
        byte[] byteArray = Convert.FromBase64String(base64);

        float[] samples = new float[byteArray.Length / sizeof(float)];
        Buffer.BlockCopy(byteArray, 0, samples, 0, byteArray.Length);

        AudioClip clip = AudioClip.Create(name, samples.Length / channels, channels, frequency, false);
        clip.SetData(samples, 0);
        return clip;
    }
}
