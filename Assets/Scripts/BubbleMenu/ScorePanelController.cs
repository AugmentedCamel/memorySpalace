using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Newtonsoft.Json.Converters;
using UnityEngine.UI;

public class ScorePanelController : MonoBehaviour
{
    
    [Header("Data")]
    [SerializeField] private int _score;
    [SerializeField] private int _highScore;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _highScoreText;
    [SerializeField] private Slider _scoreSlider;
    [SerializeField] private int highestScore;
    [SerializeField] private Color under50Color = Color.red;
    [SerializeField] private Color under75Color = Color.yellow; 
    [SerializeField] private Color under100Color = Color.cyan;
    [SerializeField] private Image _scoreFillImage;
    [Header("Animation")]
    [SerializeField] private float _textUpdateAnimationDuration = 1f;
    [SerializeField] private float _HighScoreUpdateAnimationDuration = 1f;
    [SerializeField] private AudioSource _scoreUpdateSound;
    [SerializeField] private AudioSource _HighScoreSound;
    [SerializeField] private GameObject _highScoreAchievedTitle;
    [SerializeField] private RectTransform _scoreTextRectTransformBackup;
    
    private bool hasHighScore = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!hasHighScore)
        {
            _highScoreText.text = "---";
        }

        _scoreText.text = "0";
        _highScoreAchievedTitle.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    [Button]
    public void SetScore(int score)
    {
        if (score > 100)
        {
            score = 100;
        }
        _score = score;
        _scoreText.text = _score.ToString();
        _scoreSlider.value = _score;
        
        
    }

    [Button]
    public void SetColorBasedOnscore(int score)
    {
        if (score < 50)
        {
            _scoreFillImage.color = under50Color;
        }
        else if (score < 75)
        {
            _scoreFillImage.color = under75Color;
        }
        else if (score <= 100)
        {
            _scoreFillImage.color = under100Color;
        }
    }
    [Button]
    public void SetHighScore(int highscore)
    {
        hasHighScore = true;
        _highScore = highscore;
        _highScoreText.text = highscore.ToString();
    }
    [Button] 
    public void SetScoreWithAnimation(int score)
    {
        _textUpdateAnimationDuration = (float)score / 50 * _textUpdateAnimationDuration;
        if (score > 100)
        {
            score = 100;
        }
        int currentScore = 0;
        DOTween.To(() => currentScore, x => currentScore = x, score, _textUpdateAnimationDuration)
            .OnUpdate(() => _scoreText.text = currentScore.ToString())
            .SetEase(Ease.OutQuad);
        float originalFontSize = _scoreText.fontSize;
        DOTween.To(() => _scoreText.fontSize, x => _scoreText.fontSize = (float) x, originalFontSize * 1.5, _textUpdateAnimationDuration).OnComplete(() =>
        {
            DOTween.To(() => _scoreText.fontSize, x => _scoreText.fontSize = x, originalFontSize, _textUpdateAnimationDuration / 2);
            if (hasHighScore)
            {
                if (score > _highScore)
                {
                    OnAchieveHighScore(score);
                }
            }
            else
            {
                OnAchieveHighScore(score);
            }
        });
      
        int currentPercentage = 0;
        DOTween.To(() => currentPercentage, x => _scoreSlider.value = x, score, _textUpdateAnimationDuration)
            .OnUpdate(() => SetColorBasedOnscore((int) _scoreSlider.value));
        // Adjust audio properties
        DOTween.To(() => _scoreUpdateSound.volume, x => _scoreUpdateSound.volume = x, 1, _textUpdateAnimationDuration);
        // Optionally, play a sound if it's not already playing
        if (!_scoreUpdateSound.isPlaying)
        {
            _scoreUpdateSound.Play();
        }
        // scale sound speed based on animation duration
        _scoreUpdateSound.pitch = 1 / _textUpdateAnimationDuration;

    }
    
    [Button]
    public void OnAchieveHighScore(int score)
    {
        if (!_HighScoreSound.isPlaying)
        {
            _HighScoreSound.Play();
        }
       
        _highScoreAchievedTitle.SetActive(true);
        GameObject scoreTextCopy = Instantiate(_scoreText.gameObject, _scoreText.transform.position, _scoreText.transform.rotation, _scoreText.transform.parent);
        RectTransform scoreRect = scoreTextCopy.GetComponent<RectTransform>();
        RectTransform highScoreRect = _highScoreText.GetComponent<RectTransform>();
        // Move the current score text to the high score location
        scoreRect.DOMove(highScoreRect.position, _HighScoreUpdateAnimationDuration).OnComplete(() =>
        {
            _highScoreText.text = score.ToString();
            scoreTextCopy.transform.localPosition = _scoreTextRectTransformBackup.localPosition;
            _highScoreAchievedTitle.SetActive(false);
        });
        // adjust scale 
        scoreTextCopy.transform.DOScale(0.7f, _HighScoreUpdateAnimationDuration).OnComplete(() =>
        {
            Destroy(scoreTextCopy);
        });
    }
   
    public void ResetScore()
    {
        _score = 0;
        _scoreText.text = "0";
        _scoreSlider.value = 0;
        _highScore = 0;
        _highScoreText.text = "0";
    }
    
}
