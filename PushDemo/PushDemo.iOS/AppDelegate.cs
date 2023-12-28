using System;
using Foundation;
using ObjCRuntime;
using UIKit;
using UrbanAirship;
using Xamarin.Forms.Platform.iOS;

namespace PushDemo.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate, IUARegistrationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {




            //UAirship.LogLevel = UALogLevel.Debug;

            //// Populate AirshipConfig.plist with your app's info from https://go.urbanairship.com
            //// or set runtime properties here.
            //UAConfig config = UAConfig.DefaultConfig();

            //// Bootstrap the Airship SDK
            //UAirship.TakeOff(config, options);

            //Console.WriteLine("Config:{0}", config);



            //var pushNotificationHandler = new PushHandler();
            //UAirship.Push.PushNotificationDelegate = pushNotificationHandler;
            //UAirship.Push.BackgroundPushNotificationsEnabled = true;
            //UAirship.Channel.EnableChannelCreation();

            AirshipInit(options);

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());
            return base.FinishedLaunching(app, options);
        }


        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            base.FailedToRegisterForRemoteNotifications(application, error);
        }

        private static void AirshipInit(NSDictionary launchOptions)
        {


            try {

                // Set log level for debugging config loading (optional)
                // It will be set to the value in the loaded config upon takeOff
                UAirship.LogLevel = UALogLevel.Debug;

                // Populate AirshipConfig.plist with your app's info from https://go.urbanairship.com
                // or set runtime properties here.
                UAConfig config = UAConfig.DefaultConfig();
                config.IsAutomaticSetupEnabled = true;
                config.IsChannelCaptureEnabled = true;
                
                config.IsChannelCreationDelayEnabled = true;
                var listURL = new string[] { "*" };

                config.URLAllowListScopeOpenURL = listURL;





                if (!config.Validate())
                {
                    throw new RuntimeException("The AirshipConfig.plist must be a part of the app bundle and " +
                        "include a valid appkey and secret for the selected production level.");
                }

                // Bootstrap the Airship SDK
                UAirship.TakeOff(config, launchOptions);
                Console.WriteLine("Config:{0}", config);
                //UAirship.Push.ResetBadge();
                UAirship.Channel.EnableChannelCreation();
                PushHandler pushHandler = new PushHandler();
                UAirship.Push.PushNotificationDelegate = pushHandler;
                UAirship.Push.BackgroundPushNotificationsEnabled = true;
                UAirship.Push.NotificationOptions = UANotificationOptions.Alert;
                Console.WriteLine("Token----------" + UAirship.Push.DeviceToken);
                Console.WriteLine("Identifier----------" + UAirship.Channel.Identifier);
                //UNNotificationAction sampleAction = UNNotificationAction.FromIdentifier("sampleAction", title: "Sample Action Title", options: UNNotificationActionOptions.Destructive);

                //var sampleActions = new UNNotificationAction[] { sampleAction };
                //var intentIdentifiers = new string[] { };

                //// Create category for sample content extension
                //UNNotificationCategory[] SampleCategoryArray = { UNNotificationCategory.FromIdentifier("sample-extension-category", actions: sampleActions, intentIdentifiers: intentIdentifiers, options: UNNotificationCategoryOptions.None) };
                //NSSet<UNNotificationCategory> categories = new NSSet<UNNotificationCategory>(SampleCategoryArray);

                //// Add sample content extension category to Airship custom categories
                //UAirship.Push.CustomCategories = categories;

            }

            catch (Exception e) {


                Console.WriteLine(e.Message);
            }
 
        
           
        }

        [Export("registrationSucceededForChannelID:deviceToken:")]
        public void RegistrationSucceeded(string channelID, string deviceToken)
        {
            NSNotificationCenter.DefaultCenter.PostNotificationName("channelIDUpdated", this);
        }
    }
}

