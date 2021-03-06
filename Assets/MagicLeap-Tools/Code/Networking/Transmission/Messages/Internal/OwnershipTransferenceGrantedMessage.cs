﻿// ---------------------------------------------------------------------
//
// Copyright (c) 2019 Magic Leap, Inc. All Rights Reserved.
// Use of this file is governed by the Creator Agreement, located
// here: https://id.magicleap.com/creator-terms
//
// ---------------------------------------------------------------------

namespace MagicLeapTools
{
    public class OwnershipTransferenceGrantedMessage : TransmissionMessage
    {
        //Public Variables(truncated to reduce packet size):
        /// <summary>
        /// instanceGUID
        /// </summary>
        public string ig;

        //Constructors:
        public OwnershipTransferenceGrantedMessage(string instanceGuid, string requestor) : base(TransmissionMessageType.OwnershipTransferenceGrantedMessage, TransmissionAudience.SinglePeer, requestor, true)
        {
            ig = instanceGuid;
        }
    }
}