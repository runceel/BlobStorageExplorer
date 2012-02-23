[assembly: SilverlightActivator.ApplicationStartupMethod(typeof(PhoneApp2.App_Start.StorageInitializer), "ApplicationStartup", Order = 10)]

namespace PhoneApp2.App_Start
{
    using System;
    using System.Windows;
    using Microsoft.WindowsAzure.Samples.Phone.Storage;

    public class StorageInitializer
    {
        public static void ApplicationStartup()
        {
            // By using CloudStorageClientResolverAccountAndKey.DevelopmentStorageAccountResolver you can connect directly 
            // against the Windows Azure Storage Emulator.
            var resolver = CloudStorageClientResolverAccountAndKey.DevelopmentStorageAccountResolver;

            //resolver = new CloudStorageClientResolverAccountAndKey(
            //    new StorageCredentialsAccountAndKey("kazukiohta", "ZwxmNLtD6k4SoFCFvg3RKOczKrK6XGC1Di+YwGtFMBrX9hBhesK5r8f7trsl9QR817PNBN8ngTbJiULbMSDb4w=="),
            //    new Uri("https://kazukiohta.blob.core.wa.fj-cloud.net"),
            //    new Uri("http://test"),
            //    new Uri("http://test"),
            //    Deployment.Current.Dispatcher);
            // By using CloudStorageClientResolverProxies you can connect to a Windows Azure Web Role that contains the
            // Windows Azure Storage Proxies.
            // Create a new Cloud project with an MVC 3 Web Role and install the WindowsAzure.Storage NuGet package.
            // By using CloudStorageClientResolverAccountAndKey you can connect to your Windows Azure Storage Services account directly.
            // Just replace your Windows Azure Storage account credentials and endpoints.
            // var resolver = new CloudStorageClientResolverAccountAndKey(
            //    new StorageCredentialsAccountAndKey("[your account name]", "[your account key]"),
            //    new Uri("http://[your account name].blob.core.windows.net"),
            //    new Uri("http://[your account name].queue.core.windows.net"),
            //    new Uri("http://[your account name].table.core.windows.net"),
            //    Deployment.Current.Dispatcher);

            //CloudStorageContext.Current.Resolver = resolver;
        }
    }
}