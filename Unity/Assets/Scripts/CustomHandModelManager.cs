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
    protected override void OnUpdateFrame(Frame frame)
    {
        base.OnUpdateFrame(frame);
        for (int i=0; i< frame.Hands.Count; i++)
        {
            Hand curHand = frame.Hands[i];
            if (curHand.IsLeft)
            {
                Debug.Log("Left Hand: ");
            }
            else
            {
                Debug.Log("Right Hand: ");
            }
        }
    }
    

}

