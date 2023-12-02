using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : Singleton<Hover>
{
    private Camera hoverCamera;

    public GameObject HoverPrefabInstance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        hoverCamera = Camera.main;
    }

    void Update()
    {
        if(HoverPrefabInstance != null)
            FollowMouse();
    }

    private void FollowMouse()
    {
        var position = hoverCamera.ScreenToWorldPoint(Input.mousePosition);
        HoverPrefabInstance.transform.position = new Vector3(position.x, position.y, -1);
    }

    public void SetHoverPrefabInstance(GameObject instance)
    {
        if(HoverPrefabInstance != null)
        {
            Destroy(HoverPrefabInstance.gameObject);
        }

        HoverPrefabInstance = instance;
    }
}
