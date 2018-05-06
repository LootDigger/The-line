using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallEventScript : MonoBehaviour  {

    public delegate void CallHandler(string message);

    private event CallHandler poshelNahuy;
    private event CallHandler privetAndrej;
	

    
    public CallHandler GetPrivet
    {
        get
        {
            return privetAndrej;
        }
        set
        {
            privetAndrej = value;
        }

    }


   


    public void Poslat()
    {

        if (poshelNahuy != null)
        {
            poshelNahuy("Иди нахуй!!!!11!100))");

        }

    }


    public void Poprivetstvovat()
    {

        if (privetAndrej != null)
        {
            privetAndrej("Privet Andrej tu tu- tu- tu tuuu");
        }
    }

    public void Update()
    {

        if(Input.GetKeyDown(KeyCode.F))
        {
            Poprivetstvovat();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Poslat();
        }

    }



}
