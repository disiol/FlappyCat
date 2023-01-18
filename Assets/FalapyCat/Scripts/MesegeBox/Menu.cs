using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    Button okButton;
    public string mesege;

    private void Start()
    {
        
        okButton = GameObject.Find("NoButton").GetComponent<Button>();

        okButton.onClick.AddListener(() =>
        {
            MessageBox.ShowMassage(() =>
            {}, mesege);
        });
    }

}
