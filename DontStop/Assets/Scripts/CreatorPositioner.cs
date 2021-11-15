using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class CreatorPositioner : MonoBehaviour
{
    private int rayDistance = 1000;
    public Camera creatorCamera;
    
    private PlayerControls controls;

    private int spacing = 15;
    
    [SerializeField] private List<GameObject> planeTiles = new List<GameObject>();
    [SerializeField] private List<GameObject> emptyTiles = new List<GameObject>();
    [SerializeField] private GameObject planePrefab;
    [SerializeField] private GameObject emptyPrefab;
    // Start is called before the first frame update
    void Awake()
    {
        controls = new PlayerControls();
    }

    private void OnClickCreate()
    {
        Vector3 spawnPosition;
        var ray = creatorCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out var hit, 1000f))
        {
            Debug.Log("Left click");
            spawnPosition = hit.point;
            spawnPosition.z = ((int)((spawnPosition.z + (spacing/2)) / spacing)) * spacing;
            spawnPosition.y = ((int)((spawnPosition.y + (spacing/2)) / spacing)) * spacing;
            if(hit.point.x < 0)
                spawnPosition.x = ((int)((spawnPosition.x - (spacing/2)) / spacing)) * spacing;
            else
                spawnPosition.x = ((int)((spawnPosition.x + (spacing/2)) / spacing)) * spacing;
            if (!(planeTiles.Where(tile =>
                    tile.transform.position.x == spawnPosition.x &&
                    tile.transform.position.z == spawnPosition.z))
                .Any())
            {
                GameObject g = Instantiate(planePrefab, new Vector3(spawnPosition.x, 0, spawnPosition.z),
                    Quaternion.identity);
                planeTiles.Add(g);
                g.name = "MyObject";

                if (!(emptyTiles.Where(tile =>
                    tile.transform.position.x == spawnPosition.x - spacing &&
                    tile.transform.position.z == spawnPosition.z + spacing)).Any())
                {
                    GameObject empty2 = Instantiate(emptyPrefab,
                        new Vector3(spawnPosition.x - spacing, -0.1f, spawnPosition.z + spacing),
                        Quaternion.identity);
                    emptyTiles.Add(empty2);
                }

                if (!(emptyTiles.Where(tile =>
                    tile.transform.position.x == spawnPosition.x &&
                    tile.transform.position.z == spawnPosition.z + spacing)).Any())
                {
                    GameObject empty3 = Instantiate(emptyPrefab,
                        new Vector3(spawnPosition.x, -0.1f, spawnPosition.z + spacing),
                        Quaternion.identity);
                    emptyTiles.Add(empty3);
                }

                if (!(emptyTiles.Where(tile =>
                    tile.transform.position.x == spawnPosition.x + spacing &&
                    tile.transform.position.z == spawnPosition.z + spacing)).Any())
                {
                    GameObject empty4 = Instantiate(emptyPrefab,
                        new Vector3(spawnPosition.x + spacing, -0.1f, spawnPosition.z + spacing),
                        Quaternion.identity);
                    emptyTiles.Add(empty4);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}