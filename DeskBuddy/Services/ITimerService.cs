namespace DeskBuddy.Services;

public interface ITimerService
{
    void Restart();

    void OnApplicationExit();
}