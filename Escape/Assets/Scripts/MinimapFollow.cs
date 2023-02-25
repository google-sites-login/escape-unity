using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
    public Transform Target;
    void LateUpdate(){
        transform.position = new Vector3(Target.position.x, Target.position.y, transform.position.z);
    }
}
