using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuActivator : MonoBehaviour {

    #region Varibles

    internal static PauseMenuActivator Instance = null;


    [SerializeField]
    GameObject soundBtn;
    [SerializeField]
    GameObject backGround;
    [SerializeField]
    GameObject menuLabel;
    [SerializeField]
    GameObject keepGoingBtn;
    [SerializeField]
    GameObject keepGoingBtnText;
    [SerializeField]
    GameObject tryAgainBtnText;
    [SerializeField]
    GameObject tryAgainBtn;
    [SerializeField]
    GameObject leaderBoardBtn;
    [SerializeField]
    GameObject leaderBoardBtnText;
    [SerializeField]
    GameObject rateBtn;
    [SerializeField]
    GameObject rateBtnText;
    [SerializeField]
    GameObject moreGamesBtn;
    [SerializeField]
    GameObject moreGamesBtnText;


    private Color btnColor;
    private Color textColor;
    private Color diactivColor;

    #endregion
      


    #region Unity lifeCycle

    void Start()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance == this)
            Destroy(gameObject);

        btnColor = new Color(1f, 1f, 1f, 1f);
        textColor = new Color32(255, 82, 112, 255);
        diactivColor = new Color(1, 1, 1, 0);
    }

    #endregion


    
    #region Public methods

    public void ActivateColor()
    {
        TweenColor.SetColor(soundBtn, btnColor, 1);
        TweenColor.SetColor(backGround, btnColor, 1);
        TweenColor.SetColor(menuLabel, btnColor, 1);
        TweenColor.SetColor(rateBtn, btnColor, 1);
        TweenColor.SetColor(rateBtnText, textColor, 1);
        TweenColor.SetColor(moreGamesBtn, btnColor, 1);
        TweenColor.SetColor(moreGamesBtnText, textColor, 1);
        TweenColor.SetColor(keepGoingBtnText, textColor, 1);
        TweenColor.SetColor(keepGoingBtn, btnColor, 1);
        TweenColor.SetColor(leaderBoardBtn, btnColor, 1);
        TweenColor.SetColor(leaderBoardBtnText, textColor, 1);
        TweenColor.SetColor(tryAgainBtn, btnColor, 1);
        TweenColor.SetColor(tryAgainBtnText, textColor, 1);
    }


    public void DiactivateColor()
    {
        TweenColor.SetColor(soundBtn, diactivColor, 0);
        TweenColor.SetColor(backGround, diactivColor, 0);
        TweenColor.SetColor(menuLabel, diactivColor, 0);
        TweenColor.SetColor(rateBtn, diactivColor, 0);
        TweenColor.SetColor(rateBtnText, diactivColor, 0);
        TweenColor.SetColor(moreGamesBtn, diactivColor, 0);
        TweenColor.SetColor(moreGamesBtnText, diactivColor, 0);
        TweenColor.SetColor(keepGoingBtnText, diactivColor, 0);
        TweenColor.SetColor(keepGoingBtn, diactivColor, 0);
        TweenColor.SetColor(leaderBoardBtn, diactivColor, 0);
        TweenColor.SetColor(leaderBoardBtnText, diactivColor, 0);
        TweenColor.SetColor(tryAgainBtn, diactivColor, 0);
        TweenColor.SetColor(tryAgainBtnText, diactivColor, 0);
    }

    #endregion
}
