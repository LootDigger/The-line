using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArray : SingletonMonoBehaviour<SpawnArray>
{
    #region Variables

    public Dictionary<Row, GameObject> rowsDictionary;

    public delegate void UpdateCellInfo(float cellWidth, float cellHeight);
    public delegate void SpawnBooster(int position);
    public delegate void GetLinePosition(int currentLinePos);
    public delegate void SetGameStarted(bool isStarted);
    public delegate void SetPlayerAlive(bool isAlive);

    public event SpawnBooster spawnBooster;
    public event UpdateCellInfo UpdateInfo;
    public event GetLinePosition SetLinePosition;
    public event SetGameStarted setStarted;
    public event SetPlayerAlive setAlive;


    Vector3 cellScale = Vector3.one; 

    float screenHeight;
    float screenWidth;  
    float xCellsCount;
    float yCellsCount;
    float cellHeight;
    float cellWidth;
    int currentLinePosition;
    int lineLength;
    [SerializeField]
    int minLength = 2, maxLength = 5;
    public GameObject cell;
    float time;
    float counter;
    float boosterCounter = 0; 
    float boosterSpaenFrequency;

    #endregion



    #region Properties

    bool isTimerActive{get; set;}

    public float Counter
    {
        get
        {
            return counter;
        }
        set
        {            
            counter = value;
            boosterCounter++;
          
        }
    }
    

    #endregion


    #region Unity lifecycle

    void Start ()
    {
        isTimerActive = false;
        rowsDictionary = new Dictionary<Row, GameObject>();
        Spawn();
        CalculateNewLinePosition();
    }
	
	
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
            {
                CalculateNewLinePosition(); 
            }


        if(Input.GetKeyDown(KeyCode.Space))
        {
            setStarted(true);
            setAlive(true);
            Debug.Log("Space");
        }


            if(counter == lineLength)
        {
            counter = 0;
            CalculateNewLinePosition();
            
        }


        if(boosterCounter >= boosterSpaenFrequency)
        {
             spawnBooster(currentLinePosition);            
             boosterCounter = 0;
        }

    }

    #endregion



    #region Public methods

    public void CalculateNewLinePosition()
    {        
        currentLinePosition = (int) Random.Range(1, xCellsCount - 1f);       
        lineLength = Random.Range(minLength, maxLength);      
        SetLinePosition(currentLinePosition);
    }


    public void ChangeLinePosition()
    {
    }     


    void Spawn()
    {
        GameObject rowParent;
        GameObject cellTemp;
        Row row;
        GetArrayInfo();
        CalculateScale();

        for (float y = cellHeight / 2; y <= screenHeight + 2 * cellHeight; y += (cellHeight))
        {
            rowParent = new GameObject();
            rowParent.AddComponent<Row>();
            rowParent.transform.position = new Vector3(0, y, 0);
            row = rowParent.GetComponent<Row>();         
            rowsDictionary.Add(row, rowParent);
         
            for (float x =  (cellWidth + 2f) / 2; x < xCellsCount * cellWidth; x += cellWidth)
            {
                cellTemp = Instantiate(cell, new Vector3(x, y, 0f), Quaternion.identity);   
                cellTemp.transform.parent = rowParent.transform;
                cellTemp.transform.localScale = cellScale;
                if (row != null)
                {
                    row.AddCell(cellTemp);
                }
                else
                {
                    Debug.Log("Null");
                }
            }
        }
    }


    public void CalculateScale()
    {
        float temp = screenHeight;
        temp /= ( yCellsCount );
        temp /= cellHeight;
        cellScale.y = temp ;
      

        cellHeight *= cellScale.y;
        GameConditionsController.Instance.CellHeight = cellHeight;

        temp = screenWidth;
        temp /= xCellsCount;
        temp /= cellWidth;
        cellScale.x = temp;
        cellWidth *= cellScale.x;
        GameConditionsController.Instance.CellWidth = cellWidth;
        UpdateInfo(cellWidth, cellHeight);
    }


    public void GetArrayInfo()
    {
        screenHeight =  GameConditionsController.Instance.ScreenHeight;
        screenWidth =  GameConditionsController.Instance.ScreenWidth;
        xCellsCount = GameConditionsController.Instance.CountOfCellsX;
        yCellsCount = GameConditionsController.Instance.CountOfCellsY;
        cellHeight = GameConditionsController.Instance.CellHeight;
        cellWidth = GameConditionsController.Instance.CellWidth;
        boosterSpaenFrequency = GameConditionsController.Instance.SpawnFrequency;
    }


    #endregion
}