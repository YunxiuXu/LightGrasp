using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class LeapPinch : MonoBehaviour
{
    LeapProvider provider;
    // Start is called before the first frame update
    void Start()
    {
        provider = FindObjectOfType<LeapProvider>() as LeapProvider;
    }

    public static Vector3 pinchTransform;
    public static Quaternion palmRoration, realPamlQuaternion;

    public static Vector3 realPalmRotation;
    public static float pinchDistance = 0;
    public GameObject rightThumbPinch, rightIndexPinch, rightThumbEndPinch, rightIndexEndPinch;
    public static Vector3 pinchVector, PalmVector;//后面这个是食指和拇指末端的vector
    public static Quaternion pinchQuaterion, PalmQuaterion;
    public static Vector3 palmNormal;
    public static Vector3 pinchPosition;

    public static int hasRightHand = 0;
    // Update is called once per frame
    void Update()
    {
		Frame frame = provider.CurrentFrame;
        if(frame.Hands.Count > 0){
            foreach (Hand hand in frame.Hands){
                if(hand.IsRight){
                    hasRightHand = 1;
                }
            }
		
        }
        else if(frame.Hands.Count == 0){
             hasRightHand = 0;
        }

		foreach (Hand hand in frame.Hands)
		{
			if (hand.IsRight)
			{


                //				transform.position = hand.PalmPosition.ToVector3() +
                //
                //					hand.PalmNormal.ToVector3() *
                //
                //					(transform.localScale.y * .5f + .02f);

                //transform.rotation = hand.Basis.CalculateRotation();
                //transform.position = new Vector3 (0, (hand.PalmPosition.y - 6) * 10, 0);
                //transform.localScale = new Vector3 ((hand.PalmPosition.y - 62.9f) * 2.5f, (hand.PalmPosition.y - 62.9f)* 2.5f, (hand.PalmPosition.y - 62.9f)* 2.5f);
                pinchDistance = hand.PinchDistance;//捏是0 松开100 建议30以下算捏
                pinchTransform = hand.GetPinchPosition();
                pinchPosition = pinchTransform;//重写一个更好听的名字
                //print(pinchTransform);

                pinchVector = (rightIndexPinch.transform.position - rightThumbPinch.transform.position).normalized;
                PalmVector = (rightThumbEndPinch.transform.position - rightIndexEndPinch.transform.position).normalized;
                //print("pinchDistance:" + pinchDistance);
                
                //palmRoration = hand.GetPalmPose().rotation;
                palmRoration = rightIndexPinch.transform.rotation;
                realPamlQuaternion = hand.GetPalmPose().rotation;
                pinchQuaterion = Quaternion.Euler(LookRotation(pinchVector));
                PalmQuaterion = Quaternion.Euler(LookRotation(PalmVector));

                realPalmRotation = hand.GetPalmPose().rotation.eulerAngles;
                
                palmNormal = hand.PalmNormal.ToVector3();
                //print("pn" + palmNormal);
                
                //print(LookRotation(pinchVector));
                //PinchRotation = rightIndexPinch.transform.rotation - rightThumbPinch.transform.rotation;
            }

		}
	}

     
    public Vector3 LookRotation(Vector3 fromDir)
    {
        Vector3 eulerAngles = new Vector3();
 
        //AngleX = arc cos(sqrt((x^2 + z^2)/(x^2+y^2+z^2)))
        eulerAngles.x = Mathf.Acos(Mathf.Sqrt((fromDir.x * fromDir.x + fromDir.z * fromDir.z) / (fromDir.x * fromDir.x + fromDir.y * fromDir.y + fromDir.z * fromDir.z))) * Mathf.Rad2Deg;
        if (fromDir.y > 0) eulerAngles.x = 360 - eulerAngles.x;
 
        //AngleY = arc tan(x/z)
        eulerAngles.y = Mathf.Atan2(fromDir.x, fromDir.z) * Mathf.Rad2Deg;
        if (eulerAngles.y < 0) eulerAngles.y += 180;
        if (fromDir.x < 0) eulerAngles.y += 180;
        //AngleZ = 0
        eulerAngles.z = 0; 
        return eulerAngles;
    }
}
