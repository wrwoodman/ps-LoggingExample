using System;
using Argo.MyProject.Util;
using Newtonsoft.Json;
using ObjectCloner.Extensions;
using PostSharp.Patterns.Diagnostics;

namespace Argo.MyProject.Model
{
    /// <summary>
    /// This is for project data.
    /// Make as many of these as needed.
    /// Consider creating a DTO project if there are many DTOs and if they are shared across multiple projects.
    /// </summary>
    public class MyProjectDTO
    {
        /// <summary>
        /// This field might represent the unique identifier for the record.
        /// </summary>
        public Guid DataId { get; set; }
        /// <summary>
        /// Always add comments over a data field to describe its purpose.
        /// </summary>
        public string DataPoint1 { get; set; }
        /// <summary>
        /// Comments over your fields can show up in the Swagger Document
        /// </summary>
        public bool HasMoreData { get; set; }
        /// <summary>
        /// Comments should tell the consumer of your data what to expect.
        /// </summary>
        public int NextId { get; set; }
        /// <summary>
        /// This is PII data and should not be written to the log file except in debug builds.
        /// </summary>
        public string PIIData { get; set; }

        /// <summary>
        /// By overriding the ToString method, we are able to output the information in the object to the log file.
        /// Notice that PII data is blocked out for non-debug builds.
        /// </summary>
        /// <returns></returns>
        [Log(AttributeExclude = true)]
        public override string ToString()
        {
#if DEBUG
            return JsonConvert.SerializeObject(this);
#else
            MyProjectDTO x = this.DeepClone();
            x.PIIData = ScrubData.Obscure(x?.PIIData);
            return JsonConvert.SerializeObject(x);
#endif
        }
    }
}
