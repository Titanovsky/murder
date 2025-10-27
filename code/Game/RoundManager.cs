using Sandbox;

public sealed class RoundManager : Component
{
    public static RoundManager Instance { get; private set; }

    [Property, DefaultValue(4f)] public float TimeFinish = 4f;
    [Property, DefaultValue(6f)] public float Delay = 6f;
    
    [Header("Stats")]
    [Property, ReadOnly] public int Current { get; private set; } = 0;
    [Property, ReadOnly] public float CurrentTime { get; private set; } = 0f;
    [Property, ReadOnly] public RoundStatus CurrentStatus { get; private set; } = RoundStatus.None;

    public void StartRound()
    {
        if (CurrentStatus == RoundStatus.Active) return;

        Current += 1;
        CurrentStatus = RoundStatus.Active;
        CurrentTime = 0f;

        Log.Info("[Round] Start");
    }

    public void FinishRound()
    {
        if (CurrentStatus == RoundStatus.None) return;
        if (CurrentStatus == RoundStatus.Finished)
        {
            StartRound();

            return;
        }

        CurrentStatus = RoundStatus.Finished;
        CurrentTime = 0f;

        Log.Info("[Round] Finish");
    }

    private void CreateSingleton()
    {
        if (Instance == null)
            Instance = this;
    }

    private void DestroySingleton()
    {
        if (Instance != null)
            Instance = null;
    }

    private void TimeUpdate()
    {
        if (CurrentStatus == RoundStatus.None) return;

        CurrentTime += Time.Delta;

        if ((CurrentStatus == RoundStatus.Finished && CurrentTime >= Delay) || (CurrentStatus == RoundStatus.Active && CurrentTime >= TimeFinish))
            FinishRound();
    }

    protected override void OnAwake()
    {
        CreateSingleton();
    }

    protected override void OnStart()
    {
        StartRound();
    }

    protected override void OnFixedUpdate()
    {
        TimeUpdate();
    }

    protected override void OnDestroy()
    {
        DestroySingleton();
    }
}
