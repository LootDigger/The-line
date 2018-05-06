using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour {

    [SerializeField]
    private GameObject CallEventGameobject;
    private CallEventScript CallEvent;


    


    void OnEnable()
    {
        CallEvent = CallEventGameobject.GetComponent<CallEventScript>();
        CallEvent.GetPrivet += ShowMessage;
    }


    void OnDisable()
    {
        CallEvent.GetPrivet -= ShowMessage;
    }


    public void ShowMessage(string message)
    {
        Debug.Log(message);
    }

}
