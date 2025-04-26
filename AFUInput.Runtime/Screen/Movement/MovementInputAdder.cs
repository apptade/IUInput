namespace SFInput.Screen {
public abstract class MovementInputAdder : InputAdder<MovementInputController, MovementInputData>
{
    protected override void OnDestroy()
    {
        foreach (var controller in Controllers.Values) controller.Dispose();
        base.OnDestroy();
    }
}}