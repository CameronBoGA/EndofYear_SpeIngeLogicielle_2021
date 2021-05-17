using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDefinition : MonoBehaviour
{
    public enum Typology {Wall, Touret, Player, Swamp};
    public Vector3 pos;
    public Typology objectType;
}
