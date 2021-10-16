using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clampRectTransform : MonoBehaviour
{
    public float padding = 10.0f;
    public float elementSize = 128.0f;
    public float viewSize = 250.0f;

    private RectTransform rect;
    private int amountOfElements;
    private float contentSize;

    private void Start()
    {
        rect = GetComponent<RectTransform> ();    
        }
    private void Update()
    {
        amountOfElements = rect.childCount;
        contentSize = ((amountOfElements * (elementSize + padding))-viewSize) * rect.localScale.x;

        if (rect.localPosition.x > padding)
            rect.localPosition = new Vector3 (padding , rect.localPosition.y, rect.localPosition.z);
            else if(rect.localPosition.x < -contentSize)
            rect.localPosition = new Vector3 (-contentSize , rect.localPosition.y, rect.localPosition.z);
    }
}
