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
    protected NetworkServer server;

    override void Start()
    {
        base.Start();
        server.Listen(32389);
    }


    protected override void OnUpdateFrame(Frame frame)
    {
        base.OnUpdateFrame(frame);
        SendDataToClient(frame);
        for (int i = 0; i < frame.Hands.Count; i++)
        {
            Hand curHand = frame.Hands[i];
            if (curHand.IsLeft)
            {

                Debug.Log("Left Hand tip position: " + curHand.Fingers[1].TipPosition.ToString());
            }
            else
            {
                Debug.Log("Right Hand: ");
            }
        }
    }



    public class HandPositionMessage : MessageBase
    {
        bool leftHandExists;
        Vector leftIndexFingerTip;
        bool rightHandExists;
        Vector rightIndexFingerTip;

        HandPositionMessage(Frame frame)
        {
            leftHandExists = false;
            rightHandExists = false;
            foreach (Hand curHand in frame.Hands)
            {
                if (curHand.IsLeft)
                {
                    leftHandExists = true;
                    leftIndexFingerTip = curHand.Fingers[1].TipPosition;
                }
                else
                {
                    rightHandExists = true;
                    rightIndexFingerTip = curHand.Fingers[1].TipPosition;
                }
            }
        }
    }

    public class MyMessageTypes
    {
        public static short frame = 1001;
        public static short connect = 1002;
    };

    void SendDataToClient(Frame frame)
    {
        if(server.connections.Count != 0)
        {
            msg = new HandPositionMessage(frame);

            server.sendToAll(MyMessageTypes.frame, msg);
        }
    }
    

}

