using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Transform doorPos;
    // bool isTriggered;
    // Start is called before the first frame update

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Debug.Log("ENEMY HIT TRIGGER!");
            doorPos.position += new Vector3(4, 0, 0);
            // isTriggered = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            doorPos.position -= new Vector3(4, 0, 0);
            // isTriggered = false;
        }
    }
}
