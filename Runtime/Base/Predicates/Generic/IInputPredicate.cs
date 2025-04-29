namespace AFUInput {
public interface IInputPredicate<T>
{
    bool Result(T entry);
}}