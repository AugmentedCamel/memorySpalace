using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SpeechBubble : MonoBehaviour
{
    #region SerializeFiled variables - need references
    
    // the bubbles recorded text
    [SerializeField] private TextMeshPro _recordedText;
    
    // percentage text
    [SerializeField] private TextMeshPro _percentageSimilarity;
    [SerializeField] private GameObject _scoreDisplay;
    [SerializeField] private TextDisplayer _testTextDisplayer;
    // audio source to play the clip
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private ObjectBubble _objectBubble;

    #endregion

    #region Private Variables
    
    private UnityEvent<string> _speechTextEvent = new UnityEvent<string>();
    private UnityEvent<string> _partialTranscriptionEvent = new UnityEvent<string>();
    private UnityEvent<AudioClip> _speechAudioEvent = new UnityEvent<AudioClip>();
    private string _currentString = "";
    private AudioClip _audioClip = null;
    
    #endregion

    #region Private Record Methods

    private void OnDisable()
    {
        _recordedText.gameObject.SetActive(false);
    }

    /// <summary>
    /// Records partial text, full text and audio of users input.
    /// </summary>
    private void RecordClipAndText()
    {
        _recordedText.text = "";
        _percentageSimilarity.text = "";
        _audioClip = null;
        
        SpeechToTextManager.Instance.StartRecording(_speechTextEvent, _partialTranscriptionEvent, _speechAudioEvent);
        
        // listen to partial recognized text
        RecordPartialText();
        
        // record the full text
        RecordFullText();

        // listen to audio recording
        RecordAudio();
    }

    /// <summary>
    /// Initiates audio recording.
    /// </summary>
    private void RecordAudio()
    {
        _speechAudioEvent.AddListener(recordedAudio =>
        {
            _audioClip = recordedAudio;
        });
    }

    /// <summary>
    /// Initiates full transcription of users input.
    /// </summary>
    private void RecordFullText()
    {
        _speechTextEvent.RemoveAllListeners();
        _speechTextEvent.AddListener(recognizedText =>
        {
            _currentString += " " + recognizedText;
            _recordedText.text = _currentString;
        });
    }

    /// <summary>
    /// Initiates partial transcription of users input.
    /// </summary>
    private void RecordPartialText()
    {
        _partialTranscriptionEvent.RemoveAllListeners();
        _partialTranscriptionEvent.AddListener(partialText =>
        {
            _recordedText.text = _currentString + " " + partialText;
        });
    }

    /// <summary>
    /// Stops all listeners form recording audio and text.
    /// </summary>
    private void StopRecordings()
    {
        _currentString = _recordedText.text;
        
        SpeechToTextManager.Instance.StopRecording();
        
        _speechTextEvent.RemoveAllListeners();
        _partialTranscriptionEvent.RemoveAllListeners();
        _speechAudioEvent.RemoveAllListeners();
    }
    
    /// <summary>
    /// Initiates comparison of the bubble data and users input.
    /// </summary>
    private void TestUser()
    {
        SpeechToTextManager.Instance.StartUserTest(_speechTextEvent);

        var fullRecognizedText = "";
        
        // listen to recognized text
        _speechTextEvent.RemoveAllListeners();
        _speechTextEvent.AddListener(recognizedText =>
        {
            if (recognizedText.Contains("FINISHED"))
            {
                _testTextDisplayer.UpdateText(fullRecognizedText);
                float similarity =
                    SpeechToTextManager.Instance.CalculateSimilarityPercentage(_currentString, fullRecognizedText);
                _percentageSimilarity.text = " Success Rate: " + similarity.ToString("F");
                _scoreDisplay.SetActive(true);
                _scoreDisplay.GetComponent<ScorePanelController>().SetScoreWithAnimation((int)similarity);
                _speechTextEvent.RemoveAllListeners();
            }
            else
            {
                fullRecognizedText += " " + recognizedText;
                Debug.Log("_currentString: " + _currentString);
                Debug.Log("recognizedText: " + fullRecognizedText);
            }
        });
    }
    
    #endregion

    #region Public Methods for the UI
    
    /// <summary>
    /// Method to call the comparison between bubbles set text and users input.
    /// </summary>
    public void Test()
    {
        TestUser();
    }

    /// <summary>
    /// Method to record AudioClip and text of what the user is saying.
    /// </summary>
    public void StartRecording()
    {
        RecordClipAndText();
    }

    /// <summary>
    /// Method to stop recording audio and text.
    /// </summary>
    public void StopRecording()
    {
        _objectBubble.AssignObject();
        
        StopRecordings();
    }

    /// <summary>
    /// Toggles active/inactive the text of the bubble.
    /// </summary>
    public void ToggleTranscribedText()
    {
        if (_recordedText.gameObject.activeSelf)
        {
            HideTranscribedText();
        }
        else
        {
            ShowTranscribedText();
        }
    }

    /// <summary>
    /// Shows the text of the bubble.
    /// </summary>
    public void ShowTranscribedText()
    {
        // _recordedText.text = _currentString;
        _recordedText.gameObject.SetActive(true);
    }

    /// <summary>
    /// Hides the text of the bubble.
    /// </summary>
    public void HideTranscribedText()
    {
        _recordedText.gameObject.SetActive(false);
    }

    /// <summary>
    /// Repeats the set AudioClip.
    /// </summary>
    [Button]
    public void RepeatAudioClip()
    {
        if(_audioClip == null) return;

        _audioSource.clip = _audioClip;
        _audioSource.Play();
    }

    /// <summary>
    /// Deletes bubbles Data.
    /// </summary>
    public void DeleteData()
    {
        _audioClip = null;
        _currentString = "";
        _recordedText.text = "";
        _percentageSimilarity.text = "";
        _scoreDisplay.GetComponent<ScorePanelController>().ResetScore();
        _scoreDisplay.SetActive(false);
        
    }

    /// <summary>
    /// Returns whether the bubble has data. True if it has false otherwise.
    /// </summary>
    public bool HasData()
    {
        return (_audioClip != null);
    }
    
    #endregion

    #region Public Getters of Bubble Data

    /// <summary>
    /// Get the current text of the bubble.
    /// </summary>
    public string GetBubbleText()
    {
        return _recordedText.text;
    }

    /// <summary>
    /// Returns the bubbles audio clip as a string.
    /// </summary>
    public string GetBubbleAudioClipAsString()
    {
        if (_audioClip == null)
        {
            return "";
        }
        
        return SpeechToTextManager.Instance.AudioClipToString(_audioClip);
    }

    /// <summary>
    /// Returns the current AudioClip channels.
    /// </summary>
    public int GetAudioClipChannels()
    {
        if (_audioClip == null)
        {
            return 0;
        }
        return _audioClip.channels;
    }

    /// <summary>
    /// Returns the current AudioClip frequency.
    /// </summary>
    public int GetAudioClipFrequency()
    {
        if (_audioClip == null)
        {
            return 0;
        }
        
        return _audioClip.frequency;
    }

    /// <summary>
    /// Returns the current AudioClip samples.
    /// </summary>
    public int GetAudioClipSamples()
    {
        if (_audioClip == null)
        {
            return 0;
        }
        
        return _audioClip.samples;
    }

    #endregion

    #region Public Setters of the Bubble Data

    /// <summary>
    /// Sets the text of the bubble.
    /// </summary>
    public void SetBubbleText(string text)
    {
        _currentString = text;
        _recordedText.text = _currentString;
    }

    /// <summary>
    /// Sets the bubbles audio clip from string.
    /// </summary>
    public void SetBubbleAudioClipFromString(string audioClipString, int channels, int frequency, int samples)
    {
        _audioClip = SpeechToTextManager.Instance.StringToAudioClip(audioClipString, "Microphone", channels, frequency, samples);
        _audioSource.clip = _audioClip;
    }

    #endregion
}
