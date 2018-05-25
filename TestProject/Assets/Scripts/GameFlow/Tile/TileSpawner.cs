using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TileSpawner : MonoBehaviour
{
    #region variables

    private const float TITLE_WIDTH = 360f;
    private const float TITLE_HEIGHT = 640f;
    private const int Y_TILE_HEIGHT = 9;
    private const float SPEED = 8f;
    private const float REVERT_POSITION_Y = -180f;
    private const int SPAWN_BOOSTER_TIME = 3;
    private const int SPAWN_BLUE_LINE_TIME = 7;
    private const int CELL_SCALE_FAKTOR = 4;
    private const int ROW_MIDDLE_ELEMENT = 3;


    internal static TileSpawner instance = null;


    [SerializeField] GameObject tile;
    [SerializeField] GameObject ball;
    [SerializeField] GameObject warning;
    [SerializeField] GameObject startText;
    [SerializeField] tk2dUIItem menu;
    [SerializeField] tk2dUIItem keepGoing;
       
    private List<GameObject> tileList;
    private GameObject tilesParent;
    private Vector3 firstTileSpawnPos;     
    private Vector3 spawnPosition;
    private Color startColor;
    private Color zeroAlpha;
    private bool isNeedToMakeTransition;
    private bool isPaused;
    private bool isReadyToRun;
    private bool isNeedToSpawnBooster;
    private int blueLineCounter;
    private int currentTonnelPosition;
    private int prevTonnelPosition;
    private int directionGrowLength;
    private int growLength;
    private int counter;
    private int boosterCounter;

    #endregion



    #region Properties

    public bool isNeedToMakeTransitionProperty
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

    public bool isPausedProperty
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

    public bool isReadyToRunProperty
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

    public bool isNeedToSpawnBoosterProperty {
        get
        {
            return isNeedToSpawnBooster;
        }
        set
        {
            isNeedToSpawnBooster = value;
        }

    }

    public int currentTonnelPositionProperty
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

    public int prevTonnelPositionProperty
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

    public int directionGrowLengthProperty
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

    public int growLengthProperty
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

    public int counterProperty
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
        isPaused = false;
        if (instance == null)
            instance = this;
        else if (instance == this)
            Destroy(gameObject);

        zeroAlpha = new Color(1, 1, 1, 0);
        startColor = new Color(1, 1, 1, 1);
        PoolManager.Instance.PoolForObject(ball);
        spawnPosition = new Vector3(-44, 300, -5);
        TweenColor.SetColor(warning, zeroAlpha, 0);

    }


    private void OnEnable()
    {
        menu.OnClick += Button_Pause;
        keepGoing.OnClick += Button_Pause;
    }



    private void OnDisable()
    {
        menu.OnClick -= Button_Pause;
        keepGoing.OnClick -= Button_Pause;
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
        PoolManager.Instance.pools[0].Pop().transform.position = spawnPosition;
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
        blueLineCounter = 0;
        counter = 0;
        boosterCounter = 0;
        currentTonnelPosition = ROW_MIDDLE_ELEMENT;
        tilesParent = new GameObject();
        tilesParent.SetActive(false);
        tilesParent.transform.name = "TileParent";
        isReadyToRun = false;
        firstTileSpawnPos = new Vector3(0, 0, 0);
        tileList = new List<GameObject>();
        StartCoroutine(ISpawnTiles());
        isPaused = false;

    }


    public void BuildLvl()
    {
        GameConditionsController.instance.IsAlive = true;
        Initialize();
    }


    public void StartGame()
    {
        StartCoroutine(IRunable());
        GameConditionsController.instance.IsStartedProperty = true;
        TweenColor.SetColor(warning, zeroAlpha, 1);
        GameConditionsController.instance.IsAlive = true;
        GameConditionsController.instance.IsReadyTORestartProperty = false; 
    }


    public void RestartLvl()
    {
        TweenColor.SetColor(startText, startColor, 0);
        TweenColor.SetColor(warning, zeroAlpha, 1);
        Destroy(GameObject.Find("Ball"));
        Destroy(tilesParent);
        tileList.Clear();
        Initialize();
    }

    #endregion



    #region Private methods
    
    IEnumerator ISpawnTiles()
    {
        Vector3 nextPos = firstTileSpawnPos;
        for (int i = 0; i < Y_TILE_HEIGHT; i++)
        {
            yield return null;
            tileList.Add(PoolManager.Instance.PoolForObject(tile).Pop());
            tileList[tileList.Count - 1].transform.position = nextPos;
            tileList[tileList.Count - 1].transform.parent = tilesParent.transform;
            nextPos.y += TITLE_HEIGHT / CELL_SCALE_FAKTOR;
            buildIntroLvl(i);
        }
        SpawnBall();
        tilesParent.SetActive(true);
        GameConditionsController.instance.IsReadyTORestartProperty = true;
    }


    IEnumerator IRunable()
    {
        while (true)
        {
            yield return null;

           
                if (!isPaused)
                {
                    Vector3 pos = tilesParent.transform.position;
                    pos.y -= SPEED;
                    tilesParent.transform.position = pos;

             
                    if (tileList.Count != 0 && tileList[0].transform.position.y <= REVERT_POSITION_Y)
                    {
                    
                        tileList[0].GetComponent<Tile>().DisableBoosters();
                        calculateLine();
                        blueLineCounter++;
                        boosterCounter++;
                    }
                
               
                    if (boosterCounter == SPAWN_BOOSTER_TIME)
                    {
                        tileList[0].GetComponent<Tile>().EnableBooster(currentTonnelPosition);

                        boosterCounter = 0;
                    }

                    if (blueLineCounter == SPAWN_BLUE_LINE_TIME && !isNeedToMakeTransition)
                    {
                        tileList[0].GetComponent<Tile>().ChangeColorTo(Color.blue);
                        tileList[0].GetComponent<Tile>().SetActivityTo(true);
                        GameObject.Find("Ball").GetComponent<Ball>().isReadyToJumpProperty = true;
                        TweenColor.SetColor(warning, startColor, 1);
                    }
               

                    if (blueLineCounter > SPAWN_BLUE_LINE_TIME)
                    {
                        blueLineCounter = 0;
                        TweenColor.SetColor(warning, zeroAlpha, 1);

                    }
                }
        }
    }

    #endregion



    #region Event handlers

    public void Button_Pause()
    {
        GameConditionsController.instance.IsReadyTORestartProperty = true;
        if (GameConditionsController.instance.IsAlive)
        {
            isPaused = !isPaused;

            if (isPaused)
                PauseMenuActivator.Instance.ActivateColor();
            else PauseMenuActivator.Instance.DiactivateColor();
        }
    }

    #endregion

}
