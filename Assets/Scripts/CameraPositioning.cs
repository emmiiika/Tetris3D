using UnityEngine;

/// <summary>
/// Method <c>CameraPositioning</c> holds the game login related to the camera position.
/// </summary>
public class CameraPositioning{
    private Camera _camera = Camera.main;
    
    private Vector3[] _facesNormals = new []{
        // new Vector3(0,1,0), // up face
        new Vector3(-1,0,0), // left face
        new Vector3(0,0,-1), // to me face
        new Vector3(1,0,0), // right face
        new Vector3(0,0,1), // from me face
        // new Vector3(0,-1,0), // down face
    };
    
    // movement deltas depending on from where we are looking from
    private Vector3[,] _directions = new Vector3[4, 2]{
        // Right direction, Left direction
        {new Vector3(0, 0, 1), new Vector3(0, 0, -1)}, // left face
        {new Vector3(-1, 0, 0), new Vector3(1, 0, 0)}, // to me face
        {new Vector3(0, 0, -1), new Vector3(0, 0, 1)}, // right face
        {new Vector3(1, 0, 0), new Vector3(-1, 0, 0)}, // from me face
    };

    /// <summary>
    /// Method <c>GetCameraFacingFace</c> gets the most visible face of cube-grid.
    /// </summary>
    /// <param name="localSpaceTransform">the wrapper Transform of the cube-grid.</param>
    public int GetCameraFacingFace(Transform localSpaceTransform){
        // left (0), to me (1), from me (2), right (3)
        Vector3 cameraNormal = _camera.transform.forward;
        Vector3 cameraLocalNormal = localSpaceTransform.InverseTransformDirection(cameraNormal);

        int mostVisibleSide = -1;
        float maxDotProduct = -1;
        for(int i = 0; i < _facesNormals.Length; i++){
            
            Vector3 normal = _facesNormals[i];
            cameraLocalNormal.y = 0;
            cameraLocalNormal = cameraLocalNormal.normalized;
            float dotProduct = Vector3.Dot(cameraLocalNormal, normal);
            
            if (dotProduct > maxDotProduct){
                maxDotProduct = dotProduct;
                mostVisibleSide = i;
            }
        }

        return mostVisibleSide;
    }

    /// <summary>
    /// Method <c>GetDirectionArray</c> gets the side movement vector based on the most visible face of the cube-grid.
    /// </summary>
    /// <param name="localSpaceTransform">the wrapper Transform of the cube-grid.</param>
    public Vector3[] GetDirectionArray(Transform localSpaceTransform){
        int mostVisibleSide = GetCameraFacingFace(localSpaceTransform);

        return new Vector3[] {_directions[mostVisibleSide, 0], _directions[mostVisibleSide, 1]};
    }
}