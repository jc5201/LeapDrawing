/******************************************************************************
 * Copyright (C) Leap Motion, Inc. 2011-2018.                                 *
 * Leap Motion proprietary and confidential.                                  *
 *                                                                            *
 * Use subject to the terms of the Leap Motion SDK Agreement available at     *
 * https://developer.leapmotion.com/sdk_agreement, or another agreement       *
 * between Leap Motion and you, your company or other organization.           *
 ******************************************************************************/

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;
using System;
using Leap.Unity.Attributes;
using Leap.Unity;
using Leap;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// The HandModelManager manages a pool of HandModelBases and makes HandRepresentations
/// when a it detects a Leap Hand from its configured LeapProvider.
/// 
/// When a HandRepresentation is created, a HandModelBase is removed from the pool.
/// When a HandRepresentation is finished, its HandModelBase is returned to the pool.
/// 
/// This class was formerly known as HandPool.
/// </summary>
public class CustomHandModelManager : Leap.Unity.HandModelManager
{
    private Vector3 invalidVector = new Vector3(-999, -999, -999);
    private Quaternion invalidQuar = new Quaternion(-999, -999, -999, -999);

    private Vector3 savedPalmPosition1;
    private Vector3 savedPalmPosition2;

    private Quaternion savedRotation1;
    private Quaternion savedRotation2;

    private Vector3 savedFingerDistance1;
    private Vector3 savedFingerDistance2;

    protected override void OnUpdateFrame(Frame frame)
    {
        base.OnUpdateFrame(frame);

        savedPalmPosition2 = savedPalmPosition1;
        savedRotation2 = savedRotation1;
        savedFingerDistance2 = savedFingerDistance1;
        if (frame.Hands.Count != 1 || !frame.Hands[0].IsLeft)
        {
            savedPalmPosition1 = invalidVector;
            savedRotation1 = invalidQuar;
            savedFingerDistance1 = invalidVector;
        }
        else
        {
            savedPalmPosition1 = UnityVectorExtension.ToVector3(frame.Hands[0].PalmPosition);
            savedRotation1 = ToQuaternion(frame.Hands[0].Rotation);
            savedFingerDistance1 = UnityVectorExtension.ToVector3(frame.Hands[0].Fingers[1].TipPosition)
                - UnityVectorExtension.ToVector3(frame.Hands[0].Fingers[0].TipPosition);
            savedFingerDistance1 = new Vector3(Mathf.Abs(savedFingerDistance1.x), Mathf.Abs(savedFingerDistance1.y), Mathf.Abs(savedFingerDistance1.z));
        }
    }

    public Vector3 deltaMovement()
    {
        // Debug.Log(savedPalmPosition1 + " and " + savedPalmPosition2);
        if (savedPalmPosition1 == invalidVector || savedPalmPosition2 == invalidVector)
            return new Vector3(0, 0, 0);
        return savedPalmPosition1 - savedPalmPosition2;
    }

    public Quaternion deltaRotation()
    {
        if (savedRotation1 == invalidQuar || savedRotation2 == invalidQuar)
            return Quaternion.identity;
        return Quaternion.Inverse(savedRotation2) * savedRotation1;
    }

    public Vector3 deltaFinger()
    {
        if (savedFingerDistance1 == invalidVector || savedFingerDistance2 == invalidVector)
            return new Vector3(0, 0, 0);
        Vector3 distance = savedFingerDistance1 - savedFingerDistance2;
        return new Vector3(distance.x, distance.y, distance.z);
    }

    public Quaternion ToQuaternion(LeapQuaternion q)
    {
        return new Quaternion(q.x, q.y, q.z, q.w);
    }


}

