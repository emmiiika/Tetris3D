using UnityEngine;

/// <summary>
/// Class <c>Location</c> saves coordinates <c>x, y, z</c> (floats).
/// </summary>
public class Location{
    public float x;
    public float y;
    public float z;
    
    public Location(float x, float y, float z){
        this.x = x;
        this.y = y;
        this.z = z;
    }

    /// <summary>
    /// Method <c>toVector3</c> converts <c>Location</c>'s float coordinates into <c>Vector3</c>.
    /// </summary>
    public Vector3 ToVector3(){
        return new Vector3(this.x, this.y, this.z);
    }
}