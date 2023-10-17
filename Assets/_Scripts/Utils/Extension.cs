using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension
{
    public static Vector2 GetMouseDirection(this Transform transform)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 transformPosition = transform.position;
        return mousePos - transformPosition;
    }

    public static void LookToMouse(this Transform transform, Direction forwardDirection)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 transformPosition = transform.position;
        Vector2 mouseDirection = mousePos - transformPosition;
        
        switch (forwardDirection)
        {
            case Direction.Up: 
                transform.up = mouseDirection;
                break;
            case Direction.Down:
                transform.up = -mouseDirection;
                break;
            case Direction.Left:
                transform.right = mouseDirection;
                break;
            case Direction.Right:
                transform.right = -mouseDirection;
                break;
        }
    }
}
