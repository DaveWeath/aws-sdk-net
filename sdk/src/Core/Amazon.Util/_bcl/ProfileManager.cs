﻿/*******************************************************************************
 *  Copyright 2014 Amazon.com, Inc. or its affiliates. All Rights Reserved.
 *  Licensed under the Apache License, Version 2.0 (the "License"). You may not use
 *  this file except in compliance with the License. A copy of the License is located at
 *
 *  http://aws.amazon.com/apache2.0
 *
 *  or in the "license" file accompanying this file.
 *  This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
 *  CONDITIONS OF ANY KIND, either express or implied. See the License for the
 *  specific language governing permissions and limitations under the License.
 * *****************************************************************************
 *    __  _    _  ___
 *   (  )( \/\/ )/ __)
 *   /__\ \    / \__ \
 *  (_)(_) \/\/  (___/
 *
 *  AWS SDK for .NET
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.IO;

using Amazon.Runtime;
using Amazon.Runtime.Internal.Settings;
using Amazon.Runtime.Internal.Util;

namespace Amazon.Util
{
    /// <summary>
    /// This class allows profiles supporting AWS credentials and SAML-based authentication to be registered with 
    /// the SDK so that they can later be reference by a profile name. The credential profiles will be available 
    /// for use in the AWS Toolkit for Visual Studio and the AWS Tools for Windows PowerShell. 
    /// <para>
    /// The credentials are stored under the current users AppData folder encrypted using Windows Data Protection API.
    /// </para>
    /// <para>
    /// To reference a profile from an application's App.config or Web.config use the AWSProfileName setting.
    /// <code>
    /// &lt;?xml version="1.0" encoding="utf-8" ?&gt;
    /// &lt;configuration&gt;
    ///     &lt;appSettings&gt;
    ///         &lt;add key="AWSProfileName" value="development"/&gt;
    ///     &lt;/appSettings&gt;
    /// &lt;/configuration&gt;
    /// </code>
    /// </para>
    /// </summary>
    public static class ProfileManager
    {
        // if a profile does not contain a profile type entry, we assume AWS credentials
        public const string AWSCredentialsProfileType = "AWS";
        public const string SAMLRoleProfileType = "SAML";

        /// <summary>
        /// Registers an AWS credentials profile that can later be referenced by the profileName.
        /// This profile will only be visible for the current user.
        /// </summary>
        /// <param name="profileName">Name given to the AWS credentials.</param>
        /// <param name="accessKeyId">The AWS access key id</param>
        /// <param name="secretKey">The AWS secret key</param>
        public static void RegisterProfile(string profileName, string accessKeyId, string secretKey)
        {
            AWSCredentialsProfile.Persist(profileName, accessKeyId, secretKey);
        }

        /// <summary>
        /// <para>
        /// Registers a role-based profile to be used with SAML authentication. The profile contains
        /// details of the role to be assumed when AWS credentials are requested based on the role and
        /// a reference to a SAML endpoint profile containing details of the endpoint to be called to
        /// authenticate the user.
        /// </para>
        /// <para>
        /// If user identity information is not supplied then the identity of the logged-in user will 
        /// be used when authenticaton is performed against the endpoint referenced in the SAML endpoint 
        /// profile. If identity is provided, no password information is stored in the role profile and
        /// the user must supply the password for the identity prior to authentication with the endpoint.
        /// </para>
        /// </summary>
        /// <param name="profileName">Name to be assigned to the profile</param>
        /// <param name="endpointName">
        /// The name assigned to the endpoint settings, previously saved with RegisterSAMLEndpoint.
        /// </param>
        /// <param name="roleArn">
        /// The arn of the role that the user wants to assume when using this profile. This 
        /// must be one of the set returned by the saml endpoint when the user authenticates.
        /// </param>
        /// <param name="userIdentity">
        /// Optional. By default the identity of the logged-in user will be used when authentication
        /// is performed - the user will not be prompted to supply a password. By supplying a custom 
        /// identity for this parameter, the user will be prompted to supply the password for the 
        /// identity prior to authentication.
        /// </param>
        public static void RegisterSAMLRoleProfile(string profileName,
                                                   string endpointName,
                                                   string roleArn,
                                                   string userIdentity)
        {
            SAMLRoleProfile.Persist(profileName, endpointName, roleArn, userIdentity);
        }

        /// <summary>
        /// Registers an endpoint to be used in conjunction with SAML role profiles. The role profiles
        /// reference the endpoint settings to obtain the actual endpoint and any customization settings
        /// needed to perform authentication.
        /// </summary>
        /// <param name="endpointName">Name to be assigned to the endpoint settings.</param>
        /// <param name="endpoint">The full uri of the authentication endpoint.</param>
        /// <param name="authenticationType">
        /// The authentication type to use when performing calls against the endpoint. Valid values are 'NTLM',
        /// 'Digest', 'Kerberos' and 'Negotiate'. The default if not configured (null/empty string) is 'Kerberos'.
        /// </param>
        /// <returns>The unique id assigned to the new settings.</returns>
        public static string RegisterSAMLEndpoint(string endpointName, 
                                                  Uri endpoint, 
                                                  string authenticationType)
        {
            return SAMLEndpointSettings.Persist(endpointName, endpoint, authenticationType);
        }

        /// <summary>
        /// Deletes the settings for an AWS credentials or SAML role profile from the SDK account store.
        /// </summary>
        /// <param name="profileName">The name of the profile to remove.</param>
        public static void UnregisterProfile(string profileName)
        {
            var settings = PersistenceManager.Instance.GetSettings(SettingsConstants.RegisteredProfiles);
            var os = ReadProfileSettings(settings, profileName);
            if (os != null)
            {
                settings.Remove(os.UniqueKey);
                PersistenceManager.Instance.SaveSettings(SettingsConstants.RegisteredProfiles, settings);
            }
        }

        /// <summary>
        /// Lists all profile names registered with the SDK account store.
        /// </summary>
        /// <returns>The profile names.</returns>
        public static IEnumerable<string> ListProfileNames()
        {
            var settings = PersistenceManager.Instance.GetSettings(SettingsConstants.RegisteredProfiles);
            return settings.Select(os => os.GetValueOrDefault(SettingsConstants.DisplayNameField, null)).ToList();
        }

        /// <summary>
        /// Loads and returns all available credential profiles registered in the store.
        /// </summary>
        /// <returns>Collection of profiles.</returns>
        public static IEnumerable<ProfileSettingsBase> ListProfiles()
        {
            var profiles = new List<ProfileSettingsBase>();
            var profileNames = ListProfileNames();
            foreach (var profileName in profileNames)
            {
                try
                {
                    if (SAMLRoleProfile.CanCreateFrom(profileName))
                        profiles.Add(SAMLRoleProfile.LoadFrom(profileName));
                    else if (AWSCredentialsProfile.CanCreateFrom(profileName))
                        profiles.Add(AWSCredentialsProfile.LoadFrom(profileName));
                }
                catch (Exception e)
                {
                    Logger.GetLogger(typeof(ProfileManager)).Error(e, "Error loading AWS credential or SAML role profile '{0}'", profileName);
                }
            }

            return profiles;
        }

        /// <summary>
        /// Checks if a given profile is known in the SDK credential store.
        /// </summary>
        /// <param name="profileName">The name of the profile to test for existence</param>
        /// <returns>True if the profile exists.</returns>
        public static bool IsProfileKnown(string profileName)
        {
            return (ReadProfileSettings(profileName) != null);
        }

        /// <summary>
        /// Copies the contents of the source profile to the destination. If the destination
        /// profile does not exist a new profile is created. Note that if the destination
        /// profile exists, all keys it contains are removed and replaced with keys from the
        /// source profile.
        /// </summary>
        /// <param name="sourceProfileName">The name of the profile to copy from.</param>
        /// <param name="destinationProfileName">The name of the profile to create or update.</param>
        /// <returns>The unique id assigned to the destination settings.</returns>
        public static string CopyProfileSettings(string sourceProfileName, string destinationProfileName)
        {
            var sourceSettings = ReadProfileSettings(sourceProfileName);
            if (sourceSettings == null)
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "An AWS credentials or SAML role profile with name '{0}' could not be found.", sourceProfileName));

            return CopyProfileSettings(sourceSettings, destinationProfileName);
        }

        /// <summary>
        /// Copies the contents of the source profile to the destination. If the destination
        /// profile does not exist a new profile is created. Note that if the destination
        /// profile exists, all keys it contains are removed and replaced with keys from the
        /// source profile.
        /// </summary>
        /// <param name="source">The source profile to copy keys and values from.</param>
        /// <param name="destinationProfileName">The name of the profile to create or update.</param>
        /// <returns>The unique id assigned to the destination settings.</returns>
        public static string CopyProfileSettings(SettingsCollection.ObjectSettings source, string destinationProfileName)
        {
            var allSettings = PersistenceManager.Instance.GetSettings(SettingsConstants.RegisteredProfiles);
            var destination = ReadProfileSettings(allSettings, destinationProfileName);
            // overwrite with new object if dest exists, not merge, otherwise we can potentially mix credential 
            // profile types
            if (destination == null)
                destination = allSettings.NewObjectSettings(Guid.NewGuid().ToString());
            else
                destination = allSettings.NewObjectSettings(destination.UniqueKey);

            destination[SettingsConstants.DisplayNameField] = destinationProfileName;
            foreach (var k in source.Keys)
            {
                if (!k.Equals(SettingsConstants.DisplayNameField))
                    destination[k] = source[k];
            }

            PersistenceManager.Instance.SaveSettings(SettingsConstants.RegisteredProfiles, allSettings);

            return destination.UniqueKey;
        }

        /// <summary>
        /// Tries to get the AWS credentials from a profile in the SDK account store.
        /// </summary>
        /// <param name="profileName">The profile to get the credentials for.</param>
        /// <param name="credentials">Outputs the credentials for the profile.</param>
        /// <returns>Returns true if the profile exists otherwise false is returned.</returns>
        public static bool TryGetAWSCredentials(string profileName, out AWSCredentials credentials)
        {
            credentials = null;

            try
            {
                AWSCredentialsProfile profile;
                if (TryGetProfile(profileName, out profile))
                    credentials = profile.Credentials;
            }
            catch (Exception e)
            {
                Logger.GetLogger(typeof(ProfileManager)).Error(e, "Error loading AWS credentials from profile {0}", profileName);
            }

            return credentials != null;
        }

        /// <summary>
        /// Gets the AWS credentials from a profile in the SDK account store. 
        /// </summary>
        /// <param name="profileName">The profile to get the credentials for.</param>
        /// <returns>The AWS credentials for the profile.</returns>
        /// <exception cref="AmazonClientException">Thrown if the profile does not exist</exception>
        public static AWSCredentials GetAWSCredentials(string profileName)
        {
            AWSCredentialsProfile profile;
            if (TryGetProfile(profileName, out profile))
                return profile.Credentials;

            throw new AmazonClientException(string.Format(CultureInfo.InvariantCulture, "A profile named {0} has not been registered or contains invalid data.", profileName));
        }

        /// <summary>
        /// Returns the profile with the specified name, if it has been registered in the SDK store.
        /// </summary>
        /// <param name="profileName">The name of the registered profile</param>
        /// <returns>The loaded profile data</returns>
        public static ProfileSettingsBase GetProfile(string profileName)
        {
            if (!IsProfileKnown(profileName))
                throw new AmazonClientException(string.Format(CultureInfo.InvariantCulture, "A profile named {0} has not been registered.", profileName));

            var profile = GetProfile<AWSCredentialsProfile>(profileName) ?? (ProfileSettingsBase) GetProfile<SAMLRoleProfile>(profileName);
            if (profile == null)
                throw new AmazonClientException(string.Format(CultureInfo.InvariantCulture, "A profile named {0} was found but could not be loaded.", profileName));

            return profile;
        }

        /// <summary>
        /// Returns the persisted data in the SDK store as a profile of the specified type T.
        /// </summary>
        /// <param name="profileName">The name of the profile holding the settings.</param>
        /// <returns>The loaded profile. An exception is thrown if the profile could not be loaded.</returns>
        /// <exception cref="AmazonClientException">Thrown if the profile does not exist</exception>
        /// <remarks>
        /// Currently supported profile types: AWSCredentialsProfile and SAMLRoleProfile.
        /// </remarks>
        public static T GetProfile<T>(string profileName) where T : ProfileSettingsBase
        {
            if (typeof(T) == typeof(AWSCredentialsProfile))
                return GetProfile<AWSCredentialsProfile>(profileName) as T;

            if (typeof(T) == typeof(SAMLRoleProfile))
                return GetProfile<SAMLRoleProfile>(profileName) as T;

            throw new ArgumentException("Unrecognized profile type parameter");
        }

        /// <summary>
        /// Tries to load the specified profile data corresponding to profile type T from a named 
        /// profile in the SDK account store.
        /// </summary>
        /// <param name="profileName">The name of the profile holding the settings.</param>
        /// <param name="profile">The loaded profile data.</param>
        /// <returns>Returns true if the profile exists otherwise false is returned.</returns>
        /// <remarks>
        /// Currently supported profile types: AWSCredentialsProfile and SAMLRoleProfile.
        /// </remarks>
        public static bool TryGetProfile<T>(string profileName, out T profile) where T : ProfileSettingsBase
        {
            profile = null;

            try
            {
                if (typeof(T) == typeof(AWSCredentialsProfile))
                    profile = AWSCredentialsProfile.LoadFrom(profileName) as T;
                else if (typeof(T) == typeof(SAMLRoleProfile))
                    profile = SAMLRoleProfile.LoadFrom(profileName) as T;
                else
                    throw new ArgumentException("Unrecognized profile type parameter", (typeof(T).FullName));
            }
            catch (Exception e)
            {
                Logger.GetLogger(typeof(ProfileManager)).Error(e, "Unable to load profile {0}, unknown profile, missing/invalid data or unrecognized profile type.", profileName);
            }

            return profile != null;
        }

        /// <summary>
        /// Attempts to load the settings defining a SAML endpoint.
        /// </summary>
        /// <param name="endpointName">The name assigned to the settings for the endpoint.</param>
        /// <param name="endpointSettings">The instantiated endpoint.</param>
        /// <returns>True if the settings were successfully loaded.</returns>
        public static bool TryGetSAMLEndpoint(string endpointName, out SAMLEndpointSettings endpointSettings)
        {
            endpointSettings = null;

            try
            {
                endpointSettings = SAMLEndpointSettings.LoadFrom(endpointName);
            }
            catch (Exception e)
            {
                Logger.GetLogger(typeof(ProfileManager)).Error(e, "Unable to load SAML endpoint profile '{0}', unknown profile or missing/invalid data.", endpointName);
            }

            return endpointSettings != null;
        }

        /// <summary>
        /// Loads the settings defining a SAML endpoint.
        /// </summary>
        /// <param name="endpointName">The name assigned to the settings for the endpoint.</param>
        /// <returns>The loaded settings. An exception is thrown if they could not be loaded.</returns>
        /// <exception cref="AmazonClientException">Thrown if the endpoint settings do not exist.</exception>
        public static SAMLEndpointSettings GetSAMLEndpoint(string endpointName)
        {
            SAMLEndpointSettings endpointSettings;
            if (!TryGetSAMLEndpoint(endpointName, out endpointSettings))
                throw new AmazonClientException(string.Format(CultureInfo.InvariantCulture, "A SAML endpoint profile with name {0} has not been registered or is invalid.", endpointName));

            return endpointSettings;
        }

        internal static SettingsCollection.ObjectSettings ReadProfileSettings(string profileName)
        {
            var settings = PersistenceManager.Instance.GetSettings(SettingsConstants.RegisteredProfiles);
            return ReadProfileSettings(settings, profileName);
        }

        internal static SettingsCollection.ObjectSettings ReadProfileSettings(SettingsCollection settings, string profileName)
        {
            return settings.FirstOrDefault(x => string.Equals(x[SettingsConstants.DisplayNameField], profileName, StringComparison.OrdinalIgnoreCase));
        }
    }

    /// <summary>
    /// Common base contract for all types of credential and role profiles.
    /// </summary>
    public abstract class ProfileSettingsBase
    {
        /// <summary>
        /// The user-defined name for the settings.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// The unique id of the profile in the backing store.
        /// </summary>
        public string UniqueId { get; protected set; }

        /// <summary>
        /// Saves the profile data to backing store, returning the unique id
        /// assigned to the data.
        /// </summary>
        public abstract string Persist();

        protected static SettingsCollection.ObjectSettings LoadCredentialsProfile(string profileName)
        {
            var os = ProfileManager.ReadProfileSettings(profileName);
            if (os == null)
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "An AWS credentials or SAML role profile with name '{0}' could not be found.", profileName));

            return os;
        }
    }

    /// <summary>
    /// The persisted data for a set of AWS credentials. At a minimum this
    /// is access key and secret key data.
    /// </summary>
    public class AWSCredentialsProfile : ProfileSettingsBase
    {
        public BasicAWSCredentials Credentials { get; private set; }

        /// <summary>
        /// Tests if an AWSCredentialsProfile instance could be instantiated from
        /// the persisted settings data.
        /// </summary>
        /// <param name="profileName">The name given to the persisted settings.</param>
        /// <returns>True if the settings are compatible with an AWSCredentialsProfile type.</returns>
        public static bool CanCreateFrom(string profileName)
        {
            var os = LoadCredentialsProfile(profileName);
            return CanCreateFrom(os);
        }

        /// <summary>
        /// Tests if an AWSCredentialsProfile instance could be instantiated from
        /// the persisted settings data.
        /// </summary>
        /// <param name="os">The persisted settings.</param>
        /// <returns>True if the settings are compatible with an AWSCredentialsProfile type.</returns>
        public static bool CanCreateFrom(SettingsCollection.ObjectSettings os)
        {
            var osProfileType = os.GetValueOrDefault(SettingsConstants.ProfileTypeField, null);
            // legacy AWS profiles will not have the type key present
            return osProfileType == null || osProfileType.Equals(ProfileManager.AWSCredentialsProfileType, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Instantiates an AWSCredentialsProfile instance from the specified profile name.
        /// </summary>
        /// <param name="profileName">The name of the profile holding the settings.</param>
        /// <returns>New credentials profile instance. An exception is thrown if the profile data is invalid.</returns>
        public static AWSCredentialsProfile LoadFrom(string profileName)
        {
            var os = LoadCredentialsProfile(profileName);
            return LoadFrom(os);
        }

        /// <summary>
        /// Instantiates an AWSCredentialsProfile instance from the supplied settings collection.
        /// </summary>
        /// <param name="os">The settings representing the stored profile.</param>
        /// <returns>New credentials profile instance. An exception is thrown if the profile data is invalid.</returns>
        public static AWSCredentialsProfile LoadFrom(SettingsCollection.ObjectSettings os)
        {
            if (os == null)
                throw new ArgumentNullException("os");

            if (!CanCreateFrom(os))
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Profile '{0}' does not contain AWS credential materials", os[SettingsConstants.DisplayNameField]));

            Validate(os);

            var accessKeyId = os.GetValueOrDefault(SettingsConstants.AccessKeyField, null);
            var secretkey = os.GetValueOrDefault(SettingsConstants.SecretKeyField, null);

            return new AWSCredentialsProfile(os[SettingsConstants.DisplayNameField], accessKeyId, secretkey);
        }

        /// <summary>
        /// Validates the contents of the specified profile.
        /// </summary>
        /// <param name="profileName">The name of the AWS credentials profile to validate.</param>
        /// <exception cref="InvalidDataException">Thrown if the profile settings fail to validate.</exception>
        public static void Validate(string profileName)
        {
            var os = LoadCredentialsProfile(profileName);
            Validate(os);
        }

        /// <summary>
        /// Verifies that the persisted settings contains the minimal viable data to
        /// instantiate an AWSCredentialsProfile instance.
        /// </summary>
        /// <param name="os">The persisted settings.</param>
        /// <exception cref="InvalidDataException">Thrown if the profile settings fail to validate.</exception>
        private static void Validate(SettingsCollection.ObjectSettings os)
        {
            var accessKeyId = os.GetValueOrDefault(SettingsConstants.AccessKeyField, null);
            if (accessKeyId == null)
                throw new InvalidDataException("Missing or invalid access key value in the profile settings.");

            var secretkey = os.GetValueOrDefault(SettingsConstants.SecretKeyField, null);
            if (secretkey == null)
                throw new InvalidDataException("Missing or invalid secret key value in the profile settings.");
        }

        /// <summary>
        /// Persists the profile data to the store file.
        /// </summary>
        /// <returns>The unique ID assigned to the settings.</returns>
        public override string Persist()
        {
            return Persist(Name, Credentials.GetCredentials().AccessKey, Credentials.GetCredentials().SecretKey);        
        }

        /// <summary>
        /// Creates or updates the profile data in the store file.
        /// </summary>
        /// <returns>The unique ID assigned to the settings.</returns>
        public static string Persist(string profileName, string accessKeyId, string secretKey)
        {
            var settings = PersistenceManager.Instance.GetSettings(SettingsConstants.RegisteredProfiles);
            var os = ProfileManager.ReadProfileSettings(settings, profileName);
            if (os == null)
                os = settings.NewObjectSettings(Guid.NewGuid().ToString());

            os[SettingsConstants.ProfileTypeField] = ProfileManager.AWSCredentialsProfileType;
            os[SettingsConstants.DisplayNameField] = profileName;
            os[SettingsConstants.AccessKeyField] = accessKeyId;
            os[SettingsConstants.SecretKeyField] = secretKey;

            PersistenceManager.Instance.SaveSettings(SettingsConstants.RegisteredProfiles, settings);

            return os.UniqueKey;
        }

        private AWSCredentialsProfile(string profileName, string accessKeyId, string secretKey)
        {
            Name = profileName;
            Credentials = new BasicAWSCredentials(accessKeyId, secretKey);
        }
    }

    /// <summary>
    /// The persisted data for a SAML endpoint. One or more role profiles
    /// will reference this to obtain the common endpoint and other data needed
    /// to perform authentication with a set of user credentials.
    /// </summary>
    public class SAMLEndpointSettings : ProfileSettingsBase
    {
        /// <summary>
        /// The default authentication type to use when attempting to perform an
        /// authentication call against the configured endpoint.
        /// </summary>
        public static readonly string DefaultAuthenticationType = "Kerberos";

        /// <summary>
        /// The authentication endpoint which must be a HTTPS scheme.
        /// </summary>
        public Uri Endpoint { get; private set; }

        private string _authenticationType = null;

        /// <summary>
        /// The authentication type to use when calling the endpoint.
        /// </summary>
        public string AuthenticationType
        {
            get
            {
                return string.IsNullOrEmpty(_authenticationType) ? DefaultAuthenticationType : _authenticationType;
            }
        }

        /// <summary>
        /// Tests if a SAMLEndpointSettings instance could be instantiated from
        /// the persisted settings data.
        /// </summary>
        /// <param name="endpointName">The name given to the persisted settings.</param>
        /// <returns>True if the settings are compatible.</returns>
        public static bool CanCreateFrom(string endpointName)
        {
            var os = LoadSettings(endpointName);
            return CanCreateFrom(os);
        }

        /// <summary>
        /// Tests if a SAMLEndpointSettings instance could be instantiated from
        /// the persisted settings data.
        /// </summary>
        /// <param name="os">The persisted settings.</param>
        /// <returns>True if the settings are compatible.</returns>
        public static bool CanCreateFrom(SettingsCollection.ObjectSettings os)
        {
            var endpoint = os.GetValueOrDefault(SettingsConstants.EndpointField, null);
            return !string.IsNullOrEmpty(endpoint);
        }

        /// <summary>
        /// Instantiates an instance from settings stored with the specified name.
        /// </summary>
        /// <param name="endpointName">The name of the endpoint settings in the store.</param>
        /// <returns>Profile instance or an exception if the profile data does not exist/contains invalid data.</returns>
        public static SAMLEndpointSettings LoadFrom(string endpointName)
        {
            var os = LoadSettings(endpointName);
            return LoadFrom(os);
        }

        /// <summary>
        /// Instantiates an instance from the supplied settings.
        /// </summary>
        /// <param name="os">The persisted settings.</param>
        /// <returns>Profile instance or an exception if the profile data is invalid.</returns>
        public static SAMLEndpointSettings LoadFrom(SettingsCollection.ObjectSettings os)
        {
            if (os == null)
                throw new ArgumentNullException("os");

            if (!CanCreateFrom(os))
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Endpoint settings '{0}' does not contain SAML endpoint materials", os[SettingsConstants.DisplayNameField]));

            Validate(os);

            var endpoint = os.GetValueOrDefault(SettingsConstants.EndpointField, null);
            string authenticationType = os.GetValueOrDefault(SettingsConstants.AuthenticationType, null);

            return new SAMLEndpointSettings(os[SettingsConstants.DisplayNameField], new Uri(endpoint, UriKind.RelativeOrAbsolute), authenticationType);
        }

        /// <summary>
        /// Validates the contents of the specified endpoint settings.
        /// </summary>
        /// <param name="endpointName">The name of the SAML endpoint settings to validate.</param>
        /// <exception cref="InvalidDataException">Thrown if the settings fail to validate.</exception>
        public static void Validate(string endpointName)
        {
            var os = LoadSettings(endpointName);
            Validate(os);
        }

        /// <summary>
        /// Verifies that the persisted settings contains the minimal viable data to
        /// instantiate a SAMLEndpointSettings instance.
        /// </summary>
        /// <param name="os">The persisted settings.</param>
        /// <exception cref="InvalidDataException">Thrown if the settings fail to validate.</exception>
        private static void Validate(SettingsCollection.ObjectSettings os)
        {
            var endpoint = os.GetValueOrDefault(SettingsConstants.EndpointField, null);
            if (endpoint == null)
                throw new InvalidDataException("Missing endpoint value in the profile settings.");

            try
            {
                var u = new Uri(endpoint);
                if (u.Scheme != Uri.UriSchemeHttps)
                    throw new InvalidDataException("The scheme of the endpoint must be HTTPS.");
            }
            catch (UriFormatException e)
            {
                throw new InvalidDataException("The configured endpoint is not valid.", e);
            }
        }

        /// <summary>
        /// Persists the settings to the storage file.
        /// </summary>
        /// <returns>The unique id assigned to the profile</returns>
        public override string Persist()
        {
            return Persist(Name, Endpoint, AuthenticationType);
        }

        /// <summary>
        /// Creates or updates the settings data for a SAML endpoint in the backing store file. An error is
        /// thrown if the scheme for the endpoint is not https.
        /// </summary>
        /// <param name="settingsName">The name of the settings to create or update</param>
        /// <param name="endpoint">The authentication endpoint</param>
        /// <param name="authenticationType">Optional authentication type to use when performing calls against the endpoint</param>
        /// <returns>The unique id assigned to the profile</returns>
        public static string Persist(string settingsName, Uri endpoint, string authenticationType)
        {
            if (endpoint.Scheme != Uri.UriSchemeHttps)
                throw new AmazonClientException("Endpoint uri is not Https protocol.");

            var settings = PersistenceManager.Instance.GetSettings(SettingsConstants.RegisteredSAMLEndpoints);
            var os = ProfileManager.ReadProfileSettings(settings, settingsName);
            if (os == null)
                os = settings.NewObjectSettings(Guid.NewGuid().ToString());

            os[SettingsConstants.EndpointField] = endpoint.ToString();
            os[SettingsConstants.DisplayNameField] = settingsName;

            if (!string.IsNullOrEmpty(authenticationType) && !authenticationType.Equals(DefaultAuthenticationType, StringComparison.OrdinalIgnoreCase))
                os[SettingsConstants.AuthenticationType] = authenticationType;

            PersistenceManager.Instance.SaveSettings(SettingsConstants.RegisteredSAMLEndpoints, settings);

            return os.UniqueKey;
        }

        /// <summary>
        /// Constructs an endpoint settings instance.
        /// </summary>
        /// <param name="settingsName">The user-defined name to assign to the settings.</param>
        /// <param name="endpoint">
        /// The absolute uri, including any query and relyingParty data, of the endpoint.
        /// </param>
        /// <param name="authenticationType">
        /// The authentication type to use when performing requests against the endpoint.
        /// </param>
        private SAMLEndpointSettings(string settingsName, Uri endpoint, string authenticationType)
        {
            Name = settingsName;
            Endpoint = endpoint;
            if (!string.IsNullOrEmpty(authenticationType))
                _authenticationType = authenticationType;
        }

        private static SettingsCollection.ObjectSettings LoadSettings(string endpointName)
        {
            var settings = PersistenceManager.Instance.GetSettings(SettingsConstants.RegisteredSAMLEndpoints);
            var os = ProfileManager.ReadProfileSettings(settings, endpointName);
            if (os == null)
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "SAML endpoint settings with name '{0}' could not be found.", endpointName));

            return os;
        }
    }

    /// <summary>
    /// <para>
    /// The persisted data for a saml role profile for a user. This profile
    /// references an endpoint profile containing the actual endpoint to be used, and
    /// adds details of the role to be assumed when the profile is selected.
    /// </para>
    /// <para>
    /// Optionally the profile can store a username and domain to be used during 
    /// authentication (default behavior, if this is not specified, is to use the user's 
    /// default network credentials).
    /// </para>
    /// </summary>
    public class SAMLRoleProfile : ProfileSettingsBase
    {
        /// <summary>
        /// The ARN of the role that is to be assumed.
        /// </summary>
        public string RoleArn { get; internal set; }

        /// <summary>
        /// If non-default network credentials are to used contains
        /// the user identity (in domain\user format, domain optional) that 
        /// should be used to supply credentials when the profile is used in
        /// authentication. The user must be prompted to supply the 
        /// corresponding password prior to authentication.
        /// </summary>
        public string UserIdentity { get; internal set; }

        /// <summary>
        /// If a specific user identity was specified in the profile, 
        /// returns true to indicate a password needs to be obtained from 
        /// the user before obtaining network credentials to be used on 
        /// authentication. The default is to use the credentials
        /// associated with the currently logged-in user or process to
        /// perform authentication, which does not require the user to be
        /// prompted.
        /// </summary>
        public bool UseDefaultUserIdentity
        {
            get
            {
                return string.IsNullOrEmpty(UserIdentity);
            }
        }

        /// <summary>
        /// The endpoint settings from which the actual endpoint to use in authentication
        /// is obtained.
        /// </summary>
        public SAMLEndpointSettings EndpointSettings { get; internal set; }

        /// <summary>
        /// Tests if a SAMLRoleProfile instance could be instantiated from
        /// the persisted settings data.
        /// </summary>
        /// <param name="profileName">The name given to the persisted settings.</param>
        /// <returns>True if the settings are compatible with a SAMLRoleProfile type.</returns>
        public static bool CanCreateFrom(string profileName)
        {
            var os = LoadCredentialsProfile(profileName);
            return CanCreateFrom(os);
        }

        /// <summary>
        /// Tests if a SAMLRoleProfile instance could be instantiated from
        /// the persisted settings data.
        /// </summary>
        /// <param name="os">The persisted settings.</param>
        /// <returns>True if the settings are compatible with a SAMLRoleProfile type.</returns>
        public static bool CanCreateFrom(SettingsCollection.ObjectSettings os)
        {
            var osProfileType = os.GetValueOrDefault(SettingsConstants.ProfileTypeField, null);
            return osProfileType != null && osProfileType.Equals(ProfileManager.SAMLRoleProfileType, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Instantiates an instance from settings stored with the specified name.
        /// </summary>
        /// <param name="profileName">The name of the endpoint profile.</param>
        /// <returns>Profile instance or an exception if the profile data does not exist/contains invalid data.</returns>
        public static SAMLRoleProfile LoadFrom(string profileName)
        {
            var os = LoadCredentialsProfile(profileName);
            return LoadFrom(os);
        }

        /// <summary>
        /// Instantiates an instance from the supplied settings.
        /// </summary>
        /// <param name="os">The persisted settings.</param>
        /// <returns>Profile instance or an exception if the profile data is invalid.</returns>
        public static SAMLRoleProfile LoadFrom(SettingsCollection.ObjectSettings os)
        {
            if (os == null)
                throw new ArgumentNullException("os");

            if (!CanCreateFrom(os))
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Profile '{0}' does not contain SAML role materials", os[SettingsConstants.DisplayNameField]));

            Validate(os);

            var endpointName = os[SettingsConstants.EndpointNameField];
            var endpointSettings = ProfileManager.GetSAMLEndpoint(endpointName);

            var roleArn = os[SettingsConstants.RoleArnField];
            var userIdentity = os.GetValueOrDefault(SettingsConstants.UserIdentityField, null);

            return new SAMLRoleProfile(os[SettingsConstants.DisplayNameField], endpointSettings, roleArn, userIdentity);
        }

        /// <summary>
        /// Validates the contents of the specified profile.
        /// </summary>
        /// <param name="profileName">The name of the SAML role profile to validate.</param>
        /// <exception cref="InvalidDataException">Thrown if the profile settings fail to validate.</exception>
        public static void Validate(string profileName)
        {
            var os = LoadCredentialsProfile(profileName);
            Validate(os);
        }

        /// <summary>
        /// Validates that the presented settings would result in a valid role profile
        /// instance.
        /// </summary>
        /// <param name="os">The persisted settings.</param>
        /// <exception cref="InvalidDataException">Thrown if the profile settings fail to validate.</exception>
        private static void Validate(SettingsCollection.ObjectSettings os)
        {
            var endpointName = os.GetValueOrDefault(SettingsConstants.EndpointNameField, null); 
            if (endpointName == null)
                throw new InvalidDataException("Missing EndpointName data.");

            SAMLEndpointSettings endpointSettings;
            if (!ProfileManager.TryGetSAMLEndpoint(endpointName, out endpointSettings))
                throw new InvalidDataException(string.Format(CultureInfo.InvariantCulture, "Endpoint settings with the name '{0}' could not be found.", endpointName));

            if (string.IsNullOrEmpty(os[SettingsConstants.RoleArnField]))
                throw new InvalidDataException("Missing role ARN data.");
        }

        /// <summary>
        /// Stores the data in the role profile to the backing store file.
        /// </summary>
        public override string Persist()
        {
            return Persist(Name, EndpointSettings.Name, RoleArn, UserIdentity);
        }

        /// <summary>
        /// <para>
        /// Registers a role-based profile to be used with SAML authentication. The profile contains
        /// details of the role to be assumed when AWS credentials are requested based on the role and
        /// a reference to a SAML endpoint profile containing details of the endpoint to be called to
        /// authenticate the user.
        /// </para>
        /// <para>
        /// If user identity information is not supplied then the identity of the logged-in user will 
        /// be used when authenticaton is performed against the endpoint referenced in the SAML endpoint 
        /// profile. If identity is provided, no password information is stored in the role profile and
        /// the user must supply the password for the identity prior to authentication with the endpoint.
        /// </para>
        /// </summary>
        /// <param name="profileName">Name to be assigned to the profile</param>
        /// <param name="endpointSettingsName">
        /// The name of the settings in the SAML endpoints file containing details of the 
        /// endpoint to authenticate with.
        /// </param>
        /// <param name="roleArn">
        /// The arn of the role that the user wants to assume when using this profile. This 
        /// must be one of the set returned by the saml endpoint when the user authenticates.
        /// </param>
        /// <param name="userIdentity">
        /// Optional, can be used to prompt the user for a password for the account when authentication 
        /// is performed from a system that is not domain-joined.
        /// </param>
        /// <returns>The unique id assigned to the profile.</returns>
        public static string Persist(string profileName,
                                     string endpointSettingsName,
                                     string roleArn,
                                     string userIdentity)
        {
            if (string.IsNullOrEmpty(profileName) || string.IsNullOrEmpty(endpointSettingsName) || string.IsNullOrEmpty(roleArn))
                throw new ArgumentException("Profile name, endpoint settings name and role ARN must be supplied.");

            SAMLEndpointSettings endpointSettings;
            if (!ProfileManager.TryGetSAMLEndpoint(endpointSettingsName, out endpointSettings))
            {
                var msg = string.Format(CultureInfo.CurrentCulture, "Failed to load SAML endpoint settings with name {0}", endpointSettingsName);
                throw new ArgumentException(msg);
            }

            var settings = PersistenceManager.Instance.GetSettings(SettingsConstants.RegisteredProfiles);
            var os = ProfileManager.ReadProfileSettings(settings, profileName);
            if (os == null)
                os = settings.NewObjectSettings(Guid.NewGuid().ToString());

            os[SettingsConstants.ProfileTypeField] = ProfileManager.SAMLRoleProfileType;
            os[SettingsConstants.DisplayNameField] = profileName;
            os[SettingsConstants.EndpointNameField] = endpointSettings.Name;
            os[SettingsConstants.RoleArnField] = roleArn;
            os[SettingsConstants.UserIdentityField] = userIdentity;

            PersistenceManager.Instance.SaveSettings(SettingsConstants.RegisteredProfiles, settings);

            return os.UniqueKey;
        }

        /// <summary>
        /// Constructs a profile data instance that will use the specified network identity 
        /// during authentication with configured endpoint.
        /// </summary>
        /// <param name="profileName">The user-defined name of the profile that sourced this data.</param>
        /// <param name="endpointSettings">The settings for the authentication endpoint.</param>
        /// <param name="roleArn">The role that should be assumed on successful authentication.</param>
        /// <param name="userIdentity">The credentials to supply in authentication, in domain\user format.</param>
        private SAMLRoleProfile(string profileName,
                                SAMLEndpointSettings endpointSettings,
                                string roleArn,
                                string userIdentity)
        {
            Name = profileName;
            EndpointSettings = endpointSettings;
            RoleArn = roleArn;
            UserIdentity = userIdentity;
        }

    }
}
