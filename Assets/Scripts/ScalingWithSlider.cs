using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScalingWithSlider : MonoBehaviour
{
    float scale;
    private Slider slider;
  

  

    void Start()
    {
        slider = GameObject.Find("Scaling Slider").GetComponent<Slider>();

        slider.onValueChanged.AddListener( sliderCallBack);
    }
    

    public void setScalingLimitsForTm5()
    {
        slider.value = 0.44f;
        slider.minValue = 0.2f;
        slider.maxValue = 0.8f;
    }
    public void setScalingLimitsForUR5()
    {
        slider.value = 0.02f;
        slider.minValue = 0.019f;
        slider.maxValue = 0.08f;
    }
    public void setScalingLimitsForKR30()
    {
        slider.value = 0.56f;
        slider.minValue = 0.4f;
        slider.maxValue = 1.2f;
    }
    public void setScalingLimitsForKR8()
    {
        slider.value = 0.89f;
        slider.minValue = 0.4f;
        slider.maxValue = 1.4f;
    }
    public void sliderCallBack(float value)
    {
        transform.localScale = new Vector3(value, value, value);
    }

}
