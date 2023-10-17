using System.Collections;
using UnityEngine;

public class MeleeEnemy : AbstractEnemy
{
    [SerializeField] private float _dashPower = 5f;
    [SerializeField] private float _chargingTime = 1f;
    [SerializeField] private float _retrySpecialMoveTime = 1f;
    [SerializeField] private GameObject _explosion;
    [SerializeField] private GameObject _coin;
    private bool isAttacking;

    protected override void InitialState()
    {
        base.InitialState();
        isAttacking = false;
    }

    protected override void Attacking()
    {
        base.Chasing();
    }

    protected override void AttackingEnter()
    {
        isAttacking = true;

        StartCoroutine(CheckSpecialAttack());
        
    }

    protected override void AttackingExit()
    {
        isAttacking = false;
        StopAllCoroutines();
    }

    private IEnumerator CheckSpecialAttack()
    {
        WaitForSeconds specialMoveWaitingTime = new WaitForSeconds(_retrySpecialMoveTime);

        while (isAttacking)
        {
            yield return specialMoveWaitingTime;

            bool activeSpecialMove = EnemyData.HasSpecialMove && Random.Range(0f, 1f) < EnemyData.SpecialMoveChance;
            if (activeSpecialMove)
            {
                UpdateState(EnemyState.SpecialMovement);
            }
        }
    }

    protected override void SpecialMovementEnter()
    {
        StartCoroutine(SpecialMoveCoroutine());
    }

    protected override void SpecialMovementExit()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpecialMoveCoroutine()
    {
        WaitForSeconds chargingTime = new WaitForSeconds(_chargingTime);

        yield return chargingTime;
        Vector2 dash = ((Vector2) CharacterController.GetTransform.position - Rb.position) * _dashPower;
        Rb.velocity = dash;

        yield return chargingTime;
        UpdateState(EnemyState.Chasing);
    }

    protected override void DyingEnter()
    {
        base.DyingEnter();
        _explosion.transform.parent = null;
        _explosion.SetActive(true);
        Instantiate(_coin, transform.position, Quaternion.identity);
    }
}
