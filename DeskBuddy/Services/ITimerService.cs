namespace DeskBuddy.Services;

public interface ITimerService
{
    void Start();

    void OnApplicationExit();
}