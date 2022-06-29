using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIFinishScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finishText;
    private TimeSaver timeSaver;

    private void Awake()
    {
        timeSaver = FindObjectOfType<TimeSaver>();
    }

    private void Start()
    {
        finishText.text = timeSaver.GetTime();
    }

}
