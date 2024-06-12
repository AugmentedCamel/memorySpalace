using System;
using Oculus.Voice;
using UnityEngine;
using UnityEngine.Events;

public class SpeechToTextManager : MonoBehaviour
{
    #region Public Variables

    // Singleton Instance
    public static SpeechToTextManager Instance;

    #endregion
    
    #region SerializeField Variables - need reference

    // Wit AI Application Voice Experience
    [SerializeField] private AppVoiceExperience _appVoice;

    #endregion

    #region Private Variables
    
    private AudioClip _currentAudioClip;
    private bool _isRecording = false;
    private bool _isTesting = false;

    private UnityEvent<string> _currentPartialEvent;
    private UnityEvent<string> _currentFullTextEvent;
    private UnityEvent<AudioClip> _currentAudioClipEvent;
    
    #endregion

    #region Private Basic Methods
    
    /// <summary>
    /// Ensures Singleton Pattern - only one instance of the manager - easy access
    /// </summary>
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
    
    #endregion
    
    #region Recordings
    
    /// <summary>
    /// Method to record only a text.
    /// Recognized text is returned as the event parameter, as the method is asynchronous.
    /// </summary>
    public void StartUserTest(UnityEvent<string> recognizedTextEvent)
    {
        if(_isRecording) return;

        _isTesting = true;
        
        // set events
        _currentFullTextEvent = recognizedTextEvent;
        
        StartRecording(); 
        
        // full text recognition
        StartFullTextRecognition();
    }

    /// <summary>
    /// Method to record full text and audioclip.
    /// Recognized text is returned as the recognizedTextEvent parameter - asynchronous call.
    /// Recorded clip is returned as the recognizedAudioEvent parameter - asynchronous call.
    /// </summary>
    public void StartRecording(UnityEvent<string> recognizedTextEvent, UnityEvent<AudioClip> recognizedAudioEvent)
    {
        if(_isRecording) return;

        //set events
        _currentAudioClipEvent = recognizedAudioEvent;
        _currentFullTextEvent = recognizedTextEvent;
        
        StartRecording();
        
        // full text recognition
        StartFullTextRecognition();
    }
    
    /// <summary>
    /// Method to record partial text, full text and audioclip.
    /// Partially recognized text is returned as the partialRecognizedText parameter - asynchronous call.
    /// Full recognized text is returned as the recognizedTextEvent parameter - asynchronous call.
    /// Recorded clip is returned as the recognizedAudioEvent parameter - asynchronous call.
    /// </summary>
    public void StartRecording(UnityEvent<string> recognizedTextEvent, UnityEvent<string> partialRecognizedText, UnityEvent<AudioClip> recognizedAudioEvent)
    {
        if(_isRecording) return;
        
        // set events
        _currentAudioClipEvent = recognizedAudioEvent;
        _currentFullTextEvent = recognizedTextEvent;
        _currentPartialEvent = partialRecognizedText;
        
        StartRecording();
        
        // partial text recognition
        StartPartialTextRecognition();

        // full text recognition
        StartFullTextRecognition();
    }

    private void StartFullTextRecognition()
    {
        _appVoice.VoiceEvents.OnFullTranscription.RemoveAllListeners();
        _appVoice.VoiceEvents.OnFullTranscription.AddListener(transcription =>
        {
            if (_isTesting && !_isRecording)
            {
                _isTesting = false;
                _appVoice.VoiceEvents.OnFullTranscription.RemoveAllListeners();
                _appVoice.Deactivate();
                _currentFullTextEvent.Invoke(transcription);
                _currentFullTextEvent.Invoke("FINISHED");
            }
            else
            {
                _appVoice.Deactivate();
                _appVoice.Activate();
                _currentFullTextEvent.Invoke(transcription);
            }

            // activate again to be able to recognize text with stoppings.
            _appVoice.VoiceEvents.OnFullTranscription.RemoveAllListeners();
            _appVoice.VoiceEvents.OnFullTranscription.AddListener(transcription =>
            {
                if (_isTesting && !_isRecording)
                {
                    _isTesting = false;
                    _appVoice.VoiceEvents.OnFullTranscription.RemoveAllListeners();
                    _appVoice.Deactivate();
                    _currentFullTextEvent.Invoke(transcription);
                    _currentFullTextEvent.Invoke("FINISHED");
                }
                else
                {
                    _appVoice.Deactivate();
                    _appVoice.Activate();
                    _currentFullTextEvent.Invoke(transcription);
                }
            });
        });
    }

    private void StartPartialTextRecognition()
    {
        _appVoice.VoiceEvents.OnPartialTranscription.RemoveAllListeners();
        _appVoice.VoiceEvents.OnPartialTranscription.AddListener(partialTranscrition =>
        {
            _currentPartialEvent.Invoke(partialTranscrition);
        });
    }

    /// <summary>
    /// Initiates recording.
    /// </summary>
    private void StartRecording()
    {
        _isRecording = true;
        if (!_isTesting)
        {
            _currentAudioClip = Microphone.Start(Microphone.devices[1], false, 180, 44100);
        }
        _appVoice.Deactivate();
        _appVoice.Activate();
    }
    
    /// <summary>
    /// Stops the recording.
    /// </summary>
    public void StopRecording()
    {
        if (_isTesting)
        {
            _isRecording = false;
        }
        else
        {
            _isRecording = false;
            _appVoice.Deactivate();
            Microphone.End(Microphone.devices[1]);
            _appVoice.VoiceEvents.OnFullTranscription.RemoveAllListeners();
            _appVoice.VoiceEvents.OnPartialTranscription.RemoveAllListeners();

            // send the data to listeners
            _currentAudioClipEvent.Invoke(_currentAudioClip);

            // delete temporary data
            _currentAudioClipEvent = null;
            _currentPartialEvent = null;
            _currentFullTextEvent = null;
            _currentAudioClip = null;
        }
    }
    
    #endregion

    #region Percentage Calculation
    
    /// <summary>
    /// Recalculate similarity to percentage.
    /// </summary>
    public float CalculateSimilarityPercentage(string source, string target)
    {
        int maxLength = Math.Max(source.Length, target.Length);
        if (maxLength == 0) return 100.0f;

        int distance = LevenshteinDistance(source, target);
        return ((float)(maxLength - distance) / maxLength) * 100;
    }
    
    /// <summary>
    /// Algorithm to calculate the similarity of two texts.
    /// </summary>
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
    
    #endregion
    
    #region Converter
    
    /// <summary>
    /// Converts AudioClip to string.
    /// </summary>
    public string AudioClipToString(AudioClip clip)
    {
        float[] samples = new float[clip.samples * clip.channels];
        clip.GetData(samples, 0);

        byte[] byteArray = new byte[samples.Length * sizeof(float)];
        Buffer.BlockCopy(samples, 0, byteArray, 0, byteArray.Length);

        return Convert.ToBase64String(byteArray);
    }

    /// <summary>
    /// Converts string to AudioClip.
    /// </summary>
    public AudioClip StringToAudioClip(string base64, string name, int channels, int frequency, int samples)
    {
        byte[] byteArray = Convert.FromBase64String(base64);

        float[] floatArray = new float[byteArray.Length / sizeof(float)];
        Buffer.BlockCopy(byteArray, 0, floatArray, 0, byteArray.Length);

        AudioClip clip = AudioClip.Create(name, samples, channels, frequency, false);
        clip.SetData(floatArray, 0);
        return clip;
    }
    
    #endregion
}
