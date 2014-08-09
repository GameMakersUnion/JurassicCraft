using UnityEngine;
using System.Collections;

public class Utils : MonoBehaviour {

    //@percentage = 0.001 to 1.000
    public static float PercentToPixel(float percentage, float relativeTo)
    {
        //const float minScreenHeight = 180f;
        const float minPercentage = 0.01f;
        const float maxPercentage = 1f;

        if (percentage < minPercentage)
        {
            Debug.Log("Percentage input <color=maroon>too small</color>, increased from " + percentage + " to " + minPercentage);
            percentage = minPercentage;
        }
        else if (percentage > maxPercentage)
        {
            Debug.Log("Percentage input <color=maroon>too large</color>, <color=orange>decreased from " + percentage + " to " + maxPercentage);
            percentage = maxPercentage;
        }
        float pixels = percentage * relativeTo;
        return pixels;
    }

    //C# Modulus Is WRONG!
    //http://answers.unity3d.com/questions/380035/c-modulus-is-wrong-1.html
    public static float nfmod(float a, float b)
    {
        return a - b * Mathf.Floor(a / b);
    }

    //Unity doesn't implement vector multiplication
    //http://answers.unity3d.com/questions/16824/what-happens-when-you-multiply-two-vectors.html - confirmation
    //http://docs.unity3d.com/ScriptReference/Vector3-operator_multiply.html - signature, commented out
    //public static Vector3 operator *(Vector3 a, Vector3 b)
    public static Vector3 vMult(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }
}
