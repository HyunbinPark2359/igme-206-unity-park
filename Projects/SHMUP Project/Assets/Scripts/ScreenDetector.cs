using UnityEngine;

public class ScreenDetector : MonoBehaviour
{
    private static float screenLeft;
    private static float screenTop;
    private static float screenRight;
    private static float screenBottom;

    public static float ScreenLeft { get { return screenLeft; } }
    public static float ScreenTop { get { return screenTop; } }
    public static float ScreenRight { get { return screenRight; } }
    public static float ScreenBottom { get { return screenBottom; } }

    private void Awake()
    {
        Vector3 screenBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 screenTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.nearClipPlane));

        screenLeft = screenBottomLeft.x;
        screenRight = screenTopRight.x;
        screenBottom = screenBottomLeft.y;
        screenTop = screenTopRight.y;

        //Debug.Log("Borders are: " + screenLeft + ", " + screenRight + ", " + screenBottom + ", " + screenTop);
    }
}
