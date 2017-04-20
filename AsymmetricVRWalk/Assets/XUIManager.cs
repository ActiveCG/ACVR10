using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class XUIManager : MonoBehaviour {

    string fileName = "MyFile.txt";

    public void OnButtonPress()
    {
        if (File.Exists(fileName))
        {
            Debug.Log(fileName + " already exists.");
            return;
        }
        var sr = File.CreateText(fileName);
        sr.WriteLine("This is my file.");
        sr.WriteLine("I can write ints {0} or floats {1}, and so on.",
            1, 4.2);
        sr.Close();
    }
}
