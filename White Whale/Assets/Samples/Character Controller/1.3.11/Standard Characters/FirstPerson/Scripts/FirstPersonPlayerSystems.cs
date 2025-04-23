using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif
using Unity.CharacterController;

[UpdateInGroup(typeof(SimulationSystemGroup), OrderFirst = true)]
[UpdateBefore(typeof(FixedStepSimulationSystemGroup))]
public partial class FirstPersonPlayerInputsSystem : SystemBase
{
    protected override void OnCreate()
    {
        RequireForUpdate<FixedTickSystem.Singleton>();
        RequireForUpdate(SystemAPI.QueryBuilder().WithAll<FirstPersonPlayer, FirstPersonPlayerInputs>().Build());
    }

    protected override void OnUpdate()
    {
        uint tick = SystemAPI.GetSingleton<FixedTickSystem.Singleton>().Tick;

#if ENABLE_INPUT_SYSTEM
        foreach (var (playerInputs, player) in SystemAPI.Query<RefRW<FirstPersonPlayerInputs>, FirstPersonPlayer>())
        {
            playerInputs.ValueRW.MoveInput = new float2
            {
                x = (Keyboard.current.dKey.isPressed ? 1f : 0f) + (Keyboard.current.aKey.isPressed ? -1f : 0f),
                y = (Keyboard.current.wKey.isPressed ? 1f : 0f) + (Keyboard.current.sKey.isPressed ? -1f : 0f),
            };

            playerInputs.ValueRW.LookInput = Mouse.current.delta.ReadValue() * player.LookInputSensitivity;

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                playerInputs.ValueRW.JumpPressed.Set(tick);
            }
        }
#endif
    }
}

/// <summary>
/// Apply inputs that need to be read at a variable rate
/// </summary>
[UpdateInGroup(typeof(SimulationSystemGroup))]
[UpdateAfter(typeof(FixedStepSimulationSystemGroup))]
[BurstCompile]
public partial struct FirstPersonPlayerVariableStepControlSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate(SystemAPI.QueryBuilder().WithAll<FirstPersonPlayer, FirstPersonPlayerInputs>().Build());
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (playerInputs, player) in SystemAPI.Query<FirstPersonPlayerInputs, FirstPersonPlayer>().WithAll<Simulate>())
        {
            if (SystemAPI.HasComponent<FirstPersonCharacterControl>(player.ControlledCharacter))
            {
                FirstPersonCharacterControl characterControl = SystemAPI.GetComponent<FirstPersonCharacterControl>(player.ControlledCharacter);

                characterControl.LookDegreesDelta = playerInputs.LookInput;

                SystemAPI.SetComponent(player.ControlledCharacter, characterControl);
            }
        }
    }
}

/// <summary>
/// Apply inputs that need to be read at a fixed rate.
/// It is necessary to handle this as part of the fixed step group, in case your framerate is lower than the fixed step rate.
/// </summary>
[UpdateInGroup(typeof(FixedStepSimulationSystemGroup), OrderFirst = true)]
[BurstCompile]
public partial struct FirstPersonPlayerFixedStepControlSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<FixedTickSystem.Singleton>();
        state.RequireForUpdate(SystemAPI.QueryBuilder().WithAll<FirstPersonPlayer, FirstPersonPlayerInputs>().Build());
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        uint tick = SystemAPI.GetSingleton<FixedTickSystem.Singleton>().Tick;

        foreach (var (playerInputs, player) in SystemAPI.Query<FirstPersonPlayerInputs, FirstPersonPlayer>().WithAll<Simulate>())
        {
            if (SystemAPI.HasComponent<FirstPersonCharacterControl>(player.ControlledCharacter))
            {
                FirstPersonCharacterControl characterControl = SystemAPI.GetComponent<FirstPersonCharacterControl>(player.ControlledCharacter);

                quaternion characterRotation = SystemAPI.GetComponent<LocalTransform>(player.ControlledCharacter).Rotation;

                // Move
                float3 characterForward = MathUtilities.GetForwardFromRotation(characterRotation);
                float3 characterRight = MathUtilities.GetRightFromRotation(characterRotation);
                characterControl.MoveVector = (playerInputs.MoveInput.y * characterForward) + (playerInputs.MoveInput.x * characterRight);
                characterControl.MoveVector = MathUtilities.ClampToMaxLength(characterControl.MoveVector, 1f);

                // Jump
                characterControl.Jump = playerInputs.JumpPressed.IsSet(tick);

                SystemAPI.SetComponent(player.ControlledCharacter, characterControl);
            }
        }
    }
}