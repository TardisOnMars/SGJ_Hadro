using System;
using UnityEngine;

public class AttackButtonCanvas : MonoBehaviour
{


    public Transform toFollow;
    public float yOffset;

    private RectTransform rect;

    public void Start()
    {
        rect = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 localPosition = toFollow.localPosition;
        rect.position = new Vector3(localPosition.x, localPosition.y+yOffset, localPosition.z);

    }
}
