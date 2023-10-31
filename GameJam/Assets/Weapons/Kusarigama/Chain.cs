using UnityEngine;

public class Chain : MonoBehaviour
{
    private EdgeCollider2D edgeCollider;

    private void Start()
    {
        
        edgeCollider = gameObject.AddComponent<EdgeCollider2D>();

        Vector2[] points = new Vector2[2];
        float width = transform.localScale.x; 
        float height = transform.localScale.y; 

        points[0] = new Vector2(-width / 2f, height / 2f);
        points[1] = new Vector2(width / 2f, height / 2f);

        
        edgeCollider.points = points;
    }


    private void OnDrawGizmosSelected()
    {
        Vector2[] points = new Vector2[2];
        float width = transform.localScale.x;
        float height = transform.localScale.y; 
        points[0] = transform.position + new Vector3(-width / 2f, height / 2f);
        points[1] = transform.position + new Vector3(width / 2f, height / 2f);

        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(points[0], points[1]);
    }
}

