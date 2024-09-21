using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEntity {

    public Rigidbody rb;
    public float speed;

    public CharacterMovementMode currentMode;
    public Vector3 position;
    public Vector3 forward;
    public Quaternion rotation;
    public Vector3 targetPosition;
    public float moveSpeed = 5f;
    public float turnSpeed = 10f;
    public bool isGrounded = true;
    public Vector2 movementInput;

    Vector3 velocity;
    Vector3 moveDirection;

    void Tick(float dt) {
        // 检测当前模式，并执行对应的移动逻辑
        switch (currentMode) {
            case CharacterMovementMode.Axial:
                HandleAxialMovement();
                break;

            case CharacterMovementMode.Targeted:
                HandleTargetedMovement();
                break;
        }

        // 更新角色位置
        position += velocity * dt;
    }

    #region 轴向移动
    void HandleAxialMovement() {
        // 轴向移动根据输入来改变移动方向
        moveDirection = new Vector3(movementInput.x, 0, movementInput.y);

        // 将角色的前进方向应用到输入上
        moveDirection = TransformUtil.TransformDirection(rotation, moveDirection);
        moveDirection.Normalize();

        // 根据速度调整移动
        velocity = moveDirection * moveSpeed;

        // 如果有输入移动方向，更新角色的旋转朝向
        if (moveDirection != Vector3.zero) {
            var r = Quaternion.LookRotation(moveDirection);
            rotation = Quaternion.Slerp(r, rotation, Time.deltaTime * turnSpeed);
        }
    }
    #endregion

    #region 靶向移动
    void HandleTargetedMovement() {
        // 计算朝向目标的方向
        Vector3 directionToTarget = (targetPosition - position).normalized;

        // 计算角色的旋转并平滑转向目标
        if (directionToTarget != Vector3.zero) {
            var r = Quaternion.LookRotation(directionToTarget);
            rotation = Quaternion.Slerp(r, rotation, Time.deltaTime * turnSpeed);
        }

        // 移动角色朝向目标
        velocity = directionToTarget * moveSpeed;

        // 如果角色接近目标位置，停止移动
        if (Vector3.Distance(position, targetPosition) < 0.1f) {
            velocity = Vector3.zero; // 停止
        }
    }
    #endregion

    // 切换到轴向移动模式
    public void SwitchToAxialMovement() {
        currentMode = CharacterMovementMode.Axial;
    }

    // 切换到靶向移动模式
    public void SwitchToTargetedMovement(Vector3 newTargetPosition) {
        currentMode = CharacterMovementMode.Targeted;
        targetPosition = newTargetPosition;
    }

    // 接收输入 (例如从手柄或键盘)
    public void SetMovementInput(Vector2 input) {
        movementInput = input;
    }

}