using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector3 PositionFromAngle(float radius, float angle)
    {
        Vector3 position = Vector3.zero;
        float radian = DegreeToRadian(angle);
        
        position.x = Mathf.Cos(radian) * radius;
        position.y = Mathf.Sin(radian) * radius;
        return position;
    }
    
    private static float DegreeToRadian(float angle)
    {
        return Mathf.PI * angle / 180f;
    }
}
