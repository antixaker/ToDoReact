using System;

namespace ToDoReact.Services
{
    public interface ITimeProvider
    {
        DateTime CurrentTime { get; }
    }
}

