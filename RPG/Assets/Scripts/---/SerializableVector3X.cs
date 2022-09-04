using UnityEngine;

namespace RPG.lol 
{
    [System.Serializable]
    public class SerializableVector3X
    {
        float x, y, z;

        public SerializableVector3X(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        public Vector3 ToVector() => new Vector3(x, y, z);
    }
}
