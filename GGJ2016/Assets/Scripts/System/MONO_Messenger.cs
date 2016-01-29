using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class MONO_Messenger : MonoBehaviour
{
    [HideInInspector]
    public string name = "Notify";

    public GameObject target;
    public string methodName;
    public float delay = 0;
    public bool forceActive;


    public void Activate()
    {
        Send();
    }
    public void RequestToSend()
    {
        Utils.Instance.MessengerSend(new MONO_Messenger[1] { this });
    }

    public void Send()
    {
        if (target != null)
        {
            if (!target.activeSelf)
            {
                if (forceActive)
                    target.SetActive(true);
            }
            
            target.SendMessage(methodName/*, SendMessageOptions.DontRequireReceiver*/);
        }
    }


}
