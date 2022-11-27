namespace BildMlue.Application.Interfaces;

public interface IAedImporter
{
    Task Import(Stream json);
}