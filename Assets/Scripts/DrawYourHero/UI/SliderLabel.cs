using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DrawYourHero.UI
{
    public class SliderLabel : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private int stringRound = 1;

        private void OnEnable()
        {
            label.text = slider.value.ToString("F" + stringRound);
            slider.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnValueChanged(float value)
        {
            label.text = value.ToString("F" + stringRound);
        }
    }
}