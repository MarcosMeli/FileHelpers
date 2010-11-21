// <copyright file="DemoGenerator.partial.cs" company="Microsoft">
//  Copyright © Microsoft. All Rights Reserved.
// </copyright>

namespace FileHelpers.SamplesDashboard.Demos
{
    using System;
    using T4Toolbox;

    public partial class DemoGenerator
    {
        protected override void Validate()
        {
            this.Warning("Template properties have not been validated");
        }
    }
}
