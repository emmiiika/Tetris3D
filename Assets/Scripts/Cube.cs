using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class Location{
    public float x;
    public float y;
    public float z;
    
    public Location(float x, float y, float z){
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vector3 toVector3(){
        return new Vector3(this.x, this.y, this.z);
    }
}

public class AutoIncrement
{
    private int _id = 0;

    public int GenerateId(){
        return _id++;
    }
}

[Inspectable][Serializable]
public class Cube {
    public int id;
    public Location LocalLocation;
    public Location GlobalLocation;
    public Material material;
    public int cubeSize;
    public GameObject cubeObject;

    public Cube(Location localLocation, Location globalLocation, Material material, AutoIncrement idCounter, int cubeSize=1){
        this.LocalLocation = localLocation;
        this.GlobalLocation = globalLocation;
        this.material = material;
        this.cubeSize = cubeSize;
        
        this.id = idCounter.GenerateId();
    }

    public void GenerateCube(Transform parent, GameObject cubePrefab){
        this.cubeObject = MonoBehaviour.Instantiate(cubePrefab, parent);
        cubeObject.transform.localPosition = this.GlobalLocation.toVector3();
    }

    public void Print(){
        Debug.Log("Cube (" + this.id + ")" + "\n\tlocalLocation: " + this.LocalLocation.toVector3() + "\n\tglobalLocation: " + this.GlobalLocation.toVector3() + "\n\tmaterial: " + this.material.name + "\n\tlocalLocation: " + this.LocalLocation.toVector3());
    }
}
