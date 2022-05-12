using System.Runtime.InteropServices;

namespace ButterflySystems.Security.SecurityHeaders
{
    [StructLayout(LayoutKind.Sequential, Size = 1)]
    public struct HeaderKeyNames
    {
        public static readonly string AccessControlExposeHeaders = "Access-Control-Expose-Headers";
        public static readonly string AccessControlAllowOrigin = "Access-Control-Allow-Origin";
        public static readonly string AccessControlAllowCredentials = "Access-Control-Allow-Credentials";
        public static readonly string AccessControlAllowHeaders = "Access-Control-Allow-Headers";
        public static readonly string AccessControlAllowMethods = "Access-Control-Allow-Methods";
        public static readonly string XssProtection = "X-XSS-Protection";
        public static readonly string InstanceId = "X-Instance-Id";
        public static readonly string TraceIdentifier = "X-TraceIdentifier";

        /// <summary>
        /// Disables content sniffing
        /// </summary>
        public static readonly string NoSniff = "nosniff";

        [StructLayout(LayoutKind.Sequential, Size = 1)]
        public struct DefaultValues
        {
            public static readonly string AccessControlExposeHeadersAll = "*";
            public static readonly string AccessControlAllowOriginAll = "*";
            public static readonly string AccessControlAllowCredentialsActive = "true";
            public static readonly string AccessControlAllowHeaderAll = "*";
            public static readonly string AccessControlAllowHeader = "Content-Type, Authorization, X-CSRF-Token, X-Requested-With, Accept, Accept-Version, Accept-Encoding, Content-Length, Content-MD5, Date, X-Api-Version, X-File-SearchTerm";
            public static readonly string AccessControlAllowMethod = "POST,GET,PUT,PATCH,DELETE,OPTIONS";
            public static readonly string XFrameOptionsDeny = "DENY";
            public static readonly string XFrameOptionsSameOrigin = " SAMEORIGIN";
            public static readonly string XssProtectionEnabled = "1";
            public static readonly string XssProtectionDisabled = "0";
            public static readonly string XssProtectionBlock = "1; mode=block";

            /// <summary>
            /// Disables content sniffing
            /// </summary>
            public static readonly string NoSniffDisable = "nosniff";

            /// <summary>
            /// A partially supported directive that tells the user-agent to report potential XSS attacks to a single URL. Data will be POST'd to the report URL in JSON format. 
            /// {0} specifies the report url, including protocol
            /// </summary>
            public static readonly string XssProtectionReport = "1; report={0}";

            /// <summary>
            /// The page can only be displayed in a frame on the specified origin. {0} specifies the format string
            /// </summary>
            public static readonly string XFrameOptionsAllowFromUri = "ALLOW-FROM {0}";
        }
    }
}