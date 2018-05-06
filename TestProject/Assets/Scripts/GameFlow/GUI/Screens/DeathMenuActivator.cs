using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenuActivator : MonoBehaviour
{

    #region Varibles

    internal static DeathMenuActivator instance = null;

    
    [SerializeField]
     GameObject backGround;
    [SerializeField]
     GameObject gameOverLabel;
    [SerializeField]
     GameObject scorePanelBG;
    [SerializeField]
    GameObject scorePanelText;
    [SerializeField]
     GameObject score;
    [SerializeField]
     GameObject bestScoreText;
    [SerializeField]
     GameObject bestScore;
    [SerializeField]
     GameObject shareBtn;
    [SerializeField]
     GameObject shareBtnText;
    [SerializeField]
     GameObject leaderBoardBtn;
    [SerializeField]
     GameObject leaderBoardBtnText;
    [SerializeField]
     GameObject tryAgainBtn;
    [SerializeField]
     GameObject tryAgainBtnText;
    

     private Color btnColor;
     private Color textColor;
     private Color diactivColor;

    #endregion



    #region Unity lifeCycle

    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance == this)
            Destroy(gameObject);

        btnColor = new Color(1f, 1f, 1f, 1f);
        textColor = new Color32(255, 82, 112, 255);
        diactivColor = new Color(1, 1, 1, 0);
    }

    #endregion
    


    #region Public methods

    public void ActivateColor()
    {
        TweenColor.SetColor(backGround, btnColor, 1);
        TweenColor.SetColor(gameOverLabel, btnColor, 1);
        TweenColor.SetColor(scorePanelBG, btnColor, 1);
        TweenColor.SetColor(scorePanelText, textColor, 1);
        TweenColor.SetColor(score, textColor, 1);
        TweenColor.SetColor(bestScore, textColor, 1);
        TweenColor.SetColor(bestScoreText, textColor, 1);
        TweenColor.SetColor(shareBtn, btnColor, 1);
        TweenColor.SetColor(shareBtnText, textColor, 1);
        TweenColor.SetColor(leaderBoardBtn, btnColor, 1);
        TweenColor.SetColor(leaderBoardBtnText, textColor, 1);
        TweenColor.SetColor(tryAgainBtn, btnColor, 1);
        TweenColor.SetColor(tryAgainBtnText, textColor, 1);
        
    }


    public void DiactivateColor()
    {
        TweenColor.SetColor(backGround, diactivColor, 0);
        TweenColor.SetColor(gameOverLabel, diactivColor, 0);
        TweenColor.SetColor(scorePanelBG, diactivColor, 0);
        TweenColor.SetColor(scorePanelText, diactivColor, 0);
        TweenColor.SetColor(score, diactivColor, 0);
        TweenColor.SetColor(bestScore, diactivColor, 0);
        TweenColor.SetColor(bestScoreText, diactivColor, 0);
        TweenColor.SetColor(shareBtn, diactivColor, 0);
        TweenColor.SetColor(shareBtnText, diactivColor, 0);
        TweenColor.SetColor(leaderBoardBtn, diactivColor, 0);
        TweenColor.SetColor(leaderBoardBtnText, diactivColor, 0);
        TweenColor.SetColor(tryAgainBtn, diactivColor, 0);
        TweenColor.SetColor(tryAgainBtnText, diactivColor, 0);
    }

    #endregion

}
