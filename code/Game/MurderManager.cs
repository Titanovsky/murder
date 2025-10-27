using Sandbox;

public sealed class MurderManager : Component
{
    public static MurderManager Instance { get; private set; }

    public void StartGame()
	{

	}

	public void FinishGame()
	{

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

    protected override void OnAwake()
    {
        CreateSingleton();
    }

    protected override void OnStart()
    {
        //StartRound();
    }

    protected override void OnUpdate()
    {
        //TimeUpdate();
    }

    protected override void OnDestroy()
    {
        DestroySingleton();
    }
}
