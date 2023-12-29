using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI textMeshPro = transform.GetComponent<TextMeshProUGUI>();
        int min = (int)DataLvl.time / 60;
        float sec = DataLvl.time % 60;
        if (min == 0)
        {
            textMeshPro.text = "�����:  " + (float)System.Math.Round(sec, 2) + " ���";
        }
        else
        {
            textMeshPro.text = "�����:  " + min + " ���  " + (float)System.Math.Round(sec, 2) + " ���";
        }

        MenuManager.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
