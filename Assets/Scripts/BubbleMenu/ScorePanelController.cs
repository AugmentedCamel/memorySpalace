using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using DG.Tweening;
public class ScorePanelController : MonoBehaviour
{
    
    [Header("Data")]
    [SerializeField] private int _score;
    [SerializeField] private int _highScore;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _highScoreText;
    
    
    [Header("Animation")]
    [SerializeField] private float _textUpdateAnimationDuration = 1f;
    [SerializeField] private float _HighScoreUpdateAnimationDuration = 1f;
    [SerializeField] private GameObject _HighScoreParticleEffect;
    [SerializeField] private GameObject _regularScoreTitle;
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
        _regularScoreTitle.SetActive(true);
        _highScoreAchievedTitle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    [Button]
    public void SetScore(int score)
    {
        _score = score;
        _scoreText.text = _score.ToString();
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
        int currentScore = 0;
        DOTween.To(() => currentScore, x => currentScore = x, score, _textUpdateAnimationDuration)
            .OnUpdate(() => _scoreText.text = currentScore.ToString())
            .SetEase(Ease.OutQuad);
        _scoreText.transform.DOScale(1.5f, _textUpdateAnimationDuration).OnComplete(() =>
        {
            _scoreText.transform.DOScale(1f, _textUpdateAnimationDuration);
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

    }
    
    [Button]
    public void OnAchieveHighScore(int score)
    {
        _regularScoreTitle.SetActive(false);
        _highScoreAchievedTitle.SetActive(true);
        GameObject scoreTextCopy = Instantiate(_scoreText.gameObject, _scoreText.transform.position, _scoreText.transform.rotation, _scoreText.transform.parent);
        RectTransform scoreRect = scoreTextCopy.GetComponent<RectTransform>();
        RectTransform highScoreRect = _highScoreText.GetComponent<RectTransform>();
        // Move the current score text to the high score location
        scoreRect.DOMove(highScoreRect.position, _HighScoreUpdateAnimationDuration).OnComplete(() =>
        {
            _highScoreText.text = score.ToString();
            scoreTextCopy.transform.localPosition = _scoreTextRectTransformBackup.localPosition;
            _regularScoreTitle.SetActive(true);
            _highScoreAchievedTitle.SetActive(false);
        });
        // adjust scale 
        scoreTextCopy.transform.DOScale(0.7f, _HighScoreUpdateAnimationDuration).OnComplete(() =>
        {
            Destroy(scoreTextCopy);
        });
    }
   
    
}
