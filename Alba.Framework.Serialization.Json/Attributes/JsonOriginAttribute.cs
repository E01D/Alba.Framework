﻿using System;

namespace Alba.Framework.Serialization.Json
{
    [AttributeUsage (AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class JsonOriginAttribute : JsonLinkedAttribute
    {}
}