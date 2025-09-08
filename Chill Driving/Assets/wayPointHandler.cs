using System;
using UnityEngine;

public class wayPointHandler : MonoBehaviour
{
    [SerializeField] public Transform[] wayPoints;
    public bool uwu;
    void Start()
    {
        wayPoints = new Transform[transform.childCount];
        for (int i = 0; i < wayPoints.Length; i++)
            wayPoints[i] = transform.GetChild(i);
    }
    
    void Update()
    {
        foreach (Transform wayPoint in wayPoints)
        {
            uwu = Physics.BoxCast(wayPoint.position, wayPoint.forward, Vector3.down, out RaycastHit hit, Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Debug.DrawLine(wayPoints[i].position, wayPoints[i+1].position, Color.red);
        }

    }
}
