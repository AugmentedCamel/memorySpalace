using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SpeechBubble : MonoBehaviour
{
    [SerializeField] private MenuBubbleInteractor _bubbleInteractor;
    [SerializeField] private TextMeshPro _text;
    [SerializeField] private TextMeshPro _percentageSimilarity;
    [SerializeField] private AudioSource _audioSource;

    private UnityEvent<string> _speechTextEvent = new UnityEvent<string>();
    private UnityEvent<string> _partialTranscriptionEvent = new UnityEvent<string>();
    private UnityEvent<AudioClip> _speechAudioEvent = new UnityEvent<AudioClip>();
    private string _currentString = "";
    private AudioClip _audioClip = null;
    
    private void Start()
    {
        _bubbleInteractor.onTriggerEnteredEvent.AddListener(delegate
        {
            RecordClipAndText();
            _bubbleInteractor.onTriggerEnteredEvent.RemoveAllListeners();
            
            // second try should write down the percentage
            _bubbleInteractor.onTriggerEnteredEvent.AddListener(delegate
            {
                TestUser();
                Invoke(nameof(RepeatAudioClip), 3f);
            });
        });
    }
    
    private void TestUser()
    {
        SpeechToTextManager.Instance.StartRecording(_speechTextEvent);
        // set recognized text
        _speechTextEvent.RemoveAllListeners();
        _speechTextEvent.AddListener(recognizedText =>
        {
            _percentageSimilarity.text = recognizedText + " percentage: " + SpeechToTextManager.Instance.CalculateSimilarityPercentage(_currentString, recognizedText).ToString();
            _speechTextEvent.RemoveAllListeners();
        });
    }

    private void RecordClipAndText()
    {
        SpeechToTextManager.Instance.StartRecording(_speechTextEvent, _partialTranscriptionEvent, _speechAudioEvent);
        // set recognized text
        
        _partialTranscriptionEvent.RemoveAllListeners();
        _partialTranscriptionEvent.AddListener(partialText =>
        {
            _text.text = partialText;
        });
        
        _speechTextEvent.RemoveAllListeners();
        _speechTextEvent.AddListener(recognizedText =>
        {
            _currentString = recognizedText;
            _text.text = recognizedText;
            _speechTextEvent.RemoveAllListeners();
            _partialTranscriptionEvent.RemoveAllListeners();
        });
                
        // set recorded audioclip
        _speechAudioEvent.AddListener(recordedAudio =>
        {
            _audioClip = recordedAudio;
            _speechAudioEvent.RemoveAllListeners();
        });
    }

    public void Rerecord()
    {
        RecordClipAndText();
    }

    public void ShowTranscribedText()
    {
        _text.text = _currentString;
        _text.gameObject.SetActive(true);
    }

    public void HideTranscribedText()
    {
        _text.gameObject.SetActive(false);
    }

    public void RepeatAudioClip()
    {
        if(_audioClip == null) return;

        // this was for test to convert audio to string and retrieve it from playerprefs
        // var stringAudio = SpeechToTextManager.Instance.AudioClipToString(_audioClip);
        // PlayerPrefs.SetString("recording", stringAudio);
        // _audioSource.clip = SpeechToTextManager.Instance.StringToAudioClip(PlayerPrefs.GetString("recording"), _audioClip.name, _audioClip.channels, _audioClip.frequency);

        _audioSource.clip = _audioClip;
        _audioSource.Play();
    }

    public void DeleteData()
    {
        _audioClip = null;
        _currentString = "";
    }

    public bool HasData()
    {
        return (_audioClip != null);
    }
}
