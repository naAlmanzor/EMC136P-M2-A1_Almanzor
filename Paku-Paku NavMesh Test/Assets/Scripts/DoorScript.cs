using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Transform doorPos;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Debug.Log("ENEMY HIT TRIGGER!");
            doorPos.position += new Vector3(4, 0, 0);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            doorPos.position -= new Vector3(4, 0, 0);
        }
    }
}
