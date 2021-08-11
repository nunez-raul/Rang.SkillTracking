﻿using Rang.SkillTracking.Application.Boundary.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rang.SkillTracking.Application.Common
{
    public abstract class BaseUseCase
    {
        // fields

        // properties

        // constructors

        // methods
    }

    public abstract class StorageDependentUseCase : BaseUseCase
    {
        // fields
        protected IStoragePort _storageAdapter;

        // properties

        // constructors
        public StorageDependentUseCase(IStoragePort storageAdapter)
        {
            _storageAdapter = storageAdapter ?? throw new ArgumentNullException(nameof(storageAdapter));
        }

        // methods
    }
}
