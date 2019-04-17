// ===========
// DO NOT EDIT - this file is automatically regenerated.
// ===========

using System.Linq;
using Improbable.Gdk.Core;
using UnityEngine;

namespace Improbable.Gdk.Standardtypes
{
    
[System.Serializable]
public struct IntAbsolute
{
    public int X;
    public int Y;
    public int Z;

    public IntAbsolute(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }
    public static class Serialization
    {
        public static void Serialize(IntAbsolute instance, global::Improbable.Worker.CInterop.SchemaObject obj)
        {
            {
                obj.AddInt32(1, instance.X);
            }
            {
                obj.AddInt32(2, instance.Y);
            }
            {
                obj.AddInt32(3, instance.Z);
            }
        }

        public static IntAbsolute Deserialize(global::Improbable.Worker.CInterop.SchemaObject obj)
        {
            var instance = new IntAbsolute();
            {
                instance.X = obj.GetInt32(1);
            }
            {
                instance.Y = obj.GetInt32(2);
            }
            {
                instance.Z = obj.GetInt32(3);
            }
            return instance;
        }
    }
}

}
