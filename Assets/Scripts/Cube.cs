using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Class <c>Cube</c> models a cube.
/// </summary>
[Inspectable][Serializable]
public class Cube {
    public int id;
    public Location LocalLocation; // location in the grid, (0, 0, 0), (0, 0, 1), (0, 1, 0)...
    public Location GlobalLocation; // actual location in world space
    public Material material;
    public Material transparentMaterial;
    public int cubeSize;
    public GameObject cubeObject;
    
    private Renderer _renderer;
    private Location _loc;

    /// <summary>
    /// Constructor for a <c>Cube</c> object.
    /// </summary>
    /// <param name="localLocation">location in the cube-grid.</param>
    /// <param name="globalLocation">location in world space.</param>
    /// <param name="material">material of the cube.</param>
    /// <param name="transparentMaterial">material of the cube if it is empty.</param>
    /// <param name="idCounter">Autoincrement object that adds id to each cube.</param>
    /// <param name="cubeSize">size of one side of cube.</param>
    public Cube(Location localLocation, Location globalLocation, Material material, Material transparentMaterial, AutoIncrement idCounter, int cubeSize=1){
        this.LocalLocation = localLocation;
        this.GlobalLocation = globalLocation;
        this.material = material;
        this.transparentMaterial = transparentMaterial;
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
        // Debug.Log("Cube (" + this.id + ")" + "\n\tlocalLocation: " + this.LocalLocation.ToVector3() + "\n\tglobalLocation: " + this.GlobalLocation.ToVector3() + "\n\tmaterial: " + this.material.name);
        Debug.Log("Cube (" + this.id + ")" + "\n\tlocalLocation: " + LocalLocation.ToVector3() + "\n\tmaterial: " + this.material.name);

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
        return (this.material != transparentMaterial);
    }
}
