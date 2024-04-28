using DG.Tweening;
using UnityEngine;

public class dtwen : MonoBehaviour
{
    public Transform[] waypoints1;
    public Vector3[] waypoints;
    [SerializeField] private Ease Ease;

    [SerializeField] Cinemachine.CinemachineVirtualCamera vcam1;

    public void SwapCams()
    {
        vcam1.Priority = 101;

    }
    public void PlayCutscene()
    {

        waypoints = new Vector3[waypoints1.Length];
        
        for (int i = 0; i < waypoints1.Length; i++)
        {
            waypoints[i] = waypoints1[i].position;
        }

        transform.DOPath(waypoints, 7f, PathType.CatmullRom)
            .SetLookAt(0.01f, Vector3.forward, Vector3.up)
            .SetEase(Ease);
    }
}