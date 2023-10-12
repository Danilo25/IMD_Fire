using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_Interactable
{
    bool hasTag(string tag);
    void Kick(Vector3 dir, float strength = 1f);
}
