﻿using System;
using System.Collections.Generic;
using ToDoReact.Models;

namespace ToDoReact.Services
{
    public interface ITODOService
    {
        List<TODOModel> GetAll();
    }
}

