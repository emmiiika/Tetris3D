using UnityEngine;

/// <summary>
/// Class <c>Location</c> saves coordinates <c>x, y, z</c> (floats).
/// </summary>
public class Location{
    public float X;
    public float Y;
    public float Z;
    
    public Location(float x, float y, float z){
        this.X = x;
        this.Y = y;
        this.Z = z;
    }

    /// <summary>
    /// Method <c>toVector3</c> converts <c>Location</c>'s float coordinates into <c>Vector3</c>.
    /// </summary>
    public Vector3 ToVector3(){
        return new Vector3(this.X, this.Y, this.Z);
    }
}