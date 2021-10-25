using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class Screenshot : MonoBehaviour
{
    [SerializeField]
    private string filename = "image";

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            ScreenCapture.CaptureScreenshot(Application.dataPath + "/Sprites/" + filename + ".png");
            print("captured");
        }
    }
}
