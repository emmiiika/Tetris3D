using System;
using Unity.VisualScripting;
using UnityEngine;

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
    public GameObject cubeObject;

    /// <summary>
    /// Constructor for a <c>Cube</c> object.
    /// </summary>
    /// <param name="localLocation">location in the cube-grid.</param>
    /// <param name="globalLocation">location in world space.</param>
    /// <param name="material">material of the cube.</param>
    /// <param name="transparentMaterial">material of the cube if it is empty.</param>
    /// <param name="idCounter">Autoincrement object that adds id to each cube.</param>
    public Cube(Location localLocation, Location globalLocation, Material material, Material transparentMaterial, AutoIncrement idCounter){
        this.LocalLocation = localLocation;
        this.GlobalLocation = globalLocation;
        this.material = material;
        this.transparentMaterial = transparentMaterial;
        
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
    /// Method <c>GenerateCube</c> "draws" <c>Cube</c> in world space.
    /// </summary>
    /// <param name="parent"><c>Transform</c> of which the <c>Cube</c> will be a child.</param>
    /// <param name="cubePrefab">prefab object of a cube.</param>
    /// <param name="isVisible">whether the cube is visible.</param>
    public void GenerateCube(Transform parent, GameObject cubePrefab, bool isVisible){
        this.cubeObject = MonoBehaviour.Instantiate(cubePrefab, parent);
        cubeObject.transform.localPosition = this.GlobalLocation.ToVector3();

        if (isVisible){
            cubeObject.SetActive(true);
        }
        else{
            cubeObject.SetActive(false);
        }
    }

    /// <summary>
    /// Method <c>SetActive</c> shows/hides <c>Cube</c> in world space.
    /// </summary>
    /// <param name="isVisible">if the cube should be visible.</param>
    public void SetActive(bool isVisible){
        if (isVisible){
            cubeObject.SetActive(true);
        }
        else{
            cubeObject.SetActive(false);
        }
    }
    
    
    /// <summary>
    /// Util method for debbuging. Prints out basic info about a cube.
    /// </summary>
    public void Print(){
        Debug.Log("Cube (" + this.id + ")" + "\n\tlocalLocation: " + LocalLocation.ToVector3() + "\n\tmaterial: " + this.material.name);

    }

    /// <summary>
    /// Method <c>SetMaterial</c> sets <c>material</c> of <c>Cube</c>.
    /// </summary>
    /// <param name="material">new material.</param>
    public void 
        SetMaterial(Material material){
        // Debug.Log("really setting the new material");
        this.material = material;
        
        // Debug.Log("going to color blocks: " + cubeObject);
        foreach (Transform child in cubeObject.transform){
            // Debug.Log("\tColoring block");
            if (child.name == "Cube"){
                // Debug.Log("it is a cube");
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
