namespace IUInput.Screen {
public abstract class MovementInputAdder : InputAdder<MovementInputController, MovementInputData>
{
    protected override void OnDestroy()
    {
        base.OnDestroy();
        foreach (var controller in Controllers.Values) controller.Dispose();
    }
}}