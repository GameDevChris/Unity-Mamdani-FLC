using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct RuleConnection
{
    public MF membership;
    public int index;

    public RuleConnection(MF membership, int index)
    {
        this.membership = membership;
        this.index = index;
    }
}
