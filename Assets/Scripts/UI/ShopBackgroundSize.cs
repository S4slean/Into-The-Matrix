using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBackgroundSize : MonoBehaviour
{

    RectTransform rectTransform;

    // Start is called before the first frame update
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        //rectTransform.sizeDelta = new Vector2(Screen.width +100, Screen.height‬ - (Screen.height * 0.078125f));

        float width = rectTransform.sizeDelta.x;
        float height = Screen.height;
        rectTransform.sizeDelta = new Vector2(width, height -210);
    }

}
