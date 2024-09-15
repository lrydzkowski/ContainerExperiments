using ContainerExperiments.WebApi1.Options;
using Microsoft.Extensions.Options;

namespace ContainerExperiments.WebApi1.Services;

public interface IFileService
{
    Task SaveFileAsync(string content, CancellationToken cancellationToken);
}

internal class FileService
    : IFileService
{
    private readonly VolumeMountOptions _options;

    public FileService(IOptions<VolumeMountOptions> options)
    {
        _options = options.Value;
    }

    public async Task SaveFileAsync(string content, CancellationToken cancellationToken)
    {
        string dirPath = _options.DirPath;
        string fileName = "test.txt";
        string path = Path.Combine(dirPath, fileName);
        await File.WriteAllTextAsync(path, content, cancellationToken);
    }
}
