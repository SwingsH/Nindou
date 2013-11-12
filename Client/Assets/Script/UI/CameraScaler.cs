using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraScaler : MonoBehaviour
{

    private void Start()
    {
        Camera camera = GetComponent<Camera>();
        float size = 2.0f* (float)GUIStation.MANUAL_SCREEN_WIDTH / (float)GUIStation.MANUAL_SCREEN_HEIGHT * Screen.height / Screen.width / 2;
        camera.orthographicSize = size;
    }
}