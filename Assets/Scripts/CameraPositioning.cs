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

    public Vector3[] GetDirectionArray(Transform localSpaceTransform){
        // movement deltas depending on from where we are looking from
        Vector3[,] directions = new Vector3[4, 2]
        {
            // Right direction, Left direction
            {new Vector3(0, 0, 1), new Vector3(0, 0, -1)}, // left face
            {new Vector3(-1, 0, 0), new Vector3(1, 0, 0)}, // to me face
            {new Vector3(0, 0, -1), new Vector3(0, 0, 1)}, // right face
            {new Vector3(1, 0, 0), new Vector3(-1, 0, 0)}, // from me face
        };
        
        Vector3 cameraNormal = camera.transform.forward;
        Vector3 cameraLocalNormal = localSpaceTransform.InverseTransformDirection(cameraNormal);
        
        int mostVisibleSide = -1;
        float maxDotProduct = -1;
        for(int i = 0; i < facesNormals.Length; i++){
            
            Vector3 normal = facesNormals[i];
            cameraLocalNormal.y = 0;
            cameraLocalNormal = cameraLocalNormal.normalized;
            float dotProduct = Vector3.Dot(cameraLocalNormal, normal);
            
            if (dotProduct > maxDotProduct){
                maxDotProduct = dotProduct;
                mostVisibleSide = i;
            }
        }

        return new Vector3[] {directions[mostVisibleSide, 0], directions[mostVisibleSide, 1]};
    }
}