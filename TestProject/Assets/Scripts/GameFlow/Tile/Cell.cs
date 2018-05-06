using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {

    Vector3 position;

    [SerializeField]
    GameObject Booster;
    [SerializeField]
    GameObject Sprite;



    public void BoosterCondition(bool isActive)
    {
        Booster.SetActive(isActive);
    }



    public void SpriteCondition(bool isActive)
    {        
        Sprite.SetActive(isActive);
    }



	
}
