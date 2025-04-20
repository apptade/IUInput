namespace SFInput {
public interface IInputPredicate<T>
{
    bool Result(T entry);
}}