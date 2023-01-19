using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

[CreateAssetMenu(fileName = "CrispData", menuName = "Fuzzy/CrispData")]
public class CrispData : ScriptableObject
{
    public List<CrispValue> crisps = new List<CrispValue>
    {
        new CrispValue("temperature", 25f, -10f, 30f),
        new CrispValue("humidity", 50f, 0, 100)
    };
}
