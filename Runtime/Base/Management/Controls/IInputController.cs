namespace IUInput {
public interface IInputController
{
    bool Enabled { get; }
    IInputPredicateManager PredicateManager { get; }

    bool Disable();
    bool Enable();
}}