// -----------------------------------------------------------------------
//  <copyright file="AbstractFileReadTrigger.cs" company="Hibernating Rhinos LTD">
//      Copyright (c) Hibernating Rhinos LTD. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------
using System.ComponentModel.Composition;

using Raven.Database.Plugins;
using Raven.Json.Linq;

namespace Raven.Database.FileSystem.Plugins
{
    [InheritedExport]
    public abstract class AbstractFileReadTrigger : IRequiresFileSystemInitialization
    {
        public RavenFileSystem FileSystem { get; private set; }

        public void Initialize(RavenFileSystem fileSystem)
        {
            FileSystem = fileSystem;
            Initialize();
        }

        public virtual void SecondStageInit()
        {
        }

        public virtual void Initialize()
        {
        }

        public virtual ReadVetoResult AllowRead(string name, RavenJObject metadata, ReadOperation operation)
        {
            return ReadVetoResult.Allowed;
        }
    }
}
