namespace Day07;


public class DirectoryAoc
{
    private ulong? _size;

    public string Name { get; }

    public DirectoryAoc? Parent { get; set; }

    public List<DirectoryAoc> Directories { get; } = new();
    public List<FileAoc> Files { get; } = new();

    public ulong Size => this.GetSize();



    public DirectoryAoc(string name)
    {
        this.Name = name;
    }



    private ulong GetSize()
    {
        this._size ??= this.CalculateSize();
        return this._size.Value;
    }

    private ulong CalculateSize()
        => this.Directories.Aggregate(0ul, (acc, x) => acc + x.Size) + this.Files.Aggregate(0ul, (acc, x) => acc + x.Size);

    public DirectoryAoc? GetSubdirectory(ReadOnlySpan<char> name)
    {
        if (this.Name == name)
        {
            return this;
        }

        foreach (var directory in this.Directories)
        {
            return directory.GetSubdirectory(name);
        }

        return null;
    }

    public DirectoryAoc AddSubdirectory(ReadOnlySpan<char> name)
    {
        // We can't use ReadOnlySpan in lambda function, therefore we cannot use LINQ.
        foreach (var directory in this.Directories)
        {
            if (directory.Name == name)
            {
                return directory;
            }
        }

        var newDirectory = new DirectoryAoc(name.ToString())
        {
            Parent = this
        };

        this.Directories.Add(newDirectory);

        return newDirectory;
    }

    public FileAoc AddFile(ReadOnlySpan<char> name, ulong size)
    {
        // We can't use ReadOnlySpan in lambda function, therefore we cannot use LINQ.
        foreach (var file in this.Files)
        {
            if (file.Name == name)
            {
                return file;
            }
        }

        var newFile = new FileAoc(name.ToString(), size);
        this.Files.Add(newFile);
        return newFile;
    }
}


public class FileAoc
{
    public string Name { get; }
    public ulong Size { get; }



    public FileAoc(string name, ulong size)
    {
        this.Name = name;
        this.Size = size;
    }
}
