﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float rotationSpeed=1.0f;
    
    void Update()
    {
        transform.Rotate(0,0,rotationSpeed*Time.deltaTime);
    }
}
