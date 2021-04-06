using System;
using UnityEngine;
public class EditorObject : MonoBehaviour
{
    public enum ObjectType {Wall, Touret, Player, Swamp};
    [Serializable]
    public struct Data
    {
        public Vector3 pos;
        public ObjectType objectType;
    }
    public Data data;
}