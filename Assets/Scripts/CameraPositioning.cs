using Unity.VisualScripting;
using UnityEngine;

public class CameraPositioning{
    private Vector3[] facesNormals = new []{
        // new Vector3(0,1,0), // up face
        new Vector3(-1,0,0), // left face
        new Vector3(0,0,-1), // to me face
        new Vector3(1,0,0), // right face
        new Vector3(0,0,1), // from me face
        // new Vector3(0,-1,0), // down face
    };
    private Camera camera = Camera.main;

    public Vector3[] GetDirectionArray(){
        // movement deltas depending on from where we are looking from
        Vector3[,] directions = new Vector3[4, 2]
        {
            // Right direction, Left direction
            { new Vector3(0, 0, -1), new Vector3(0, 0, 1)}, // left face
            { new Vector3(1, 0, 0), new Vector3(-1, 0, 0)}, // to me face
            { new Vector3(0, 0, 1), new Vector3(0, 0, -1)}, // right face
            { new Vector3(-1, 0, 0), new Vector3(1, 0, 0)} // from me face
        };
        
        Vector3 cameraNormal = camera.transform.forward;
        int mostVisibleSide = -1;
        float maxDotProduct = -1;
        for(int i = 0; i < facesNormals.Length; i++){
            Vector3 normal = facesNormals[i];
            float dotProduct = Vector3.Dot(cameraNormal, normal);
            if (dotProduct > maxDotProduct){
                maxDotProduct = dotProduct;
                mostVisibleSide = i;
            }
        }

        Debug.Log(mostVisibleSide);
        return new Vector3[] {directions[mostVisibleSide, 0], directions[mostVisibleSide, 1]};
    }
}