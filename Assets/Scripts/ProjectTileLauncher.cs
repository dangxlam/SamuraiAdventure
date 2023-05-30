using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTileLauncher : MonoBehaviour
{
    public Transform launcherPoint;
    public GameObject projectTilePrefab;

    public void ArrowProjectTile()
    {
        GameObject projectTile = Instantiate(projectTilePrefab, launcherPoint.position, projectTilePrefab.transform.rotation);

        Vector3 oriScale = projectTile.transform.localScale;

        //Flip projectTile direction
        projectTile.transform.localScale = new Vector3(
            oriScale.x * transform.localScale.x > 0 ? 1 : -1 ,
            oriScale.y * transform.localScale.x > 0 ? 1 : -1,
            oriScale.z);

    }
}
