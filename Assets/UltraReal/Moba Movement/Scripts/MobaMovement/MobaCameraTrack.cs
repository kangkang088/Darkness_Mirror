﻿using UnityEngine;
using System.Collections;

namespace UltraReal.MobaMovement
{
    public abstract class MobaCameraTrack : MonoBehaviour
    {
        public abstract void SetTarget(Transform target);
    }
}
