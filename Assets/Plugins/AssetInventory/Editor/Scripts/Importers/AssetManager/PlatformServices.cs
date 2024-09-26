// adapted from https://docs.unity3d.com/Packages/com.unity.cloud.assets@1.2/manual/get-started-management.html
#if USE_ASSET_MANAGER && USE_CLOUD_IDENTITY
using System;
using System.Threading.Tasks;
using Unity.Cloud.AppLinking.Runtime;
using Unity.Cloud.Assets;
using Unity.Cloud.Common;
using Unity.Cloud.Common.Runtime;
using Unity.Cloud.Identity;
using Unity.Cloud.Identity.Runtime;

namespace AssetInventory
{
    public static class PlatformServices
    {
        /// <summary>
        /// Returns a <see cref="ICompositeAuthenticator"/>.
        /// </summary>
        public static ICompositeAuthenticator Authenticator { get; private set; }

        /// <summary>
        /// Returns a <see cref="IAuthenticationStateProvider"/>.
        /// </summary>
        public static IAuthenticationStateProvider AuthenticationStateProvider => Authenticator;

        /// <summary>
        /// Returns an <see cref="IOrganizationRepository"/>.
        /// </summary>
        public static IOrganizationRepository OrganizationRepository => Authenticator;

        /// <summary>
        /// Returns an <see cref="IAssetRepository"/>.
        /// </summary>
        public static IAssetRepository AssetRepository { get; private set; }

        public static void Create()
        {
            UnityHttpClient httpClient = new UnityHttpClient();
            IServiceHostResolver serviceHostResolver = UnityRuntimeServiceHostResolverFactory.Create();
            UnityCloudPlayerSettings playerSettings = UnityCloudPlayerSettings.Instance;
            IAuthenticationPlatformSupport platformSupport = PlatformSupportFactory.GetAuthenticationPlatformSupport();

            CompositeAuthenticatorSettings compositeAuthenticatorSettings = new CompositeAuthenticatorSettingsBuilder(httpClient, platformSupport, serviceHostResolver, playerSettings)
                .AddDefaultPkceAuthenticator(playerSettings)
                .Build();

            Authenticator = new CompositeAuthenticator(compositeAuthenticatorSettings);

            ServiceHttpClient serviceHttpClient = new ServiceHttpClient(httpClient, Authenticator, playerSettings);

            AssetRepository = AssetRepositoryFactory.Create(serviceHttpClient, serviceHostResolver);
        }

        public static async Task InitOnDemand()
        {
            CloudAssetManagement.IncBusyCount();
            if (AssetRepository == null || Authenticator == null)
            {
                Create();
                await InitializeAsync();
            }
            else if (Authenticator.AuthenticationState == AuthenticationState.LoggedOut)
            {
                await Authenticator.LoginAsync();
            }
            CloudAssetManagement.DecBusyCount();
        }

        /// <summary>
        /// A Task that initializes all platform services.
        /// </summary>
        /// <returns>A Task.</returns>
        public static async Task InitializeAsync()
        {
            await Authenticator.InitializeAsync();
            if (Authenticator.AuthenticationState == AuthenticationState.LoggedOut)
            {
                await Authenticator.LoginAsync();
            }
        }

        /// <summary>
        /// Shuts down all platform services.
        /// </summary>
        public static void ShutDownServices()
        {
            (Authenticator as IDisposable)?.Dispose();
            Authenticator = null;
        }
    }
}
#endif
