
using DG.Tweening;
using UnityEngine;

public class MovementUtilities
{
    private Transform transform;
    // private ColliderDetection colliderDetection;
    public CharacterController controller;
    public MovementUtilities(Transform transform, CharacterController controller)
    {
        this.transform = transform;
        this.controller = controller;
    }


    public void DoMove(Vector3 move_direction, float speed, bool rotate_on_move = true)
    {
        Vector3 look_direction = transform.position - CameraCaching.Instance.mainCamera.transform.position;
        look_direction = MyUtils.ModifyVector(look_direction, y : 0);
        Quaternion t = Quaternion.LookRotation(look_direction);

        Vector3 move_orientation = t * move_direction;

        move_orientation.Normalize();
        if (rotate_on_move)
            RotateTowardDirection(move_orientation);
    }

    public void DoSimpleMove(Vector3 move_direction, float speed, bool rotate_on_move = true)
    {
        if (rotate_on_move)
            RotateTowardDirection(move_direction);
        controller.Move(move_direction * speed * Time.fixedDeltaTime);
    }


    public void RotateTowardDirection(Vector3 direction)
    {

        transform.DOLookAt(direction + transform.position, 0.2f, up: Vector3.up);
    }

    public void RotateTowardTarget(Vector3 target)
    {
        transform.DOLookAt(new Vector3(target.x, 0, target.z), 0.1f, up: Vector3.up);
    }

    public void DoJump(float jumpPulse)
    {
        DoMove(Vector3.up, jumpPulse);
    }

    public void Gravity(float gravity)
    {
        controller.Move(Vector3.down * gravity * Time.fixedDeltaTime);
    }
}