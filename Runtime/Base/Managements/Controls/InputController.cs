namespace IUInput {
public abstract class InputController : IInputController
{
    private bool _enabled;

    public bool Enabled { get => _enabled; }
    public IPredicateManager PredicateManager { get; }

    public InputController()
    {
        PredicateManager = new PredicateManager();
    }

    public void Disable()
    {
        if (!_enabled) return;

        _enabled = false;
        OnDisable();
    }

    public void Enable()
    {
        if (_enabled) return;

        _enabled = true;
        OnEnable();
    }

    protected abstract void OnDisable();
    protected abstract void OnEnable();
}}