// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#define DEBUG

using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Microsoft.AppCenter
{
    public partial class AppCenter
    {
        internal AppCenter()
        {
        }

        /* Error message to display for unsupported targets. */
        const string ErrorMessage =
            "[AppCenter] ASSERT: Cannot use App Center on this target. If you are using the SDK from a .NET standard library, you must also add the App Center NuGet packages in the Android, iOS and UWP/WPF/WinForms projects as well. Other targets are not yet supported.";

        static LogLevel PlatformLogLevel { get; set; }

        static Task<bool> PlatformIsEnabledAsync()
        {
            return Task.FromResult(false);
        }

        static Task PlatformSetEnabledAsync(bool enabled)
        {
            return Task.FromResult(default(object));
        }

        static Task<Guid?> PlatformGetInstallIdAsync()
        {
            return Task.FromResult((Guid?)null);
        }

        static void PlatformSetLogUrl(string logUrl)
        {
            Debug.WriteLine(ErrorMessage);
        }

        static void PlatformSetUserId(string userId)
        {
            Debug.WriteLine(ErrorMessage);
        }

        static bool PlatformConfigured { get; }

        static void PlatformConfigure(string appSecret)
        {
            Debug.WriteLine(ErrorMessage);
        }

        static void PlatformStart(params Type[] services)
        {
            Debug.WriteLine(ErrorMessage);
        }

        static void PlatformStart(string appSecret, params Type[] services)
        {
            Debug.WriteLine(ErrorMessage);
        }

        static void PlatformSetCustomProperties(CustomProperties customProperties)
        {
            Debug.WriteLine(ErrorMessage);
        }

        internal static void PlatformUnsetInstance()
        {
        }

        /// <summary>
        /// Sets the two-letter ISO country code to send to the backend.
        /// </summary>
        /// <param name="countryCode">The two-letter ISO country code. See <see href="https://www.iso.org/obp/ui/#search"/> for more information.</param>
        public static void SetCountryCode(string countryCode)
        {
            if (countryCode != null && countryCode.Length != 2)
            {
                AppCenterLog.Error(AppCenterLog.LogTag, "App Center accepts only the two-letter ISO country code.");
                return;
            }
            Debug.WriteLine(ErrorMessage);
        }
    }
}
