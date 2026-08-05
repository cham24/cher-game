using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Click-to-move controller for a 2D point-and-click character.
/// Left-click anywhere in the scene and the character walks toward it.
/// Attach to the player GameObject (needs a SpriteRenderer to actually be visible).
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float stopDistance = 0.05f;

    private Vector3 targetPosition;
    private bool isMoving;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 screenPos = Mouse.current.position.ReadValue();
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(screenPos);
            worldPos.z = transform.position.z; // keep the character's own depth
            targetPosition = worldPos;
            isMoving = true;
        }

        if (!isMoving)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) <= stopDistance)
        {
            isMoving = false;
        }
    }
}
