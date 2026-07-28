namespace App.BL.Settings;

public class MinioSettings
{
    public string Endpoint { get; set; } = string.Empty;
    public string PublicEndpoint { get; set; } = string.Empty;
    public string AccessKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public string BucketName { get; set; } = "ask-media";
    public bool UseSsl { get; set; }
}
