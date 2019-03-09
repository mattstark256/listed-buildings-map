using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkerUI : MonoBehaviour {

    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Text dateText;

    public void DisplayName(string name)
    {
        nameText.text = name;
    }

    public void DisplayDate(string date)
    {
        dateText.text = date;
    }
}
