namespace IUInput {
public interface IInputPredicate<T>
{
    bool Result(T entry);
}}