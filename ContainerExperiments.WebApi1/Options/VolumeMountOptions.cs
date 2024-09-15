namespace ContainerExperiments.WebApi1.Options;

public class VolumeMountOptions
{
    public const string Position = "VolumeMount";

    public string DirPath { get; init; } = "";
}
