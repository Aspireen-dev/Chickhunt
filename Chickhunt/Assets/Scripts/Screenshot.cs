using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    [SerializeField]
    private string filename = "";

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (filename == "" || filename == null) filename = "image";
        print("Press 'A' key to take a screenshot");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            string path = Application.dataPath + "/Sprites/" + filename + ".png";
            ScreenCapture.CaptureScreenshot(path);
            print("Screenshot captured : " + path);
        }
    }
}
