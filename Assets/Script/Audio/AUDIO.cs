/// <summary>
/// オーディオ名を定数で管理するクラス
/// </summary>
public static class AUDIO
{
    public const string BGM_TITLE = "bgm_maoudamashii_orchestra23";
    public const string BGM_MAIN = "bgm_maoudamashii_orchestra21";
    public const string BGM_BOSS = "bgm_maoudamashii_orchestra24";
    public const string BGM_RESULT = "bgm_maoudamashii_piano34";

    public const string SE_SHOOT = "PlayerShoot";
    public const string SE_ENEMYHIT = "EnemyHit";
    public const string SE_BREAK = "";
    public const string SE_DECIDE = "GetItem";
}

public enum Bgm
{
    Title,
    Main,
    Boss,
    Result,
}
