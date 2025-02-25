﻿using Analogy.LogViewer.Template;
using System.Drawing;
using System.Reflection;
using System.Runtime.Versioning;

namespace Analogy.DataTypes
{
    public sealed class MissingDownloadInformation : AnalogyDownloadInformation
    {
        public override Guid FactoryId { get; set; }
        public override string Name { get; set; }

        public override Version InstalledVersion { get; }
        public override TargetFrameworkAttribute CurrentFrameworkAttribute { get; set; } = (TargetFrameworkAttribute)Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(TargetFrameworkAttribute));
        protected override string RepositoryURL { get; set; }
        public override string InstalledVersionNumber { get; }
        public Image Image { get; set; }
        public MissingDownloadInformation(Guid factoryId, string name,string repoURL,Image image)
        {
            FactoryId = factoryId;
            Name = name;
            InstalledVersion = new Version(0,0,0);
            InstalledVersionNumber = "0.0.0";
            RepositoryURL = repoURL;
            Image = image;
        }

    }
}
