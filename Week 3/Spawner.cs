using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // This transform will hold all of the spawned objects, so that they don't flood the Hierarchy
    [SerializeField] Transform container;

    [SerializeField] GameObject kirbyPrefab;
    [SerializeField] GameObject marioPrefab;
    [SerializeField] GameObject linkPrefab;

    Camera mainCam;

    void Start() => mainCam = Camera.main;

    void Update()
    {
        // Input.mousePosition returns a Vector in pixel coordinates. Bottom left is (0, 0) and top right
        // is (Screen.width, Screen.height). This converts that value to World Units
        var spawnPos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        // As the function is called on the camera, its z value will be -10. Set this to 0 instead, because
        // we won't see an object that is on the same depth as the camera
        spawnPos.z = 0;
   
        // Mouse buttons work exactly like GetKey, but with an int instead of a KeyCode
        if (Input.GetMouseButton(0))
            Instantiate(kirbyPrefab, spawnPos, Quaternion.identity, container);
        if (Input.GetMouseButton(1))
            Instantiate(marioPrefab, spawnPos, Quaternion.identity, container);
        if (Input.GetMouseButton(2))
            Instantiate(linkPrefab, spawnPos, Quaternion.identity, container);
    }
}
