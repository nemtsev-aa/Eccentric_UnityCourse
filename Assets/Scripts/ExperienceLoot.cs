using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceLoot : Loot
{
    [Tooltip("���������� �����")]
    [SerializeField] private int _experienceValue;

    public override void Take(Collector collector)
    {
        base.Take(collector);
        collector.TakeExperince(_experienceValue);
    }
}
  