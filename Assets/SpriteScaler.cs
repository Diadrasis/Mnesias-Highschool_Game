//scaler script was used by https://medium.com/@kunaltandon.kt/scaling-sprites-based-on-screen-resolutions-f28c47744ab5
using UnityEngine;

public class SpriteScaler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float width = ScreenSize.GetScreenToWorldWidth;
        transform.localScale = Vector3.one * width;
    }

    
}

public class ScreenSize
{
    public static float GetScreenToWorldHeight
    {
        get
        {
            Vector2 topRightCorner = new Vector2(1, 1);
            Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner); var height = edgeVector.y * 2;
            return height;
        }
    }
    public static float GetScreenToWorldWidth
    {
        get
        {
            Vector2 topRightCorner = new Vector2(1, 1);
            Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner); var width = edgeVector.x * 2;
            return width;
        }
    }
}
