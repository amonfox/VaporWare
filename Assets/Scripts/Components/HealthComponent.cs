﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public struct HealthComponent : IComponentData
    {
        public float Max;
        public float Current;
    }
}
