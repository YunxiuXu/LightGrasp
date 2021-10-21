using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knife.HDRPOutline.Core;
public class GraspMain : MonoBehaviour
{
    public float FloorThreshold, UpperThreshold, minGrapDistance;
    public static float FloorPublic, UpperPublic, minGPublic;
    public static List<GameObject> grasped_object = new List<GameObject>();//可抓取物品集
    private int isGraspping = 0;//抓取状态

    public float minGraspDistance = 0.1f;//抓取的最小距离
    Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //print(LeapPinch.pinchDistance);
        FloorPublic = FloorThreshold;
        UpperPublic = UpperThreshold;
        minGPublic = minGrapDistance;   
        ChangeOutlineByDistance();
    }

    void ChangeOutlineByDistance(){
        var nearestObject = GetNearestGameObject(grasped_object);
        for(int i = 0;i < grasped_object.Count;i++){
            //grasped_object[i].GetComponent<OutlineObject>().enabled = false;
            grasped_object[i].GetComponent<outlineControl>().isSelected = false;
        }
        if(Vector3.Distance(LeapPinch.pinchTransform, nearestObject.transform.position) < minGraspDistance){
            nearestObject.GetComponent<outlineControl>().isSelected = true;
            //nearestObject.GetComponent<OutlineObject>().enabled = true;
        }
    }
    GameObject GetNearestGameObject(List<GameObject> listTemp){//返回距离最近的物体
        if(listTemp!=null&&listTemp.Count>0){
            GameObject targetTemp = listTemp.Count>0? listTemp[0]:null; 
            float dis = Vector3.Distance(LeapPinch.pinchPosition,listTemp[0].transform.position);
            float disTemp;
            for(int i=1;i<listTemp.Count;i++){
                disTemp = Vector3.Distance(LeapPinch.pinchPosition,listTemp[i].transform.position);
                if(disTemp<dis){
                    targetTemp = listTemp[i];
                    dis = disTemp;
                }
            }
            return targetTemp;
        }else{
            return null;
        }
    }
}
