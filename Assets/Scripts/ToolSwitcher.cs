using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Tools;
using UnityEngine;

public class ToolSwitcher : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            GravityGun();

        if (Input.GetKeyDown(KeyCode.Alpha2))
            WeldTool();

        if (Input.GetKeyDown(KeyCode.Alpha3))
            Thruster();
    }

    void GravityGun()
    {
        GetComponent<Tool_GravityGun>().enabled = true;
        GetComponent<Tool_Thruster>().enabled = false;
        GetComponent<Tool_Weld>().enabled = false;
    }

    void WeldTool()
    {
        GetComponent<Tool_Weld>().enabled = true;
        GetComponent<Tool_Thruster>().enabled = false;
        GetComponent<Tool_GravityGun>().enabled = false;
    }

    void Thruster()
    {
        GetComponent<Tool_Thruster>().enabled = true;
        GetComponent<Tool_Weld>().enabled = false;
        GetComponent<Tool_GravityGun>().enabled = false;
    }
}
