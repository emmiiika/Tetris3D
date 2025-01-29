using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

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

/// <summary>
/// Class <c>AutoIncrement</c> generates a counter for IDs. This class should have only one instance for unique IDs.
/// </summary>
public class AutoIncrement
{
    private int _id = 0;

    /// <summary>
    /// Method <c>GenerateID</c> returns a new ID and increments the counter.
    /// </summary>
    public int GenerateId(){
        return _id++;
    }
}

/// <summary>
/// Class <c>Cube</c> models a cube.
/// </summary>
[Inspectable][Serializable]
public class Cube {
    public int id;
    public Location LocalLocation; // location in the grid, (0, 0, 0), (0, 0, 1), (0, 1, 0)...
    public Location GlobalLocation; // actual location in world space
    public Material material;
    public int cubeSize;
    public GameObject cubeObject;
    
    private Renderer _renderer;

    /// <summary>
    /// Constructor for a <c>Cube</c> object.
    /// </summary>
    /// <param name="localLocation">location in the cube-grid.</param>
    /// <param name="globalLocation">location in world space.</param>
    /// <param name="material">material of the cube.</param>
    /// <param name="idCounter">Autoincrement object that adds id to each cube.</param>
    /// <param name="cubeSize">size of one side of cube.</param>
    public Cube(Location localLocation, Location globalLocation, Material material, AutoIncrement idCounter, int cubeSize=1){
        this.LocalLocation = localLocation;
        this.GlobalLocation = globalLocation;
        this.material = material;
        this.cubeSize = cubeSize;
        
        this.id = idCounter.GenerateId();
    }
    
    /// <summary>
    /// Method <c>GenerateCube</c> "draws" <c>Cube</c> in world space.
    /// </summary>
    /// <param name="parent"><c>Transform</c> of which the <c>Cube</c> will be a child.</param>
    /// <param name="cubePrefab">prefab object of a cube.</param>
    public void GenerateCube(Transform parent, GameObject cubePrefab){
        this.cubeObject = MonoBehaviour.Instantiate(cubePrefab, parent);
        cubeObject.transform.localPosition = this.GlobalLocation.ToVector3();
    }

    /// <summary>
    /// Util method for debbuging. Prints out basic info about a cube.
    /// </summary>
    public void Print(){
        Debug.Log("Cube (" + this.id + ")" + "\n\tlocalLocation: " + this.LocalLocation.ToVector3() + "\n\tglobalLocation: " + this.GlobalLocation.ToVector3() + "\n\tmaterial: " + this.material.name + "\n\tlocalLocation: " + this.LocalLocation.ToVector3());

    }

    /// <summary>
    /// Method <c>SetMaterial</c> sets <c>material</c> of <c>Cube</c>.
    /// </summary>
    /// <param name="material">new material.</param>
    public void SetMaterial(Material material){
        this.material = material;
        
        foreach (Transform child in cubeObject.transform){
            if (child.name == "Cube"){
                child.GetComponent<Renderer>().material = material;

            }
        }
    }

    /// <summary>
    /// Method <c>isOccupied</c> returns if a <c>Cube</c> is "drawn" in the world space.
    /// </summary>
    public bool IsOccupied(){
        return (this.cubeObject != null);
    }
}
