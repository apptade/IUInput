namespace SFInput.Screen {
public abstract class MovementInputAdder : InputAdder<MovementInputController, MovementInputData>
{
    protected override void OnDestroy()
    {
        base.OnDestroy();
        this.ForEachController(c => c.Dispose());
    }
}}