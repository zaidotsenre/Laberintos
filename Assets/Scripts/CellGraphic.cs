using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGraphic : MonoBehaviour
{
    [SerializeField] GameObject topWall;
    [SerializeField] GameObject rightWall;
    [SerializeField] GameObject bottomWall;
    [SerializeField] GameObject leftWall;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject exitMarker;

    Transform thisTransform;

    public Cell CellData
    {
        set
        {
            cellData = value;
            topWall.SetActive(!cellData.top);
            rightWall.SetActive(!cellData.right);
            bottomWall.SetActive(!cellData.bottom);
            leftWall.SetActive(!cellData.left);
            if (cellData.exit)
                AddCollider();
        }
    }

    GameObject player;
    Cell cellData;

    private void Start()
    {
        thisTransform = transform;

        if (cellData.entry)
        {
            player = Instantiate(playerPrefab, thisTransform.position, Quaternion.identity);
        }
        if (cellData.exit)
        {
            Instantiate(exitMarker, thisTransform.position, Quaternion.Euler(-90, 0, 0), thisTransform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (cellData.exit)
        {
            EventBroker.onWin.Invoke();
        }
    }

    private void OnDestroy()
    {
        if(cellData.entry)
            Destroy(player);
    }

    void AddCollider()
    {
        BoxCollider collider = gameObject.AddComponent<BoxCollider>();
        collider.size = new Vector3(3.6f, 2.0f, 3.6f);
        collider.center = Vector3.up;
        collider.isTrigger = true;
    }
}
