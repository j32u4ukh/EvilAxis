﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour
{
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(Camera.main.transform.position, hit.transform.position, Color.red, 0.1f, true);
            Debug.Log(hit.transform.name);

            Destroy(hit.collider.gameObject);
        }
    }
}
