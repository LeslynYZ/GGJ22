using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
public class InteractCursor : MonoBehaviour
{
    [SerializeField] Transform player;
    PlayerInput playerInput;


    public Tile highlightTile;
    public Tilemap hilightMap;

    public Tile placeableTile;
    public Tilemap tileMap;

    private Vector3Int previous;
    private Vector3Int selectedCell;
    private Vector3Int hilightedCell;

    [SerializeField] LayerMask tileLayer;

    void OnEnable()
    {
        playerInput = GetComponentInParent<PlayerInput>();
        hilightMap = GameObject.FindWithTag("HilightMap").GetComponent<Tilemap>();
        tileMap = GameObject.FindWithTag("TileMap").GetComponent<Tilemap>();
    }

    public void Destroy()
    {
        // set the new tile
        tileMap.SetTile(selectedCell, null);
    }
    public void Create()
    {
        tileMap.SetTile(selectedCell, placeableTile);
    }    
    private void LateUpdate()
    {
        if (playerInput.playerIndex == 0)
        {
            DesroyHilighter();
        }
        else if (playerInput.playerIndex == 1)
        {
            CreateHilighter();
        }
    }

    private void CreateHilighter()
    {
        RaycastHit2D rayCast = Physics2D.Raycast(player.position,
            (player.position - transform.position).normalized, -4f, tileLayer);
        Debug.DrawRay(player.transform.position, (player.
            position - transform.position).normalized * -3f, Color.red);
        if (rayCast.collider != null)
        {
            if (rayCast.collider.GetComponent<Tilemap>())
            {
                selectedCell = tileMap.WorldToCell(rayCast.point);
                if (transform.position.x > player.position.x)
                {
                    selectedCell = new Vector3Int(selectedCell.x - 1, selectedCell.y, selectedCell.z);
                }
                if (transform.position.y > player.position.y)
                {
                    selectedCell = new Vector3Int(selectedCell.x, selectedCell.y - 1, selectedCell.z);
                }
            }
        }
        hilightMap.SetTile(selectedCell, highlightTile);

        if (previous != selectedCell)
        {
            hilightMap.SetTile(previous, null);

            previous = selectedCell;
        }
    }

    private void DesroyHilighter()
    {
        RaycastHit2D rayCast = Physics2D.Raycast(player.position,
            (player.position - transform.position).normalized, -3f, tileLayer);
        Debug.DrawRay(player.transform.position, (player.
            position - transform.position).normalized * -3f, Color.red);
        if (rayCast.collider != null)
        {
            if (rayCast.collider.GetComponent<Tilemap>())
            {
                selectedCell = tileMap.WorldToCell(rayCast.point);
                if (transform.position.x < player.position.x)
                {
                    selectedCell = new Vector3Int(selectedCell.x - 1, selectedCell.y, selectedCell.z);
                }
                if (transform.position.y < player.position.y)
                {
                    selectedCell = new Vector3Int(selectedCell.x, selectedCell.y - 1, selectedCell.z);
                }
            }
            hilightMap.SetTile(selectedCell, highlightTile);
        }

        if (previous != selectedCell)
        {
            hilightMap.SetTile(previous, null);

            previous = selectedCell;
        }
    }
}
