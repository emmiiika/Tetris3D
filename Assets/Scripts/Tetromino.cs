using UnityEngine;

/// <summary>
/// Class <c>Tetromino</c> models one tetromino.
/// </summary>
public class Tetromino{
    public string Name;
    public GameObject Prefab;
    public Material Material;
    public Transform[] Blocks;

    private Renderer _renderer;

    /// <summary>
    /// Constructor <c>Tetromino</c> creates one tetromino.
    /// </summary>
    /// <param name="prefab">tetromino <c>GameObject</c>.</param>
    public Tetromino(GameObject prefab){
        string prefabPath = "Assets/Prefabs/" + prefab.name + ".prefab";
        
        switch (prefab.name){
            case "BlockI":
                this.Name = "BlockI";
                break;
            case "BlockO":
                this.Name = "BlockO";
                break;
            case "BlockL":
                this.Name = "BlockL";
                break;
            case "BlockZ":
                this.Name = "BlockZ";
                break;
        }

        this.Prefab = prefab;
        this.Blocks = new Transform[4];
        int i = 0;
        foreach (Transform block in prefab.transform){
            Blocks[i++] = block;
        }
    }

}