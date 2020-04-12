using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformRotate : MonoBehaviour
{
    public int mouseButton = 1;
    public float degreesPerScreenX = 180;
    public float degreesPerScreenY = 90;
    public Vector2 rotateAxisHorizontalMinMax = new Vector2(-45, 45);
    public Vector2 rotateAxisVerticalMinMax = new Vector2(-15, 15);

    Vector3 lastPoint;

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        if (Input.GetMouseButtonDown(mouseButton))
        {
            lastPoint = mousePos;
        }

        if (Input.GetMouseButton(mouseButton))
        {
            var delta = mousePos - lastPoint;

            var horizontalMouseMoveDegrees = delta.x / Screen.width * degreesPerScreenX;
            var verticalMouseMoveDegrees = -(delta.y / Screen.height * degreesPerScreenY);

            transform.localEulerAngles += new Vector3(verticalMouseMoveDegrees, horizontalMouseMoveDegrees, 0);
            var localEulerAngles = transform.localEulerAngles;
            if (localEulerAngles.x > 180) localEulerAngles.x -= 360;
            if (localEulerAngles.y > 180) localEulerAngles.y -= 360;
            localEulerAngles.x = Mathf.Clamp(localEulerAngles.x, rotateAxisVerticalMinMax.x, rotateAxisVerticalMinMax.y);
            localEulerAngles.y = Mathf.Clamp(localEulerAngles.y, rotateAxisHorizontalMinMax.x, rotateAxisHorizontalMinMax.y);

            transform.localEulerAngles = localEulerAngles;

            lastPoint = mousePos;
        }
    }
}
