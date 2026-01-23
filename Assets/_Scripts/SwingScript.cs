using UnityEngine;

public class SwingScript : MonoBehaviour
{
    [Header("Anchor")]
    [SerializeField] private Transform anchor;

    [Header("Swing Settings")]
    [Tooltip("Maximum swing angle in degrees")]
    [SerializeField] private float maxAngle = 45f;

    [Tooltip("Swing speed (cycles per second)")]
    [SerializeField] private float swingSpeed = 1f;

    [Tooltip("Start offset in degrees")]
    [SerializeField] private float phaseOffset = 0f;

    [Header("Gizmo")]
    [SerializeField] private Color gizmoColor = Color.cyan;
    [SerializeField] private int gizmoSteps = 32;

    private Vector3 initialLocalOffset;
    private float time;

    private void Awake()
    {
        if (anchor == null)
        {
            Debug.LogError($"{name} SwingScript: Anchor is not assigned.");
            enabled = false;
            return;
        }

        initialLocalOffset = transform.position - anchor.position;
    }

    private void Update()
    {
        time += Time.deltaTime;

        float angle = Mathf.Sin(time * swingSpeed * Mathf.PI * 2f) * maxAngle + phaseOffset;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.position = anchor.position + rotation * initialLocalOffset;
    }

    private void OnDrawGizmosSelected()
    {
        if (anchor == null)
            return;

        Gizmos.color = gizmoColor;

        Vector3 dir = Application.isPlaying
            ? initialLocalOffset
            : transform.position - anchor.position;

        float step = (maxAngle * 2f) / gizmoSteps;

        for (int i = 0; i < gizmoSteps; i++)
        {
            float a1 = -maxAngle + step * i;
            float a2 = -maxAngle + step * (i + 1);

            Vector3 p1 = anchor.position + Quaternion.AngleAxis(a1, Vector3.forward) * dir;
            Vector3 p2 = anchor.position + Quaternion.AngleAxis(a2, Vector3.forward) * dir;

            Gizmos.DrawLine(p1, p2);
        }

        // Draw center line
        Gizmos.DrawLine(anchor.position, anchor.position + dir);
    }
}
