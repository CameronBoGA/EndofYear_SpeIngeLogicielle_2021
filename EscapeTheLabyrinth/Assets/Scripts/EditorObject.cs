using System;
using UnityEngine;

public class EditorObject : MonoBehaviour
{
    public enum ObjectType {Wall, Caltrop, Player, Mud, End};
    [Serializable]
    public struct Data
    {
        public Vector3 pos;
        public ObjectType objectType;
    }
    public Data data;
}