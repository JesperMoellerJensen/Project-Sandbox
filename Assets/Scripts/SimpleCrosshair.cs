﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCrosshair : MonoBehaviour {

    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 30, 30), "");
    }
}