namespace RendrModeDemo.Client;


public record AppState(Guid Id, bool IsCheckedOut, string Name, int CounterCount)
{
    public AppState() : this(Guid.NewGuid(), false, "Default", 0)
    {

    }


}
