using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatter : MonoBehaviour{
    void OnTriggerEnter2D(Collider2D other){
        Debug.Log("Other" + other);
        if(other.GetComponent<PlayerMovement>() != null){
            other.GetComponent<PlayerMovement>().currentBlood++;
            Debug.Log(other.GetComponent<PlayerMovement>().currentBlood);
            Destroy(gameObject);
        }
    }
}
