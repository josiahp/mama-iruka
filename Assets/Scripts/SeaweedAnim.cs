﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SeaweedAnim : MonoBehaviour
{
    public SpriteShapeController spriteShapeController;
    private float totalMove = 0f;
    public float speed = 0.1f;
    public float maxMovement;
    public int numSplines;
    private int k = 1;
    //public float tangentLength = 1.0f;

    // Update is called once per frame
    void Start() {
        Spline spline = spriteShapeController.spline;
        Vector3 posStart = spline.GetPosition(0);
        Vector3 posStop = spline.GetPosition(1);
        float yTop = posStop.y;
        float yBottom = posStart.y;
        float x = (posStart.x + posStop.x) / 2;
        float z = (posStart.z + posStop.z) / 2;
        float step = (yTop - yBottom) / numSplines;
        spline.RemovePointAt(1);
        for (int i = 1; i < numSplines; i++) {
            spline.InsertPointAt(i, new Vector3(x, yBottom + i * step, z));
        }
        spline.InsertPointAt(numSplines, posStop);
        spriteShapeController.RefreshSpriteShape();
    }
    void Update() {
        SetSpline();
    }

    void SetSpline() {
        Spline spline = spriteShapeController.spline;
        if (totalMove >= maxMovement) {
                totalMove = 0f;
                k = -k;
            }
        for (int i = 1; i < spline.GetPointCount(); i++) {
            Vector3 pos = spline.GetPosition(i);
            //Debug.Log(pos);
            spline.RemovePointAt(i);
            spline.InsertPointAt(i, new Vector3(pos.x + k * speed, pos.y + k * speed/10, pos.z));
            if (i == 1) {
                totalMove += Mathf.Abs(k*speed); 
                Debug.Log(totalMove);    
            }
            k = -k;
            spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            /*spline.SetRightTangent(i, rotation * Vector3.down * tangentLength);
            spline.SetLeftTangent(i, rotation * Vector3.up * tangentLength); */
        }
        spriteShapeController.RefreshSpriteShape();
    }
}