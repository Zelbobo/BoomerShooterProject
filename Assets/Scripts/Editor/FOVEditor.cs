using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyController))]
public class FOVEditor : Editor
{
    private void OnSceneGUI()
    {
        EnemyController controller = (EnemyController)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(controller.transform.position, Vector3.up, Vector3.forward, 360, controller.GetRaduis);

        Vector3 viewAnge01 = DirectionFromAngle(controller.transform.eulerAngles.y, -controller.GetFOVAnge / 2);
        Vector3 viewAnge02 = DirectionFromAngle(controller.transform.eulerAngles.y, controller.GetFOVAnge / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(controller.transform.position, controller.transform.position + viewAnge01 * controller.GetRaduis);
        Handles.DrawLine(controller.transform.position, controller.transform.position + viewAnge02 * controller.GetRaduis);
    
        if (controller.GetCurrentPlayer != null)
        {
            Handles.color = Color.green;
            Handles.DrawLine(controller.transform.position, controller.GetCurrentPlayer.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
