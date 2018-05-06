using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{

    #region Variables

    private const float TEXT_FADE_TIME = 0.2f;
    private const int SCORE_START_VALUE = 5;
    private const float SCORE_ADD_RATE = 0.1f;


    internal static ScoreController instance = null;   


    [SerializeField] tk2dTextMesh scoreText;
    [SerializeField] tk2dTextMesh resultScoreText;
    [SerializeField] tk2dTextMesh resultBestScoreText;
    [SerializeField] tk2dTextMesh diactivateTimeText;
    [SerializeField] tk2dTextMesh diactivateText;   
    

    private float firstTimer;
    private float secondTimer;
    private Color startColor;
    private Color endColor;
    private int timeVar;
    private int score;
    private int bestScore;
    private bool isTimerStarted;

    #endregion



    #region Properties

    public int ScoreProperty
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }

    }

    #endregion



    #region Unity lifecycle

    void Start()
	{
        if (instance == null)
            instance = this;
        else if (instance == this)
            Destroy(gameObject);

        bestScore = 0;
        score = 0;
        firstTimer = 0;
        secondTimer = 0;
        isTimerStarted = false;
        timeVar = 5;
        startColor = new Color(1, 1, 1, 1);
        endColor = new Color(1, 1, 1, 0);
    }


	private void Update()
	{
        AddScore();
        if (isTimerStarted)
        {
            Timer();
            ScoreController.instance.SetVisibleTimerColor(startColor);
        }
        else
        {
            ScoreController.instance.SetVisibleTimerColor(endColor);
            secondTimer = Time.time;
        }
    }

    #endregion



    #region Public methods

    public void SetVisibleTimerColor(Color col)
    {
        TweenColor.SetColor(diactivateText.gameObject, col, TEXT_FADE_TIME);
        TweenColor.SetColor(diactivateTimeText.gameObject, col, TEXT_FADE_TIME);
    }


    public void SetTimerStarted()
    {
        timeVar = SCORE_START_VALUE;
        isTimerStarted = true;
        diactivateTimeText.text = timeVar.ToString();
    }


    public void DiactivateTimer()
    {
        isTimerStarted = false;
    }

    #endregion



    #region Private methods

    void AddScore()
    {
        if (GameConditionsController.Instance.IsAlive && 
            GameConditionsController.Instance.IsStartedProperty && 
            !TileSpawner.Instance.IsPausedProperty)
        {
            if (Time.time >= firstTimer + SCORE_ADD_RATE)
            {
                score++;
                firstTimer = Time.time;              
            }    
        }
        else
        {
            if (bestScore < score)
                bestScore = score;
        }

        scoreText.text = score.ToString();
        resultScoreText.text = score.ToString();
        resultBestScoreText.text = bestScore.ToString();
    }


    void Timer()
    {
        if (Time.time >= secondTimer + 1 && timeVar >= 0 && GameConditionsController.Instance.IsAlive)
        {       
            timeVar--;
            diactivateTimeText.text = timeVar.ToString();
            secondTimer = Time.time;
        }

        if(timeVar == 0)
        {
            SetVisibleTimerColor(endColor);
        }

    }
    
    #endregion
    
}
