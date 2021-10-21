using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rod : MonoBehaviour
{
    // Start is called before the first frame update
    public bool is5Dof = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = LeapPinch.pinchPosition;
        if(is5Dof == true){
            var tmp = LeapPinch.pinchQuaterion;
            tmp.x = 0;
            this.transform.rotation = tmp;
        }
        else
            this.transform.rotation = LeapPinch.pinchQuaterion;
    }
}
