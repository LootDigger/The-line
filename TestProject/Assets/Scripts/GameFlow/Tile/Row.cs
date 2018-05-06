using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Row : MonoBehaviour
{
    #region Variables
    
    public List<GameObject> cellsList;  

    Vector3 position;
    float newPosition;
    float speed;
    float countOfCellsX;
    float countOfCellsY;
    float yCellHeight;
    float screenHeight;
    bool isStarted = false;
    int currentLinePos;
    int prevLinePos = 3;

    #endregion



    #region Properties

    bool isNeedToMakeTransition{ get; set; }
    bool isNeedToChangePosition { get; set; }

    #endregion



    #region Unity lifecycle

    void Start()
    {       
        speed *= Time.fixedDeltaTime;
    }

    void Awake()
    {
        cellsList = new List<GameObject>();
        GetArrayInfo();
        isNeedToMakeTransition = false;
    }
    

    void OnEnable()
    {
        SpawnArray.Instance.SetLinePosition += GetLineInfo;
        SpawnArray.Instance.spawnBooster += SpawnBooster;
    }

    void OnDisable()
    {
        SpawnArray.Instance.SetLinePosition -= GetLineInfo;
        SpawnArray.Instance.spawnBooster += SpawnBooster;
    }



    void FixedUpdate ()
    {       
        if (Input.GetKeyDown(KeyCode.Space))
            isStarted = ! isStarted;

        if( isStarted)
        Run();
    }

    #endregion


    #region Public methods

    public void Run()
    {
        try
        {
            if (transform.position.y <= -yCellHeight / 2)
            {
                isNeedToChangePosition = true;             
                ActivateRow();
                DeactivateCell(currentLinePos);
                transform.position = new Vector3(transform.position.x, newPosition);
                SpawnArray.Instance.Counter++;
            }
            else
            {
             isNeedToChangePosition = false;
            }
           
            position = transform.position;
            position.y -= speed;
            transform.position = position;
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex);
        }
    }

    
    public void ActivateRow()
    {
        for(int i = 0; i<cellsList.Count; i++)
        {
            cellsList[i].SetActive(true);
            cellsList[i].GetComponent<Cell>().SpriteCondition(true);
        }
    }


    public void AddCell(GameObject cell)
    {
        cellsList.Add(cell);
    }


    public void MakeTransition( )
    {
        int min;
        int max;
        if (Mathf.Min(prevLinePos, currentLinePos) == prevLinePos)
        {
            min = prevLinePos;
            max = currentLinePos;
        }
        else
        {
            min = currentLinePos;
            max = prevLinePos;
        }


        for (int i = min; i < max + 1; i++)
        {
            DeactivateCell(i);
        }
    }


   
    public void GetArrayInfo()
    {
        countOfCellsX = GameConditionsController.Instance.CountOfCellsX;
        countOfCellsY = GameConditionsController.Instance.CountOfCellsY;
        yCellHeight = GameConditionsController.Instance.CellHeight;
        screenHeight = GameConditionsController.Instance.ScreenHeight;
        speed = GameConditionsController.Instance.Speed;
        newPosition = countOfCellsY;
        newPosition *= yCellHeight;
        newPosition += 1.5f * yCellHeight;
        newPosition -= speed;
        speed *= yCellHeight;
    }



    public void DeactivateCell(int position)
    {
        cellsList[position].GetComponent<Cell>().SpriteCondition(false);
    }
    
    public void DeactivateBooster()
    {
        //cellsList[currentLinePos].GetComponent<Cell>().BoosterCondition(false);
    } 

    public void DeactivateRow()
    {       
        for (int i=0; i<countOfCellsX; i++)
        {
            DeactivateCell(i);
        }
        isNeedToMakeTransition = false;       
    }


    public void GetLineInfo(int currentLinePos)
    {       

        if (this.currentLinePos != null)
        {
            prevLinePos = this.currentLinePos;
        }
        
        this.currentLinePos = currentLinePos;
        isNeedToMakeTransition = true;

        if (isNeedToChangePosition)
        {
            MakeTransition();
        }

    }


    public void SpawnBooster(int position)
    {
        if (isNeedToChangePosition)
        {
            cellsList[currentLinePos].GetComponent<Cell>().BoosterCondition(true);
        }
    }

    #endregion

}
