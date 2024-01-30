using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Symbol
{
    public List<float> allAngles;

    public Material matToApply;

    public GameObject VFX;
}

public class SymbolStorage : ScriptableObject
{
    public List<Symbol> symbolDataBase;
}
