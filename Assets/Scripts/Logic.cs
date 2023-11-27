using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Logic : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _bestResultText;

    private void Start()
    {
        _bestResultText.text = PlayerPrefs.GetInt("best").ToString();
    }
}
