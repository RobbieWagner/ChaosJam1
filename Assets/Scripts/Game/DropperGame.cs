using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropperGame : MonoBehaviour
{
    [Header("Top")]
    [SerializeField] private Transform top;

    [Header("Bottom")]
    [SerializeField] private Transform bottom;
    [SerializeField] private BallResetterManager ballResetterManager;

    [Header("Build")]
    [SerializeField] private float startingRowYPos;
    private float newRowYPos;

    public static DropperGame Instance {get; private set;}

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(gameObject); 
        } 
        else 
        { 
            Instance = this; 
        } 

        newRowYPos = top.position.y + startingRowYPos;
    }

    public void AddRow(GameObject rowPrefab, float height)
    {
        if (rowPrefab != null)
        {
            GameObject newRow = Instantiate(rowPrefab, transform);
            newRow.transform.position += new Vector3(transform.position.x, newRowYPos - height/2, transform.position.z);
            newRowYPos -= height;
            bottom.position += Vector3.down * height;
        }
    }
}
