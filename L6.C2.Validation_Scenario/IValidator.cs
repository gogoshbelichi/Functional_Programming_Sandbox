namespace L6.C2.Validation_Scenario;

public interface IValidator<T> 
{
    bool IsValid(T t);
}