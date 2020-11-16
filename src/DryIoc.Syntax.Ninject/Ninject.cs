// -------------------------------------------------------------------------------------------------
// <copyright file="DisposableObject.cs" company="Ninject Project Contributors">
//   Copyright (c) 2007-2010 Enkari, Ltd. All rights reserved.
//   Copyright (c) 2010-2020 Ninject Project Contributors. All rights reserved.
//
//   Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
//   You may not use this file except in compliance with one of the Licenses.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//   or
//       http://www.microsoft.com/opensource/licenses.mspx
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
// -------------------------------------------------------------------------------------------------



namespace Ninject.Infrastructure
{
    /// <summary>Indicates that the object has a reference to an <see cref="IKernel"/>.</summary>
    public interface IHaveKernel
    {
        /// <summary>Gets the kernel.</summary>
        IKernel Kernel { get; }
    }
}

namespace Ninject 
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using Ninject.Modules;
    using Ninject.Planning.Bindings;
    using Ninject.Syntax;

    /// <summary>Provides the access to the everything</summary>
    public interface IKernel : IKernelConfiguration, IReadOnlyKernel {}

    /// <summary>A kernel that is used to resolve instances and has a configuration that can't be changed anymore.</summary>
    public interface IReadOnlyKernel : IResolutionRoot, IServiceProvider
    {
        /// <summary>Gets the bindings registered for the specified service.</summary>
        IBinding[] GetBindings(Type service);
    }

    public interface IKernelConfiguration : IBindingRoot, IDisposable
    {
        /// <summary>Gets the modules that have been loaded into the kernel.</summary>
        IEnumerable<INinjectModule> GetModules();

        /// <summary>Determines whether a module with the specified name has been loaded in the kernel.</summary>
        bool HasModule(string name);

        /// <summary>Loads the module(s) into the kernel.</summary>
        void Load(IEnumerable<INinjectModule> modules);

        /// <summary>Loads modules from the files that match the specified pattern(s).</summary>
        /// <param name="filePatterns">The file patterns (i.e. "*.dll", "modules/*.rb") to match.</param>
        void Load(IEnumerable<string> filePatterns); // todo: @feature won't be supported initially

        /// <summary>Loads modules defined in the specified assemblies.</summary>
        void Load(IEnumerable<Assembly> assemblies);

        /// <summary>Unloads the plugin with the specified name.</summary>
        /// <param name="name">The plugin's name.</param>
        void Unload(string name);

        /// <summary>Gets the bindings registered for the specified service.</summary>
        IBinding[] GetBindings(Type service);

        /// <summary>Creates the readonly kernel.</summary>
        IReadOnlyKernel BuildReadOnlyKernel();
    }
}
