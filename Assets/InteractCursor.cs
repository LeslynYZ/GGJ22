using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class InteractCursor : MonoBehaviour
{


    public Tile highlightTile;
    public Tilemap highlightMap;

    public Tile selectedTile;
    public Tilemap tileMap;
 
    private Vector3Int previous;

     LayerMask tileLayer;
 

 void OnEnable()
 {
     highlightMap = GameObject.FindWithTag("HilightMap").GetComponent<Tilemap>();
     tileMap = GameObject.FindWithTag("TileMap").GetComponent<Tilemap>();
 }
    // do late so that the player has a chance to move in update if necessary

    public void Destroy()
    {
        Vector3Int currentCell = tileMap.WorldToCell(transform.position);
            // set the new tile
            tileMap.SetTile(currentCell, null);
    }
    private void LateUpdate()
    {
        // get current grid location
        Vector3Int currentCell = highlightMap.WorldToCell(transform.position);
 
        // if the position has changed
        if(currentCell != previous)
        {
            // set the new tile
            highlightMap.SetTile(currentCell, highlightTile);
 
            // erase previous
            highlightMap.SetTile(previous, null);
 
            // save the new position for next frame
            previous = currentCell;
        }
    }
}
