// VsPkg.cs : Implementation of monoflavor
//

using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Flavor;

namespace binaryfinery.monoflavor
{
    [ProvideProjectFactory(typeof(MonoFlavorProjectFactory), 
        "Mono Flavor", "Mono Files (*.csproj);*.csproj", null, null, null)]
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [DefaultRegistryRoot("Software\\Microsoft\\VisualStudio\\9.0")]
    [InstalledProductRegistration(false, "#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideLoadKey("Standard", "1.0", "monoflave", "binaryfinery", 104)]
    [Guid(GuidList.guidmonoflavorPkgString)]
    public sealed class MonoFlavorPackage : Package
    {
        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public MonoFlavorPackage()
        {
            Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", ToString()));
        }


        /////////////////////////////////////////////////////////////////////////////
        // Overriden Package Implementation

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initilaization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", ToString()));
            this.RegisterProjectFactory(new MonoFlavorProjectFactory(this));
            base.Initialize();
        }
    }

    [ComVisible(false)]
    [Guid(MonoProjectFactoryGuid)]
    public class MonoFlavorProjectFactory : FlavoredProjectFactoryBase
    {
        private const string MonoProjectFactoryGuid = "E613F3A2-FE9C-494F-B74E-F63BCB86FEA6";
        //private const string MonoProjectFactoryGuid = "628E6A0A-36B0-4a79-BB2E-3E1B3BB38C82";
        private MonoFlavorPackage package;

        public MonoFlavorProjectFactory(MonoFlavorPackage package)
            : base()
        {
            this.package = package;
        }

        protected override object PreCreateForOuter(IntPtr outerProjectIUnknown)
        {
            return new MonoFlavePackageProject(this.package);
        }
    }

    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid(PackageProjectGuid)]
    public class MonoFlavePackageProject : FlavoredProjectBase
    {
        private const string PackageProjectGuid = "628E6A0A-36B0-4a79-BB2E-3E1B3BB38C82";
        //private const string PackageProjectGuid = "E613F3A2-FE9C-494F-B74E-F63BCB86FEA6";
        private MonoFlavorPackage package;

        public MonoFlavePackageProject(MonoFlavorPackage package)
        {
            this.package = package;
        }
            
        protected override void SetInnerProject(IntPtr innerIUnknown)
        {
            if (base.serviceProvider == null)
            {
                base.serviceProvider = this.package;
            }

            base.SetInnerProject(innerIUnknown);
        }

    }

}