namespace IUInput {
public abstract class InputController : IInputController
{
    public bool Enabled { get; private set; }
    public IInputPredicateManager PredicateManager { get; }

    public InputController()
    {
        PredicateManager = new InputPredicateManager();
    }

    public bool Disable()
    {
        if (!Enabled) return false;

        Enabled = false;
        OnDisable();
        return true;
    }

    public bool Enable()
    {
        if (Enabled) return false;

        Enabled = true;
        OnEnable();
        return true;
    }

    protected abstract void OnDisable();
    protected abstract void OnEnable();
}}