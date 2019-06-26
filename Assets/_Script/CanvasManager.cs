using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public Button add;
    public Button del;
    public Button save;
    public Button init;
    public Button clear;
    public Transform sphere;
    Vector3 rotation_value;
    public SaveData sd;

    string path;
    bool display;
    float display_time;

    // Start is called before the first frame update
    void Start()
    {
        rotation_value = Vector3.zero;
        display = false;
        display_time = 0f;

        add.onClick.AddListener(() =>
        {
            GameInfo.FunctionMode = EFunction.Add;
            print(string.Format("FunctionMode: {0}", GameInfo.FunctionMode));            
        });


        del.onClick.AddListener(() =>
        {
            GameInfo.FunctionMode = EFunction.Del;
            print(string.Format("FunctionMode: {0}", GameInfo.FunctionMode));            
        });


        save.onClick.AddListener(() =>
        {
            display = true;

            switch (GameInfo.Version)
            {
                case EVersion.Local:
                    string file_name = DateTime.Now.ToString("yyyy-MM-dd@H-mm-ss-ffff");
                    path = Path.Combine(GameInfo.DataPath, string.Format("{0}.png", file_name));
                    ScreenCapture.CaptureScreenshot(path);
                    print(string.Format("Save file: {0}", path));
                    break;
                case EVersion.WebGL:
                    StartCoroutine(sd.uploadScreenShot());
                    break;
                case EVersion.Test:
                    StartCoroutine(sd.uploadScreenShot());
                    break;
            }
        });

        init.onClick.AddListener(() =>
        {
            print("Initialize rotation angle.");
            sphere.rotation = Quaternion.identity;

        });

        clear.onClick.AddListener(()=> {
            int i, child_number = sphere.childCount;

            for(i = 0; i < child_number; i++)
            {
                Destroy(sphere.GetChild(i).gameObject);
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (display)
        {
            display_time += Time.deltaTime;

            if(display_time > 2.0f)
            {
                display = false;
                display_time = 0f;
            }
        }
    }

    private void OnGUI()
    {
        if (display)
        {
            // 文字位置
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;

            string label = "圖片已儲存";
            float font_size = Mathf.Sqrt(Mathf.Sqrt(Screen.width * Screen.height));
            float gui_x = Screen.width * 0.4f;
            float gui_y = Screen.height * 0.2f;
            float gui_width = font_size * label.Length;
            float gui_height = font_size;

            GUI.contentColor = Color.red;
            // 文字大小
            GUI.skin.label.fontSize = (int)font_size;
            // 顯示位置與文字內容
            GUI.Label(new Rect(gui_x, gui_y, gui_width, gui_height), label);
        }
    }

    public void onSliderXValueChanged(float value)
    {
        float x = value - rotation_value.x;
        sphere.Rotate(new Vector3(x, 0f, 0f));
        rotation_value.x = value;
    }

    public void onSliderYValueChanged(float value)
    {
        float y = value - rotation_value.y;
        sphere.Rotate(new Vector3(0f, y, 0f));
        rotation_value.y = value;
    }

    public void onSliderZValueChanged(float value)
    {
        float z = value - rotation_value.z;
        sphere.Rotate(new Vector3(0f, 0f, z));
        rotation_value.z = value;
    }

}
