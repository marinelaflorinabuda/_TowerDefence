using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputManager : MonoBehaviour
{

    public Camera mainCamera;

    public float rayCastDistance = 50f;

    public LayerMask layerMask;

    public EntitySpawnerAuthoring entitySpawner;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayCastDistance, layerMask))
            {
                Vector3 spawnPosition = hit.point;
                entitySpawner.CreateDefenderAt(spawnPosition);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayCastDistance, layerMask))
            {
                Vector3 spawnPosition = hit.point;
                entitySpawner.CreateProjectibleAt(spawnPosition);
            }

        }
    }
}
