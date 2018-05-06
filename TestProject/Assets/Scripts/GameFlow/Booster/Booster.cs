using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Booster : MonoBehaviour 
{
    #region Variables

    private const float FLICKER_RATE = 0.2f; 


    float time;
    Color endColor;
    Color startColor;
    bool isImmortableBooster;


    #endregion



    #region Properties

    public bool IsImmortableBoosterProperty
    {
        get
        {
            return isImmortableBooster;
        }

        set
        {
            isImmortableBooster = value;
        }
    }

    #endregion



    #region Unity lifecycle

    void Start()
    {
        endColor = Color.red;
        startColor = Color.blue;
        Flick();
    }  


    void OnEnable()
    {
        
        int i = Random.Range(0, 2);
        if (i == 0)
        {
            isImmortableBooster = false;
        }
        else if (i == 1)
        {
            isImmortableBooster = true;            
        }
    }
    
    #endregion



    #region Public methods

    public void Flick()
    {
        if(isImmortableBooster)
        TweenColor.SetColor(this.gameObject, startColor, 0.1f);
        else TweenColor.SetColor(this.gameObject, endColor, 0.1f);

    }
    
    #endregion

}
