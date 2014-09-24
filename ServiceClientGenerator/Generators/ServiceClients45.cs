﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 12.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace ServiceClientGenerator.Generators
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "12.0.0.0")]
    public partial class ServiceClients45 : BaseGenerator
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            
            #line 6 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"

    AddLicenseHeader();

            
            #line default
            #line hidden
            this.Write("\r\n\r\nusing System;\r\nusing System.Runtime.ExceptionServices;\r\nusing System.Threadin" +
                    "g;\r\nusing System.Threading.Tasks;\r\nusing System.Collections.Generic;\r\n\r\nusing ");
            
            #line 17 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.Namespace));
            
            #line default
            #line hidden
            this.Write(".Model;\r\nusing ");
            
            #line 18 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.Namespace));
            
            #line default
            #line hidden
            this.Write(".Model.Internal.MarshallTransformations;\r\nusing Amazon.Runtime;\r\nusing Amazon.Run" +
                    "time.Internal;\r\nusing Amazon.Runtime.Internal.Auth;\r\nusing Amazon.Runtime.Intern" +
                    "al.Transform;\r\n\r\nnamespace ");
            
            #line 24 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.Namespace));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n");
            
            #line 26 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
    this.FormatServiceClientDocumentation(this.Config.ServiceModel.Documentation); 
            
            #line default
            #line hidden
            this.Write("    public partial class Amazon");
            
            #line 27 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Client : AmazonServiceClient, IAmazon");
            
            #line 27 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("\r\n    {\r\n");
            
            #line 29 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"

    // Generates basic constructors for the service if enabled in the model
    if(this.Config.GenerateConstructors)
    {

            
            #line default
            #line hidden
            this.Write("        #region Constructors\r\n\r\n        /// <summary>\r\n        /// Constructs Ama" +
                    "zon");
            
            #line 37 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write(@"Client with the credentials loaded from the application's
        /// default configuration, and if unsuccessful from the Instance Profile service on an EC2 instance.
        /// 
        /// Example App.config with credentials set. 
        /// <code>
        /// &lt;?xml version=""1.0"" encoding=""utf-8"" ?&gt;
        /// &lt;configuration&gt;
        ///     &lt;appSettings&gt;
        ///         &lt;add key=""AWSProfileName"" value=""AWS Default""/&gt;
        ///     &lt;/appSettings&gt;
        /// &lt;/configuration&gt;
        /// </code>
        ///
        /// </summary>
        public Amazon");
            
            #line 51 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Client()\r\n            : base(FallbackCredentialsFactory.GetCredentials(), new Ama" +
                    "zon");
            
            #line 52 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Config()) { }\r\n\r\n        /// <summary>\r\n        /// Constructs Amazon");
            
            #line 55 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write(@"Client with the credentials loaded from the application's
        /// default configuration, and if unsuccessful from the Instance Profile service on an EC2 instance.
        /// 
        /// Example App.config with credentials set. 
        /// <code>
        /// &lt;?xml version=""1.0"" encoding=""utf-8"" ?&gt;
        /// &lt;configuration&gt;
        ///     &lt;appSettings&gt;
        ///         &lt;add key=""AWSProfileName"" value=""AWS Default""/&gt;
        ///     &lt;/appSettings&gt;
        /// &lt;/configuration&gt;
        /// </code>
        ///
        /// </summary>
        /// <param name=""region"">The region to connect.</param>
        public Amazon");
            
            #line 70 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Client(RegionEndpoint region)\r\n            : base(FallbackCredentialsFactory.GetC" +
                    "redentials(), new Amazon");
            
            #line 71 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Config{RegionEndpoint = region}) { }\r\n\r\n        /// <summary>\r\n        /// Constr" +
                    "ucts Amazon");
            
            #line 74 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write(@"Client with the credentials loaded from the application's
        /// default configuration, and if unsuccessful from the Instance Profile service on an EC2 instance.
        /// 
        /// Example App.config with credentials set. 
        /// <code>
        /// &lt;?xml version=""1.0"" encoding=""utf-8"" ?&gt;
        /// &lt;configuration&gt;
        ///     &lt;appSettings&gt;
        ///         &lt;add key=""AWSProfileName"" value=""AWS Default""/&gt;
        ///     &lt;/appSettings&gt;
        /// &lt;/configuration&gt;
        /// </code>
        ///
        /// </summary>
        /// <param name=""config"">The Amazon");
            
            #line 88 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Client Configuration Object</param>\r\n        public Amazon");
            
            #line 89 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Client(Amazon");
            
            #line 89 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Config config)\r\n            : base(FallbackCredentialsFactory.GetCredentials(), c" +
                    "onfig) { }\r\n\r\n        /// <summary>\r\n        /// Constructs Amazon");
            
            #line 93 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Client with AWS Credentials\r\n        /// </summary>\r\n        /// <param name=\"cre" +
                    "dentials\">AWS Credentials</param>\r\n        public Amazon");
            
            #line 96 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Client(AWSCredentials credentials)\r\n            : this(credentials, new Amazon");
            
            #line 97 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Config())\r\n        {\r\n        }\r\n\r\n        /// <summary>\r\n        /// Constructs " +
                    "Amazon");
            
            #line 102 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Client with AWS Credentials\r\n        /// </summary>\r\n        /// <param name=\"cre" +
                    "dentials\">AWS Credentials</param>\r\n        /// <param name=\"region\">The region t" +
                    "o connect.</param>\r\n        public Amazon");
            
            #line 106 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Client(AWSCredentials credentials, RegionEndpoint region)\r\n            : this(cre" +
                    "dentials, new Amazon");
            
            #line 107 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Config{RegionEndpoint = region})\r\n        {\r\n        }\r\n\r\n        /// <summary>\r\n" +
                    "        /// Constructs Amazon");
            
            #line 112 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Client with AWS Credentials and an\r\n        /// Amazon");
            
            #line 113 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Client Configuration object.\r\n        /// </summary>\r\n        /// <param name=\"cr" +
                    "edentials\">AWS Credentials</param>\r\n        /// <param name=\"clientConfig\">The A" +
                    "mazon");
            
            #line 116 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Client Configuration Object</param>\r\n        public Amazon");
            
            #line 117 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Client(AWSCredentials credentials, Amazon");
            
            #line 117 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Config clientConfig)\r\n            : base(credentials, clientConfig)\r\n        {\r\n " +
                    "       }\r\n\r\n        /// <summary>\r\n        /// Constructs Amazon");
            
            #line 123 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Client with AWS Access Key ID and AWS Secret Key\r\n        /// </summary>\r\n       " +
                    " /// <param name=\"awsAccessKeyId\">AWS Access Key ID</param>\r\n        /// <param " +
                    "name=\"awsSecretAccessKey\">AWS Secret Access Key</param>\r\n        public Amazon");
            
            #line 127 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Client(string awsAccessKeyId, string awsSecretAccessKey)\r\n            : this(awsA" +
                    "ccessKeyId, awsSecretAccessKey, new Amazon");
            
            #line 128 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Config())\r\n        {\r\n        }\r\n\r\n        /// <summary>\r\n        /// Constructs " +
                    "Amazon");
            
            #line 133 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write(@"Client with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name=""awsAccessKeyId"">AWS Access Key ID</param>
        /// <param name=""awsSecretAccessKey"">AWS Secret Access Key</param>
        /// <param name=""region"">The region to connect.</param>
        public Amazon");
            
            #line 138 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Client(string awsAccessKeyId, string awsSecretAccessKey, RegionEndpoint region)\r\n" +
                    "            : this(awsAccessKeyId, awsSecretAccessKey, new Amazon");
            
            #line 139 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Config() {RegionEndpoint=region})\r\n        {\r\n        }\r\n\r\n        /// <summary>\r" +
                    "\n        /// Constructs Amazon");
            
            #line 144 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Client with AWS Access Key ID, AWS Secret Key and an\r\n        /// Amazon");
            
            #line 145 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Client Configuration object. \r\n        /// </summary>\r\n        /// <param name=\"a" +
                    "wsAccessKeyId\">AWS Access Key ID</param>\r\n        /// <param name=\"awsSecretAcce" +
                    "ssKey\">AWS Secret Access Key</param>\r\n        /// <param name=\"clientConfig\">The" +
                    " Amazon");
            
            #line 149 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Client Configuration Object</param>\r\n        public Amazon");
            
            #line 150 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Client(string awsAccessKeyId, string awsSecretAccessKey, Amazon");
            
            #line 150 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Config clientConfig)\r\n            : base(awsAccessKeyId, awsSecretAccessKey, clie" +
                    "ntConfig)\r\n        {\r\n        }\r\n\r\n        /// <summary>\r\n        /// Constructs" +
                    " Amazon");
            
            #line 156 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write(@"Client with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name=""awsAccessKeyId"">AWS Access Key ID</param>
        /// <param name=""awsSecretAccessKey"">AWS Secret Access Key</param>
        /// <param name=""awsSessionToken"">AWS Session Token</param>
        public Amazon");
            
            #line 161 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Client(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken)\r" +
                    "\n            : this(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, new Ama" +
                    "zon");
            
            #line 162 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Config())\r\n        {\r\n        }\r\n\r\n        /// <summary>\r\n        /// Constructs " +
                    "Amazon");
            
            #line 167 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write(@"Client with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name=""awsAccessKeyId"">AWS Access Key ID</param>
        /// <param name=""awsSecretAccessKey"">AWS Secret Access Key</param>
        /// <param name=""awsSessionToken"">AWS Session Token</param>
        /// <param name=""region"">The region to connect.</param>
        public Amazon");
            
            #line 173 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Client(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken, " +
                    "RegionEndpoint region)\r\n            : this(awsAccessKeyId, awsSecretAccessKey, a" +
                    "wsSessionToken, new Amazon");
            
            #line 174 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Config{RegionEndpoint = region})\r\n        {\r\n        }\r\n\r\n        /// <summary>\r\n" +
                    "        /// Constructs Amazon");
            
            #line 179 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Client with AWS Access Key ID, AWS Secret Key and an\r\n        /// Amazon");
            
            #line 180 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write(@"Client Configuration object. 
        /// </summary>
        /// <param name=""awsAccessKeyId"">AWS Access Key ID</param>
        /// <param name=""awsSecretAccessKey"">AWS Secret Access Key</param>
        /// <param name=""awsSessionToken"">AWS Session Token</param>
        /// <param name=""clientConfig"">The Amazon");
            
            #line 185 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Client Configuration Object</param>\r\n        public Amazon");
            
            #line 186 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Client(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken, " +
                    "Amazon");
            
            #line 186 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Config.BaseName));
            
            #line default
            #line hidden
            this.Write("Config clientConfig)\r\n            : base(awsAccessKeyId, awsSecretAccessKey, awsS" +
                    "essionToken, clientConfig)\r\n        {\r\n        }\r\n\r\n        #endregion\r\n");
            
            #line 192 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"

    }

            
            #line default
            #line hidden
            this.Write("\r\n        #region Overrides\r\n\r\n        protected override AbstractAWSSigner Creat" +
                    "eSigner()\r\n        {\r\n            return new ");
            
            #line 200 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GeneratorHelpers.DetermineSigner(this.Config.ServiceModel.SignatureVersion, this.Config.BaseName)));
            
            #line default
            #line hidden
            this.Write("();\r\n        }    \r\n\r\n");
            
            #line 203 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"

    // Generates any overrides specified in the customizations file
        if(this.Config.ServiceModel.Customizations.PipelineOverride != null)
        {

            
            #line default
            #line hidden
            this.Write("        protected override void CustomizeRuntimePipeline(RuntimePipeline pipeline" +
                    ")\r\n        {\r\n");
            
            #line 210 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"

            var pipelineOverrides = this.Config.ServiceModel.Customizations.PipelineOverride;
            foreach(var o in pipelineOverrides.Overrides )
            {
                if(o.OverrideMethod == "remove")
                {

            
            #line default
            #line hidden
            this.Write("            pipeline.");
            
            #line 217 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(o.FormattedOverrideMethod));
            
            #line default
            #line hidden
            this.Write("();\r\n");
            
            #line 218 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"

                }
                else
                {

            
            #line default
            #line hidden
            this.Write("            pipeline.");
            
            #line 223 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(o.FormattedOverrideMethod));
            
            #line default
            #line hidden
            this.Write("(new ");
            
            #line 223 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(o.NewType));
            
            #line default
            #line hidden
            this.Write("(");
            
            #line 223 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(o.ConstructorInput));
            
            #line default
            #line hidden
            this.Write("));\r\n");
            
            #line 224 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"

                }
            }

            
            #line default
            #line hidden
            this.Write("        }\t\r\n");
            
            #line 229 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"

        }

            
            #line default
            #line hidden
            this.Write("\r\n        #endregion\r\n\r\n        #region Dispose\r\n\r\n        protected override voi" +
                    "d Dispose(bool disposing)\r\n        {\r\n            base.Dispose(disposing);\r\n    " +
                    "    }\r\n\r\n        #endregion\r\n\r\n        ");
            
            #line 244 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"

        foreach(var operation in this.Config.ServiceModel.Operations)
        {
        
            
            #line default
            #line hidden
            this.Write("\r\n        #region  ");
            
            #line 249 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(operation.Name));
            
            #line default
            #line hidden
            this.Write("\r\n\r\n");
            
            #line 251 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"

        // Creates a version of the operation that takes no arguments and passes a request with no set members if specified in the cusomtizations
        if (this.Config.ServiceModel.Customizations.CreateNoArgOverload(operation.Name))
        {

            
            #line default
            #line hidden
            
            #line 256 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
 this.FormatOperationDocumentation(operation, false); 
            
            #line default
            #line hidden
            this.Write("        public ");
            
            #line 257 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(operation.Name));
            
            #line default
            #line hidden
            this.Write("Response ");
            
            #line 257 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(operation.Name));
            
            #line default
            #line hidden
            this.Write("()\r\n        {\r\n            return ");
            
            #line 259 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(operation.Name));
            
            #line default
            #line hidden
            this.Write("(new ");
            
            #line 259 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(operation.Name));
            
            #line default
            #line hidden
            this.Write("Request());\r\n        }\r\n\r\n");
            
            #line 262 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"

        }
        // Adds any simple method forms specified in the customizations file
        AddSimpleClientMethods(operation);

            
            #line default
            #line hidden
            
            #line 267 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
 this.FormatOperationDocumentation(operation); 
            
            #line default
            #line hidden
            this.Write("        ");
            
            #line 268 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(operation.IsInternal ? "internal" : "public"));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 268 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(operation.Name));
            
            #line default
            #line hidden
            this.Write("Response ");
            
            #line 268 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(operation.Name));
            
            #line default
            #line hidden
            this.Write("(");
            
            #line 268 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(operation.Name));
            
            #line default
            #line hidden
            this.Write("Request request)\r\n        {\r\n            var marshaller = new ");
            
            #line 270 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(operation.Name));
            
            #line default
            #line hidden
            this.Write("RequestMarshaller();\r\n            var unmarshaller = ");
            
            #line 271 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(operation.Name));
            
            #line default
            #line hidden
            this.Write("ResponseUnmarshaller.Instance;\r\n\r\n            return Invoke<");
            
            #line 273 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(operation.Name));
            
            #line default
            #line hidden
            this.Write("Request,");
            
            #line 273 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(operation.Name));
            
            #line default
            #line hidden
            this.Write("Response>(request, marshaller, unmarshaller);\r\n        }\r\n\r\n");
            
            #line 276 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
AddAsyncDocumentation(operation.Name);
            
            #line default
            #line hidden
            this.Write("        ");
            
            #line 277 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(operation.IsInternal ? "internal" : "public"));
            
            #line default
            #line hidden
            this.Write(" Task<");
            
            #line 277 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(operation.Name));
            
            #line default
            #line hidden
            this.Write("Response> ");
            
            #line 277 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(operation.Name));
            
            #line default
            #line hidden
            this.Write("Async(");
            
            #line 277 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(operation.Name));
            
            #line default
            #line hidden
            this.Write("Request request, System.Threading.CancellationToken cancellationToken = default(C" +
                    "ancellationToken))\r\n        {\r\n            var marshaller = new ");
            
            #line 279 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(operation.Name));
            
            #line default
            #line hidden
            this.Write("RequestMarshaller();\r\n            var unmarshaller = ");
            
            #line 280 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(operation.Name));
            
            #line default
            #line hidden
            this.Write("ResponseUnmarshaller.Instance;\r\n\r\n            return InvokeAsync<");
            
            #line 282 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(operation.Name));
            
            #line default
            #line hidden
            this.Write("Request,");
            
            #line 282 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(operation.Name));
            
            #line default
            #line hidden
            this.Write("Response>(request, marshaller, \r\n                unmarshaller, cancellationToken)" +
                    ";\r\n        }\r\n\r\n        #endregion\r\n        ");
            
            #line 287 "C:\dev\net\runtime.rebase\sdk\src\ServiceClientGenerator\Generators\ServiceClients45.tt"

        }
        
            
            #line default
            #line hidden
            this.Write("\r\n    }\r\n}\r\n");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
}