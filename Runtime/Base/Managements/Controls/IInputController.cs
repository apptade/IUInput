namespace IUInput {
public interface IInputController
{
    bool Enabled { get; }
    IPredicateManager PredicateManager { get; }

    void Disable();
    void Enable();
}}