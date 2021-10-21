using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knife.HDRPOutline.Core;
public class outlineControl : MonoBehaviour
{
    // Start is called before the first frame update
    OutlineObject Oj;
    Rigidbody rb;
    public bool isSelected = false;//是否被选中\
    int isGraspping = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Oj = GetComponent<OutlineObject>();
        GraspMain.grasped_object.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isSelected == true)
            Oj.enabled = true;
        else
            Oj.enabled = false;
        if(isSelected == true && LeapPinch.pinchDistance < GraspMain.FloorPublic)
            isGraspping = 1;
        else
            isGraspping = 0;
        if(isGraspping == 1){
            transform.position = LeapPinch.pinchPosition;
            transform.rotation = LeapPinch.realPamlQuaternion;
            rb.isKinematic = true;
        }
        else
            rb.isKinematic = false;

        // if(isGraspping == 0 && LeapPinch.pinchDistance < Grasp && Vector3.Distance(cube.transform.position, LeapPinch.pinchPosition) < minGrapDistance)
        // {
        //     isGraspping = 1;
        //     //rb.isKinematic = true;
        // }
        // else if(isGraspping == 1 && LeapPinch.pinchDistance > UpperThreshold)
        // {
        //     //rb.isKinematic = false;
        //     isGraspping = 0;
        // }


    //     if(LeapPinch.pinchDistance > 50)
    //     Oj.enabled = false;
    //     else
    //     Oj.enabled = true;
        
    }
}
