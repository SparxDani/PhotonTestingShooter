using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IndicatorScreen : MonoBehaviour
{
    public List<Image> IndicatorDamage = new List<Image>();
    public float speed=2;
    
    // Start is called before the first frame update
    void Start()
    {
        SetIndicator(0);
    }
    public void SetIndicator(int max)
    {
        foreach (var item in IndicatorDamage)
        {
            Color color1 = item.color;
            color1.a = max;
            item.color = color1;
        }
    }
    void UpdateIndicator()
    {
        foreach (var item in IndicatorDamage)
        {
            Color color1 = item.color;
            color1.a = Mathf.Lerp(color1.a, 0f, Time.deltaTime * speed);
            item.color = color1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        UpdateIndicator();
    }
}
