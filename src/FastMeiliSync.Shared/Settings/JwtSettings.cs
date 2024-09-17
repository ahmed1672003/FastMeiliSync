namespace FastMeiliSync.Shared.Settings;

public class JwtSettings
{
    public static string Secret =>
        "I4Uv6E3+xK4pt07YHpN4UX8xLpWghBPPr6VLPBxYIJY=xK4pt07YHpN4UX8xLpWghBPPr6VLPBxYIJY=xK4pt07YHpN4UX8xLpWghBPPr6VLPBxYIJY";
    public static string Issuer => "meili sync";
    public static string Audience => "meili search users";
    public static bool ValidateIssuer => true;
    public static bool ValidateAudience => true;
    public static bool ValidateLifeTime => true;
    public static bool ValidateIssuerSigningKey => true;
    public static int AccessTokenExpireDate => 1;
    public static int RefreshTokenExpireDate => 2;
}
