using UnityEngine;
using UnityEngine.AI;

public class MoveState_Melee : EnemyState
{
    private Enemy_Melee enemy;
    private Vector3 destination;

    public MoveState_Melee(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        enemy = enemyBase as Enemy_Melee;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.agent.speed = enemy.moveSpeed;

        destination = enemy.GetPatrolDestination();
        enemy.agent.SetDestination(destination);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(enemy.PlayerInAggressionRange())
        {
            stateMachine.ChangeState(enemy.recoveryState);
            return;
        }

        // if aggressive
        // then run towards player
        // else
        // move to patrol point

        enemy.transform.rotation = enemy.FaceTarget(GetNextPathPoint());

        if (enemy.agent.remainingDistance <= enemy.agent.stoppingDistance + .05f)
            stateMachine.ChangeState(enemy.idleState);
    }
}
