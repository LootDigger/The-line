using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    #region Variables

    private const float REVERT_POSITION_Y = -180f;
    private const int MIDDLE_CELL_POS = 3;


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
        updatedSplitSpawnPos = new Vector3(0, 1250, 0);
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
            DestroyElement(TileSpawner.instance.currentTonnelPositionProperty);
            if (TileSpawner.instance.counterProperty == TileSpawner.instance.growLengthProperty - 1)
            {
                TileSpawner.instance.isNeedToMakeTransitionProperty = true;
                TileSpawner.instance.counterProperty = 0;

                leftDirLength = TileSpawner.instance.currentTonnelPositionProperty - TileSpawner.instance.directionGrowLengthProperty;
                rightDirLength = TileSpawner.instance.currentTonnelPositionProperty + TileSpawner.instance.directionGrowLengthProperty;
                

                TileSpawner.instance.prevTonnelPositionProperty = TileSpawner.instance.currentTonnelPositionProperty;
                if (TileSpawner.instance.currentTonnelPositionProperty < MIDDLE_CELL_POS)
                {
                    TileSpawner.instance.currentTonnelPositionProperty = Random.Range(2, 4);
                }
                if (TileSpawner.instance.currentTonnelPositionProperty > MIDDLE_CELL_POS)
                {
                    TileSpawner.instance.currentTonnelPositionProperty = Random.Range(3, 5);
                }

                if (TileSpawner.instance.currentTonnelPositionProperty == MIDDLE_CELL_POS)
                {
                    TileSpawner.instance.currentTonnelPositionProperty = Random.Range(2, 5);
                }

            }
            else
            {
                if (TileSpawner.instance.counterProperty < TileSpawner.instance.growLengthProperty)
                    TileSpawner.instance.counterProperty++;
            }

            transform.position = updatedSplitSpawnPos;
        }

        if (TileSpawner.instance.isNeedToMakeTransitionProperty)
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

        if(TileSpawner.instance.currentTonnelPositionProperty > TileSpawner.instance.prevTonnelPositionProperty)
        {
            for (int i = TileSpawner.instance.prevTonnelPositionProperty; i <= TileSpawner.instance.currentTonnelPositionProperty; i++)
            {
                DestroyElement(i);
            }

        }

        else if(TileSpawner.instance.currentTonnelPositionProperty < TileSpawner.instance.prevTonnelPositionProperty)
        {

            for (int i = TileSpawner.instance.currentTonnelPositionProperty; i < TileSpawner.instance.prevTonnelPositionProperty; i++)
            {
                DestroyElement(i);
            }

        }

        TileSpawner.instance.isNeedToMakeTransitionProperty = false;

    }

    #endregion

}
