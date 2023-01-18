using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;


public class MessageBox : MonoBehaviour {

    private static MessageBox instance;

    public GameObject Template;

	// Use this for initialization
	void Awake () {
        instance = this;
    }

    public static void ShowMassage(Action action, string text)
    {
        GameObject massageBox = Instantiate(instance.Template);
        Transform panel = massageBox.transform.Find("Panel");
        
//        panel.Find("Text").GetComponent<TextMeshProUGUI>().text = text;

        Button ok = panel.Find("Ok").GetComponent<Button>();
        ok.onClick.AddListener(() =>
        {
            action();
            Destroy(massageBox);
        });

     
    }
}
