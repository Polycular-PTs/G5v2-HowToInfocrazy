using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "NewLeakStage", menuName = "LeakStageInfokratie")]
public class LeakDataStage : ScriptableObject
{
    [Header("On Time Out")]
    public int happiness;
    public int budget;
    public int opposition;
    public int functionality;

    [Header("Leaks")]
    public List<LeakData> leaks;
}