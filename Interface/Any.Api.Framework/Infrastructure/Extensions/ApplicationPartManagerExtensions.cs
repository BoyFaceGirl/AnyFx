using Any.Core;
using Any.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Any.Api.Framework.Infrastructure.Extensions
{
    class ApplicationPartManagerExtensions
    {
        #region Fields

        private static readonly IAnyFileProvider _fileProvider;
        private static readonly List<string> _baseAppLibraries;
        //private static readonly Dictionary<string, PluginLoadedAssemblyInfo> _loadedAssemblies = new Dictionary<string, PluginLoadedAssemblyInfo>();
        private static readonly ReaderWriterLockSlim _locker = new ReaderWriterLockSlim();

        #endregion

        static ApplicationPartManagerExtensions()
        {
            //we use the default file provider, since the DI isn't initialized yet
            _fileProvider = CommonHelper.DefaultFileProvider;

            _baseAppLibraries = new List<string>();

            //get all libraries from /bin/{version}/ directory
            _baseAppLibraries.AddRange(_fileProvider.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Select(fileName => _fileProvider.GetFileName(fileName)));

            //get all libraries from base site directory
            if (!AppDomain.CurrentDomain.BaseDirectory.Equals(Environment.CurrentDirectory, StringComparison.InvariantCultureIgnoreCase))
            {
                _baseAppLibraries.AddRange(_fileProvider.GetFiles(Environment.CurrentDirectory, "*.dll")
                    .Select(fileName => _fileProvider.GetFileName(fileName)));
            }

            //get all libraries from refs directory
            //var refsPathName = _fileProvider.Combine(Environment.CurrentDirectory, NopPluginDefaults.RefsPathName);
            //if (_fileProvider.DirectoryExists(refsPathName))
            //{
            //    _baseAppLibraries.AddRange(_fileProvider.GetFiles(refsPathName, "*.dll")
            //        .Select(fileName => _fileProvider.GetFileName(fileName)));
            //}


        }


    }
}
