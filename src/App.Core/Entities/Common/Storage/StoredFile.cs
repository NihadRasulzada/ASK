namespace App.Core.Entities.Common.Storage;

public class StoredFile
{
    public string ObjectKey { get; private set; }

    public StoredFile(string objectKey)
    {
        if (string.IsNullOrWhiteSpace(objectKey))
            throw new ArgumentException("Object key cannot be null or empty.", nameof(objectKey));
        ObjectKey = objectKey;
    }

    private StoredFile() { ObjectKey = string.Empty; }

    public static bool IsNullOrEmpty(StoredFile? file)
        => file is null || string.IsNullOrWhiteSpace(file.ObjectKey);
}
