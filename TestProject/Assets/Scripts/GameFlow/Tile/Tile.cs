using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    #region Variables

    private const float REVERT_POSITION_Y = -180f;
    private const int MIDDLE_CELL_POS = 3;
    private const float SCREEN_HEIGHT = 1136f;
    private const float SCREEN_WIDTH = 640f;

    [SerializeField] GameObject[] sprites;
    [SerializeField] GameObject[] boosters;


    private Vector3 updatedSplitSpawnPos;
    private int leftGrowLength;
    private int rightGrowLength;
    private int leftDirLength;
    private int rightDirLength;
    private float time;

    #endregion
    


    #region Unity lifecycle

    void Start()
    {
        updatedSplitSpawnPos = new Vector3(SCREEN_WIDTH/2 + 70, 1250 , 0);
        time = 0;
    }


    void Update()
    {
        CheckCoords();       
    }

    #endregion



    #region Public methods

    public void CheckCoords()
    {
        if (transform.position.y <= REVERT_POSITION_Y)
        {
            SetActivityTo(true);
            ChangeColorTo(Color.white);
            DestroyElement(TileSpawner.Instance.CurrentTonnelPositionProperty);
            if (TileSpawner.Instance.CounterProperty == TileSpawner.Instance.GrowLengthProperty - 1)
            {
                TileSpawner.Instance.IsNeedToMakeTransitionProperty = true;
                TileSpawner.Instance.CounterProperty = 0;

                leftDirLength = TileSpawner.Instance.CurrentTonnelPositionProperty - TileSpawner.Instance.DirectionGrowLengthProperty;
                rightDirLength = TileSpawner.Instance.CurrentTonnelPositionProperty + TileSpawner.Instance.DirectionGrowLengthProperty;
                

                TileSpawner.Instance.PrevTonnelPositionProperty = TileSpawner.Instance.CurrentTonnelPositionProperty;
                if (TileSpawner.Instance.CurrentTonnelPositionProperty < MIDDLE_CELL_POS)
                {
                    TileSpawner.Instance.CurrentTonnelPositionProperty = Random.Range(2, 4);
                }
                if (TileSpawner.Instance.CurrentTonnelPositionProperty > MIDDLE_CELL_POS)
                {
                    TileSpawner.Instance.CurrentTonnelPositionProperty = Random.Range(3, 5);
                }

                if (TileSpawner.Instance.CurrentTonnelPositionProperty == MIDDLE_CELL_POS)
                {
                    TileSpawner.Instance.CurrentTonnelPositionProperty = Random.Range(2, 5);
                }

            }
            else
            {
                if (TileSpawner.Instance.CounterProperty < TileSpawner.Instance.GrowLengthProperty)
                    TileSpawner.Instance.CounterProperty++;
            }

            transform.position = updatedSplitSpawnPos;
        }

        if (TileSpawner.Instance.IsNeedToMakeTransitionProperty)
        {
            MakeTransition();
        }

    }
   

    public void DestroyElement(int number)
    {
        sprites[number].SetActive(false);
    }
    

    public void SetActivityTo(bool boolean)
    {
        sprites[0].SetActive(boolean);
        sprites[1].SetActive(boolean);
        sprites[2].SetActive(boolean);
        sprites[3].SetActive(boolean);
        sprites[4].SetActive(boolean);
        sprites[5].SetActive(boolean);
        sprites[6].SetActive(boolean);
    }


    public void DisableBoosters()
    {
        boosters[0].SetActive(false);
        boosters[1].SetActive(false);
        boosters[2].SetActive(false);
        boosters[3].SetActive(false);
        boosters[4].SetActive(false);
        boosters[5].SetActive(false);
        boosters[6].SetActive(false);
    }


    public void EnableBooster(int i)
    {
        boosters[i].SetActive(true);


    }

    public void ChangeColorTo(Color color)
    {        
        TweenColor.SetColor(sprites[0], color, 0);
        TweenColor.SetColor(sprites[1], color, 0);
        TweenColor.SetColor(sprites[2], color, 0);
        TweenColor.SetColor(sprites[3], color, 0);
        TweenColor.SetColor(sprites[4], color, 0);
        TweenColor.SetColor(sprites[5], color, 0);
        TweenColor.SetColor(sprites[6], color, 0);
    }

    
    public void MakeTransition()
    {
        if(TileSpawner.Instance.CurrentTonnelPositionProperty > TileSpawner.Instance.PrevTonnelPositionProperty)
        {
            for (int i = TileSpawner.Instance.PrevTonnelPositionProperty; i <= TileSpawner.Instance.CurrentTonnelPositionProperty; i++)
            {
                DestroyElement(i);
            }

        }

        else if(TileSpawner.Instance.CurrentTonnelPositionProperty < TileSpawner.Instance.PrevTonnelPositionProperty)
        {

            for (int i = TileSpawner.Instance.CurrentTonnelPositionProperty; i < TileSpawner.Instance.PrevTonnelPositionProperty; i++)
            {
                DestroyElement(i);
            }

        }

        TileSpawner.Instance.IsNeedToMakeTransitionProperty = false;

    }

    #endregion

}
