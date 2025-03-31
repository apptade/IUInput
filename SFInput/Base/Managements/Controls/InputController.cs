namespace SFInput {
public abstract class InputController : IInputController
{
    private bool _enabled;
    private readonly IInputPredicateManager _predicateManager;

    public bool Enabled { get => _enabled; }
    public IInputPredicateManager PredicateManager { get => _predicateManager; }

    public InputController()
    {
        _predicateManager = new InputPredicateManager();
    }

    public void Enable()
    {
        if (_enabled) return;

        _enabled = true;
        OnEnable();
    }

    public void Disable()
    {
        if (!_enabled) return;

        _enabled = false;
        OnDisable();
    }

    protected abstract void OnEnable();
    protected abstract void OnDisable();
}}