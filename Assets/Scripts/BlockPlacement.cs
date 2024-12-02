using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BlockPlacement : MonoBehaviour{
    public void PlaceBlock(Cube[,,] grid, GameObject block, Location localLocation){
        int x = (int)localLocation.x;
        int y = (int)localLocation.y;
        int z = (int)localLocation.z;

        Renderer renderer;
        foreach (Transform child in block.transform){
             renderer = child.GetComponent<Renderer>();
            Material blockMaterial = renderer.sharedMaterial;
            // Debug.Log(blockMaterial.name);
            
            float i = child.localPosition.x;
            float j = child.localPosition.y;
            float k = child.localPosition.z;
            // Debug.Log(i + "," + j + "," + k);
            
            grid[(int)(x + i), (int)(y + j), (int)(z + k)].SetMaterial(blockMaterial);
            // Debug.Log((int)(x + i) + ", " + (int)(y + j) + ", " + (int)(z + k));
        }
    }

}