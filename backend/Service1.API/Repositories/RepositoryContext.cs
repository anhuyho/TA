using System.Text.Json;
using Microsoft.Extensions.Options;
using Service1.API.Models;
using Service1.API.Repositories.Contracts;

namespace Service1.API.Repositories;

public class RepositoryContext : IRepositoryContext
{
    private readonly string? _folder;
    private readonly Dictionary<string, object?> _stores;
    private string? _fileName;
    private object? _store;

    public RepositoryContext(IOptions<DataStore> configuration)
    {
        _stores = new Dictionary<string, object?>();
        var dataStore = configuration.Value;
        _folder = dataStore?.FolderPath;
    }

    public List<T>? Set<T>() where T : class
    {
        _fileName = $"{_folder}{typeof(T).Name}Store.json";
        if (File.Exists(_fileName))
        {
            var json = File.ReadAllText(_fileName);
            _store = JsonSerializer.Deserialize<List<T>>(json);
        }
        else
        {
            _store = new List<T>();
        }

        _stores.Add(_fileName, _store);

        return _store as List<T>;
    }

    public void SaveChanges()
    {
        foreach (var store in _stores)
        {
            var fileName = store.Key;
            var s = store.Value;

            var json = JsonSerializer.Serialize(_store);

            if (_fileName != null) File.WriteAllText(_fileName, json);
        }
    }
}