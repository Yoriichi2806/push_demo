using System;
using System.Linq;
using UrbanAirship.NETStandard;
using UrbanAirship.NETStandard.MessageCenter;
using Xamarin.Forms;

namespace PushDemo
{
    public partial class App : Application
    {
        public App ()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart ()
        {


            Airship.Instance.OnMessageCenterUpdated += OnMessageCenterUpdated;
            Airship.Instance.OnMessageCenterDisplay += OnMessageCenterDisplay;
            //Airship.Instance.OnPushNotificationStatusUpdate
            Console.WriteLine(Airship.Instance.ChannelId);
       

        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }

        static void OnMessageCenterUpdated(object sender, EventArgs e)
        {
            Console.WriteLine($"OnMessageCenterUpdated {e}");
        }
        static void OnMessageCenterDisplay(object sender, MessageCenterEventArgs e)
        {
            //TabbedPage originalRootPage = (TabbedPage)App.Current.MainPage.Navigation.NavigationStack.Last();

            //originalRootPage.CurrentPage = originalRootPage.Children[1];
            Console.WriteLine($"OnMessageCenterDisplay {e.MessageId}");
            //if (e.MessageId != null)
            //{
            //    var messagePage = new MessagePage();
            //    messagePage.MessageId = e.MessageId;
            //    messagePage.Closed += OnMessageClosed;
            //    originalRootPage.Navigation.PushAsync(messagePage);
            //}
        }

        static void OnMessageClosed(object sender, MessageClosedEventArgs e)
        {
            //App.Current.MainPage.Navigation.PopAsync();
        }
    }
}

