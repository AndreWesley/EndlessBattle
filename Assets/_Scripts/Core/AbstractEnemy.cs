using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public abstract class AbstractEnemy : MonoBehaviour
{
    [SerializeField] private ScriptableCharacter _enemyData;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private GameObject _spriteRendererObject;
    [SerializeField] private ScriptableGameEvent _explosionEvent;
    [SerializeField] private ScriptableGameEvent _hitEvent;
    private static Transform _target;
    private int _hp;
    private float _speed;

    private EnemyState _currentState;

    private delegate void State();
    private State _state;
    private State _stateEnter;
    private State _stateExit;

    protected ScriptableCharacter EnemyData { get => _enemyData; }
    protected Rigidbody2D Rb { get => _rb; }
    protected Collider2D Collider { get => _collider; }
    protected GameObject SpriteRendererObject { get => _spriteRendererObject; }
    protected int Hp { get => _hp; set => _hp = value; }
    protected float Speed { get => _speed; set => _speed = value; }

    void OnEnable()
    {
        _currentState = EnemyState.Initial;
        _state = InitialState;
    }

    protected virtual void InitialState()
    {
        ResetValues();
        UpdateState(EnemyState.Chasing);
    }

    #region Attacking state
    protected abstract void AttackingEnter();
    protected abstract void Attacking();
    protected abstract void AttackingExit();
    #endregion

    #region Chasing state
    protected virtual void ChasingEnter() { }
    protected virtual void Chasing()
    {
        if (!CharacterController.GetTransform) return;
        
        Vector2 position = Vector2.MoveTowards(Rb.position, CharacterController.GetTransform.position, Speed * Time.deltaTime);
        Rb.MovePosition(position);

        float distance = Vector2.Distance(Rb.position, CharacterController.GetTransform.position);
        if (EnemyData.AttackRange < distance)
        {
            _state = Attacking;
        }
    }
    protected virtual void ChasingExit() { }
    #endregion

    #region Dying State

    protected virtual void DyingEnter()
    {
        _explosionEvent.Call();
    }
    protected virtual void Dying()
    {
        SpriteRendererObject.SetActive(false);
        Collider.enabled = false;
        Rb.isKinematic = true;
    }
    protected virtual void DyingExit() { }
    #endregion

    #region Special Move State
    protected virtual void SpecialMovementEnter() { }
    protected virtual void SpecialMove() { }
    protected virtual void SpecialMovementExit() { }
    #endregion

    private void ResetValues()
    {
        Hp = EnemyData.MaxHP;
        Speed = EnemyData.Speed;

        SpriteRendererObject.SetActive(true);
        Collider.enabled = true;
        Rb.isKinematic = true;
    }

    protected void Update()
    {
        _state.Invoke();
    }

    protected void UpdateState(EnemyState newState)
    {
        if (_currentState == newState)
        {
            Debug.LogWarning($"The enemy are already in the {_currentState} state", this);
            return;
        }

        bool willUpdateState = false;

        switch (_currentState)
        {
            case EnemyState.Initial:
                willUpdateState = newState == EnemyState.Chasing;
                break;
            case EnemyState.Chasing:
                willUpdateState = newState == EnemyState.Attacking;
                willUpdateState |= newState == EnemyState.Dying;
                willUpdateState |= newState == EnemyState.SpecialMovement && EnemyData.HasSpecialMove;
                break;
            case EnemyState.Attacking:
                willUpdateState = newState == EnemyState.Chasing;
                willUpdateState |= newState == EnemyState.Dying;
                willUpdateState |= newState == EnemyState.SpecialMovement && EnemyData.HasSpecialMove;
                break;
            case EnemyState.SpecialMovement:
                willUpdateState = newState == EnemyState.Chasing;
                willUpdateState |= newState == EnemyState.Attacking;
                willUpdateState |= newState == EnemyState.Dying;
                break;
            case EnemyState.Dying:
                willUpdateState = newState == EnemyState.Initial;
                break;
        }

        if (willUpdateState)
        {
            SetState(newState);
            return;
        }

        Debug.Log($"You are trying to change your current state ({_currentState}) to a invalid one ({newState})");
    }

    private void SetState(EnemyState newState)
    {
        _stateExit?.Invoke();
        _currentState = newState;

        switch (newState)
        {
            case EnemyState.Initial:
                _stateEnter = null;
                _state = InitialState;
                _stateExit = null;
                break;
            case EnemyState.Chasing:
                _stateEnter = ChasingEnter;
                _state = Chasing;
                _stateExit = ChasingExit;
                break;
            case EnemyState.Attacking:
                _stateEnter = AttackingEnter;
                _state = Attacking;
                _stateExit = AttackingExit;
                break;
            case EnemyState.SpecialMovement:
                _stateEnter = SpecialMovementEnter;
                _state = SpecialMove;
                _stateExit = SpecialMovementExit; 
                break;
            case EnemyState.Dying:
                _stateEnter = DyingEnter;
                _state = Dying;
                _stateExit = DyingExit;
                break;
        }

        _stateEnter?.Invoke();
    }

    public void Hit(int damage)
    {
        _hitEvent.Call();

        Hp -= damage;
        if (Hp <= 0)
        {
            UpdateState(EnemyState.Dying);
        }
    }

    public enum EnemyState
    {
        Initial,
        Chasing,
        Attacking,
        SpecialMovement,
        Dying
    }
}
