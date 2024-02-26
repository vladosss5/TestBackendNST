namespace Core.Helpers;

public class Decode
{
    private string _connectionString;

    public string ConnectionString
    {
        get => this._connectionString;
    }

    public Decode(string connectionString)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(connectionString);
        this._connectionString = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }
}