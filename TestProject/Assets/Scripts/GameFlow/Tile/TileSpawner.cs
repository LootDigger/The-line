using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TileSpawner : SingletonMonoBehaviour<TileSpawner>
{
    #region variables

    private const float TITLE_WIDTH = 90f;
    private const float TITLE_HEIGHT = 160;
    private const float REVERT_POSITION_Y = -180f;
    private const int ROW_MIDDLE_ELEMENT = 3;
    private const float SCREEN_HEIGHT = 1136f;
    private const float SCREEN_WIDTH = 640f;
    

    [SerializeField] GameObject tile;
    [SerializeField] GameObject warning;
    [SerializeField] GameObject startText;
    [SerializeField] tk2dUIItem menu;
    [SerializeField] tk2dUIItem keepGoing;
    [SerializeField] private float ballSpeed = 1f;
    [SerializeField] private int spawnBoosterFrequency = 3;
    [SerializeField] private Color endColor = new Color(1, 1, 1, 0);
    [SerializeField] private Color startColor = new Color(1, 1, 1, 1);
    [SerializeField] private int Y_TILE_HEIGHT = 9;

    
    private List<GameObject> tileList = new List<GameObject>();
    private GameObject tilesParent;
    private Vector3 firstTileSpawnPos;     
    private Vector3 ballSpawnPosition;   
    private bool isNeedToMakeTransition;
    private bool isPaused;
    private bool isReadyToRun;
    private bool isNeedToSpawnBooster;
    private int currentTonnelPosition;
    private int prevTonnelPosition;
    private int directionGrowLength;
    private int growLength;
    private int counter;
    private int boosterCounter;

    #endregion



    #region Properties

    public bool IsNeedToMakeTransitionProperty
    {
        get
        {
            return isNeedToMakeTransition;
        }
        set
        {
            isNeedToMakeTransition = value;
        }
    }


    public bool IsPausedProperty
    {
        get
        {
            return isPaused;
        }
        set
        {
            isPaused = value;
        }
    }


    public bool IsReadyToRunProperty
    {
        get
        {
            return isReadyToRun;
        }
        set
        {
            isReadyToRun = value;
        }
    }


    public bool IsNeedToSpawnBoosterProperty 
    {
        get
        {
            return isNeedToSpawnBooster;
        }
        set
        {
            isNeedToSpawnBooster = value;
        }
    }


    public int CurrentTonnelPositionProperty
    {
        get
        {
            return currentTonnelPosition;
        }
        set
        {
            currentTonnelPosition = value;
        }
    }


    public int PrevTonnelPositionProperty
    {
        get
        {
            return prevTonnelPosition;
        }

        set
        {
            prevTonnelPosition = value;
        }
    }


    public int DirectionGrowLengthProperty
    {
        get
        {
            return directionGrowLength;
        }

        set
        {
            directionGrowLength = value;
        }
    }


    public int GrowLengthProperty
    {
        get
        {
            return growLength;
        }

        set
        {
            growLength = value;
        }
    }


    public int CounterProperty
    {
        get
        {
            return counter;
        }

        set
        {
            counter = value;
        }
    }

    #endregion



    #region Unity lifecycle

    void Start()
    {
        isPaused = true;      
        ballSpawnPosition = new Vector3(SCREEN_WIDTH/2, SCREEN_HEIGHT/4, -5);
        TweenColor.SetColor(warning, endColor, 0);
    }


    //private void OnEnable()
    //{
    //    menu.OnClick += Button_Pause;
    //    keepGoing.OnClick += Button_Pause;
    //}



    //private void OnDisable()
    //{
    //    menu.OnClick -= Button_Pause;
    //    keepGoing.OnClick -= Button_Pause;
    //}



    private void Update()
    {
        IRunable();
    }
    
    #endregion



    #region Public methods

    public void buildIntroLvl(int i)
    {
        if (i == 0 || i == 1)
            tileList[i].GetComponent<Tile>().DestroyElement(0);

        if (i == 0 || i == 1 || i == 2 || i == 3)
            tileList[i].GetComponent<Tile>().DestroyElement(1);

        if (i == 0 || i == 1 || i == 2 || i == 3 || i == 4 || i == 5)
            tileList[i].GetComponent<Tile>().DestroyElement(2);

        if (i == 0 || i == 1 || i == 2 || i == 3 || i == 4 || i == 5 || i == 6 || i == 7 || i == 8)
            tileList[i].GetComponent<Tile>().DestroyElement(3);

        if (i == 0 || i == 1 || i == 2 || i == 3 || i == 4 || i == 5)
            tileList[i].GetComponent<Tile>().DestroyElement(4);

        if (i == 0 || i == 1 || i == 2 || i == 3)
            tileList[i].GetComponent<Tile>().DestroyElement(5);

        if (i == 0 || i == 1)
            tileList[i].GetComponent<Tile>().DestroyElement(6);
    }


    public void SpawnBall()
    {
       // PoolManager.Instance.pools[0].Pop().transform.position = ballSpawnPosition;
    }


    public void calculateLine()
    {
        growLength = UnityEngine.Random.Range(2, 4);
    }


    public void Initialize()
    {
        isNeedToMakeTransition = false;
        isNeedToSpawnBooster = false;
        prevTonnelPosition = currentTonnelPosition;
        counter = 0;
        boosterCounter = 0;
        currentTonnelPosition = ROW_MIDDLE_ELEMENT;
        tilesParent = new GameObject();
        tilesParent.SetActive(false);
        tilesParent.transform.position = ballSpawnPosition;
        tilesParent.transform.name = "TileParent";
        isReadyToRun = false;
        firstTileSpawnPos = new Vector3(SCREEN_WIDTH/2 + 70, 0, 0);
        //ISpawnTiles();
    }


    public void BuildLvl()
    {
        GameConditionsController.Instance.IsAlive = true;
        Initialize();
    }


    public void StartGame()
    {
        GameConditionsController.Instance.IsStartedProperty = true;
        TweenColor.SetColor(warning, endColor, 1);
        GameConditionsController.Instance.IsAlive = true;
        GameConditionsController.Instance.IsReadyTORestartProperty = false; 
    }


    public void RestartLvl()
    {
        TweenColor.SetColor(startText, startColor, 0);
        TweenColor.SetColor(warning, endColor, 1);
        Destroy(GameObject.Find("Ball"));
        Destroy(tilesParent);
        tileList.Clear();
        Initialize();
    }

    #endregion



    #region Private methods
    
    //void ISpawnTiles()
    //{
    //    Vector3 nextPos = firstTileSpawnPos;
    //    for (int i = 0; i < Y_TILE_HEIGHT; i++)
    //    {
    //        //tileList.Add(PoolManager.Instance.PoolForObject(tile).Pop());
    //        tileList[tileList.Count - 1].transform.position = nextPos;
    //        tileList[tileList.Count - 1].transform.parent = tilesParent.transform;
    //        nextPos.y += TITLE_HEIGHT;
    //        buildIntroLvl(i);
    //    }
    //    SpawnBall();
    //    tilesParent.SetActive(true);
    //    GameConditionsController.Instance.IsReadyTORestartProperty = true;
    //}


    void IRunable()
    {
        if (Input.GetKeyDown(KeyCode.F))
            isPaused = false;        
         
                if (!isPaused)
                {
                    Vector3 pos = tilesParent.transform.position;
                    pos.y -= ballSpeed*Time.deltaTime;
                    tilesParent.transform.position = pos;
             
                    if (tileList.Count != 0 && tileList[0].transform.position.y <= REVERT_POSITION_Y)
                    {                    
                        tileList[0].GetComponent<Tile>().DisableBoosters();
                        calculateLine();
                        boosterCounter++;
                    }                
               
                    if (boosterCounter == spawnBoosterFrequency)
                    {
                        tileList[0].GetComponent<Tile>().EnableBooster(currentTonnelPosition);
                        boosterCounter = 0;
                    }                                 
                 }
    }

    #endregion



    #region Event handlers

    public void Button_Pause()
    {
        GameConditionsController.Instance.IsReadyTORestartProperty = true;
        if (GameConditionsController.Instance.IsAlive)
        {
            isPaused = !isPaused;
            if (isPaused)
                PauseMenuActivator.Instance.ActivateColor();
            else PauseMenuActivator.Instance.DiactivateColor();
        }
    }

    #endregion

}
