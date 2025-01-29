using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Class <c>BlockPlacement</c> .
/// </summary>
public class BlockPlacement : MonoBehaviour{
    
    /// <summary>
    /// Method <c>PlaceBlock</c> puts a tetromino (<c>block</c>) inside the cube-grid if possible.
    /// </summary>
    /// <param name="grid">the cube-grid.</param>
    /// <param name="block">one tetromino.</param>
    /// <param name="localLocation">location of the first tetromino block in the cube-grid.</param>
    public bool PlaceBlock(Cube[,,] grid, GameObject block, Location localLocation){
        int x = (int)localLocation.x;
        int y = (int)localLocation.y;
        int z = (int)localLocation.z;

        // if (CanPlaceBlock(grid, block, localLocation)){
            Renderer renderer;
            foreach (Transform child in block.transform){
                // color relevant cube-grid's cubes with tetromino's color based on each tetromino's block position
                renderer = child.GetComponent<Renderer>();
                Material blockMaterial = renderer.sharedMaterial;

                float i = child.localPosition.x;
                float j = child.localPosition.y;
                float k = child.localPosition.z;

                grid[(int)(x + i), (int)(y + j), (int)(z + k)].SetMaterial(blockMaterial);
            }

        //     return true;
        // }
        
        // TODO: save the actual tetromino into the grid?

        return true;
    }
    
    /// <summary>
    /// Method <c>IsBlockInsideCube</c> resolves if tetromino (<c>block</c>) is inside the cube-grid.
    /// </summary>
    /// <param name="grid">the cube-grid.</param>
    /// <param name="block">one tetromino.</param>
    /// <param name="localLocation">location of the first tetromino block in the cube-grid.</param>
    public bool IsBlockInsideCube(Cube[,,] grid, GameObject block, Location localLocation){
        int x = (int)localLocation.x;
        int y = (int)localLocation.y;
        int z = (int)localLocation.z;

        int gridSize = grid.Length;

        bool isInside = true;
        // find out if every part of the tetromino is inside the cube-grid
        foreach (Transform child in block.transform){
            float i = child.localPosition.x;
            float j = child.localPosition.y;
            float k = child.localPosition.z;

            if (((int)(x + i) < 0 || (int)(x + i) >= gridSize) ||
                ((int)(y + j) < 0 || (int)(y + j) >= gridSize) ||
                ((int)(z + k) < 0 || (int)(z + k) >= gridSize)){
                isInside = false;
                Debug.Log("Block " + block.name + " is inside the cube: " + isInside);
                break;
            }
        }

        return isInside;


    }
    
    /// <summary>
    /// Method <c>IsCoveringOtherBlocks</c> resolves if tetromino (<c>block</c>) is covering another
    /// tetromino already placed inside the cube-grid.
    /// </summary>
    /// <param name="grid">the cube-grid.</param>
    /// <param name="block">one tetromino.</param>
    /// <param name="localLocation">location of the first tetromino block in the cube-grid.</param>
    public bool IsCoveringOtherBlocks(Cube[,,] grid, GameObject block, Location localLocation){
        int x = (int)localLocation.x;
        int y = (int)localLocation.y;
        int z = (int)localLocation.z;

        int gridSize = grid.Length;

        bool isCovering = false;
        foreach (Transform child in block.transform){
            // if any part of the tetromino is covering any other block
            float i = child.localPosition.x;
            float j = child.localPosition.y;
            float k = child.localPosition.z;

            if (grid[(int)(x + i), (int)(y + j), (int)(z + k)].IsOccupied()){
                isCovering = true;
                Debug.Log("Block " + block.name + " is covering another block: " + isCovering);
                break;
            }
        }

        return isCovering;
    }

    /// <summary>
    /// Method <c>CanPlaceBlock</c> resolves if tetromino (<c>block</c>) can be place on the <c>localLocation</c> position.
    /// </summary>
    /// <param name="grid">the cube-grid.</param>
    /// <param name="block">one tetromino.</param>
    /// <param name="localLocation">location of the first tetromino block in the cube-grid.</param>
    public bool CanPlaceBlock(Cube[,,] grid, GameObject block, Location localLocation){
        // the whole tetromino is inside the cube-grid
        bool isInside = IsBlockInsideCube(grid, block, localLocation);
        
        if (isInside){
            // no part of the tetromino is covering other tetromino
            bool isCoveringOther = IsCoveringOtherBlocks(grid, block, localLocation);
            return isInside && isCoveringOther;
        }
        return false;
    }

}