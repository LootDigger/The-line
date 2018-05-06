using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConditionsController : SingletonMonoBehaviour<GameConditionsController>
{

    #region Variables 

    private const float SCREEN_HEIGHT = 1136f;
    private const float SCREEN_WIDTH = 640f;

    [SerializeField] tk2dUIItem startGameButton;
    [SerializeField] tk2dUIItem restart;
    [SerializeField] tk2dUIItem restartSecondItem;
    [SerializeField] GameObject startTextGO;
    [SerializeField] int countOfCellsY = 11;
    [SerializeField] int countOfCellsX = 7;
    [SerializeField] Color endColor;
    [SerializeField] float speed;
    [SerializeField] float boosterSpawnFrequency;


    bool isStarted;
    bool isOnPauseMenu;
    bool isAlive;
    bool isReadyTORestart;
    int currentLinePosition;
    float cellHeight = 160;
    float cellWidth = 90;

    #endregion



    #region Properties

    public float SpawnFrequency
    {
        get
        {
            return boosterSpawnFrequency;            
        }
    }



    public float Speed
    {
        get
        {
            return speed;
        }
    }


    public float CellHeight
    {
        get
        {
            return cellHeight;
        }
        set
        {
            cellHeight = value;
        }
    }


    public float CellWidth
    {
        get
        {
            return cellWidth;
        }
        set
        {
            cellWidth = value;
        }
    }
    

    public float ScreenHeight
    {
        get
        {
            return SCREEN_HEIGHT;
        }
    }


    public float ScreenWidth
    {
        get
        {
            return SCREEN_WIDTH;
        }
    }
    

    public float CountOfCellsY
    {
        get
        {
            return countOfCellsY;
        }
    }


    public float CountOfCellsX
    {
        get
        {
            return countOfCellsX;
        }
    }

    
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
        isReadyTORestart = false;
        IsStartedProperty = false;
        endColor = new Color(1, 1, 1, 0);
        TileSpawner.Instance.BuildLvl();
        currentLinePosition = (countOfCellsX + 1) / 2;
    }

     void Instance_UpdateInfo(float cellWidth, float cellHeight)
    {
        this.cellHeight = cellHeight;
        this.cellWidth = cellWidth;
    }

    private void OnEnable()
    {
        startGameButton.OnClick += Button_Begin;
        restart.OnClick += Button_GCReplay;
        restartSecondItem.OnClick += Button_GCReplay;
        SpawnArray.Instance.UpdateInfo += Instance_UpdateInfo;
    }


    private void OnDisable()
    {
        startGameButton.OnClick -= Button_Begin;
        restart.OnClick -= Button_GCReplay;
        restartSecondItem.OnClick -= Button_GCReplay;
        SpawnArray.Instance.UpdateInfo -= Instance_UpdateInfo;
    }


    void Update()
    {
        if (TileSpawner.Instance.IsPausedProperty && !isOnPauseMenu)
        {
            isOnPauseMenu = true;
        }
        else if (!TileSpawner.Instance.IsPausedProperty && isOnPauseMenu)
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


    public void CalculateLength()
    {
        

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
        ScoreController.instance.ScoreProperty = 0;
        TileSpawner.Instance.StopAllCoroutines();

            if (isOnPauseMenu)
            {
               TileSpawner.Instance.RestartLvl();
               Scheduler.Instance.CallMethodWithDelay(this.gameObject, PauseMenuActivator.Instance.DiactivateColor,1.5f);
            }
        }
    }


    public void Button_Begin()
    {
        if (!IsStartedProperty)
        {
            TweenColor.SetColor(startTextGO, endColor,1);
            TileSpawner.Instance.StartGame();
            IsStartedProperty = true;
        }
    }



    #endregion


}
