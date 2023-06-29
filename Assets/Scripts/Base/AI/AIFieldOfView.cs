using UnityEngine;

public class AIFieldOfView : MonoBehaviour
{
    [SerializeField] private EnemyController aiController;

    private void Start()
    {
        if (aiController == null)
        {
            aiController = GetComponent<EnemyController>();

            if (aiController == null)
            {
                aiController = GetComponentInParent<EnemyController>();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerStats player))
        {
            aiController.OnPlayerEnter(player);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerStats player))
        {
            aiController.OnPlayerExit(player);
        }
    }
}
