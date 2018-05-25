using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConditionsController : MonoBehaviour
{

    #region Variables 

    internal static GameConditionsController instance = null;


    [SerializeField] tk2dUIItem startGameButton;
    [SerializeField] tk2dUIItem restart;
    [SerializeField] tk2dUIItem restartSecondItem;
    [SerializeField] GameObject startTextGO;
        

    private Color endColor;
    private bool isStarted;
    private bool isOnPauseMenu;
    private bool isAlive;
    private bool isReadyTORestart;

    #endregion



    #region Properties

    public bool IsStartedProperty
    {
        get
        {
            return isStarted;
        }
        set
        {
            isStarted = value;
        }

    }
   
    public bool IsAlive
    {
        get
        {
            return isAlive;
        }
        set
        {
            isAlive = value;
        }
    }

    public bool IsReadyTORestartProperty
    {
        get
        {
            return isReadyTORestart;
        }
        set
        {
            isReadyTORestart = value;
        }
    }

    public bool IsOnPauseMenuproperty {
        get
        {
            return isOnPauseMenu;
        }
        set
        {
            isOnPauseMenu = value;
        }


    }

    #endregion



    #region Unity lifycycle

    void Start()
    {       
        if (instance == null)
            instance = this;
        else if (instance == this)
            Destroy(gameObject);

        isReadyTORestart = false;
        IsStartedProperty = false;
        endColor = new Color(1, 1, 1, 0);
        TileSpawner.instance.BuildLvl();
    }


    private void OnEnable()
    {
        startGameButton.OnClick += Button_Begin;
        restart.OnClick += Button_GCReplay;
        restartSecondItem.OnClick += Button_GCReplay;
    }


    private void OnDisable()
    {

        startGameButton.OnClick -= Button_Begin;
        restart.OnClick -= Button_GCReplay;
        restartSecondItem.OnClick -= Button_GCReplay;

    }


    void Update()
    {
        if (TileSpawner.instance.isPausedProperty && !isOnPauseMenu)
        {
            isOnPauseMenu = true;

        }
        else if (!TileSpawner.instance.isPausedProperty && isOnPauseMenu)
        {
            isOnPauseMenu = false;
        }
    }

    #endregion



    #region Public methods

    public void ShowDeathMenu()
    {
        DeathMenuActivator.instance.ActivateColor();
    }

    #endregion



    #region Event handlers

    public void Button_GCReplay()
    {
        if (isReadyTORestart)
        {
            IsStartedProperty = false;
        IsAlive = true;
        DeathMenuActivator.instance.DiactivateColor();
        ScoreController.instance.scoreProperty = 0;
        TileSpawner.instance.StopAllCoroutines();

            if (isOnPauseMenu)
            {
               TileSpawner.instance.RestartLvl();
               Scheduler.Instance.CallMethodWithDelay(this.gameObject, PauseMenuActivator.Instance.DiactivateColor,1.5f);
            }
        }
    }


    public void Button_Begin()
    {
        if (!IsStartedProperty)
        {
            TweenColor.SetColor(startTextGO, endColor,1);
            TileSpawner.instance.StartGame();
            IsStartedProperty = true;
        }
    }

    #endregion


}
