using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PhoneApp3.Resources;
using System.Text;

using System.Xml.Linq; 

namespace PhoneApp3
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Konstruktor
        public MainPage()
        {
            InitializeComponent();
            tb1.Text = "some text";

            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);
            // I've used a link of a Tunisian Radio that I've developped their app on Windows Phone 
            // feel free to change it and set it with your own links 
            wc.Encoding = Encoding.GetEncoding("iso-8859-1");
            wc.DownloadStringAsync(new Uri("http://www.presseportal.de/rss/dienststelle_19027.rss2")); 

            // Beispielcode zur Lokalisierung der ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            //This simple test will return an error if the device is not connected to the internet 
            if (e.Error != null)
            {
                tb1.Text = "error on download";
                return;
            }

            
            XElement xmlitems = XElement.Parse(e.Result);
            // We need to create a list of the elements 
            List<XElement> elements = xmlitems.Descendants("item").ToList();

            // Now we're putting the informations that we got in our ListBox in the XAML code 
            // we have to use a foreach statment to be able to read all the elements  
            // Description 1 , Link1 , Title1 are the attributes in the RSSItem class that I've already added 
            //List<RSSItem> aux = new List<RSSItem>();
            foreach (XElement rssItem in elements)
            {

                tb1.Text = tb1.Text + rssItem.Element("title").Value;
                
                //rss calls from the orginal example 
                //RSSItem rss = new RSSItem();

                /*rss.Description1 = rssItem.Element("description").Value;
                rss.Link1 = rssItem.Element("link").Value;
                rss.Title1 = rssItem.Element("title").Value;
                aux.Add(rss);

                TextBlock tbTitle = new TextBlock();
                tbTitle.Text = rss.Title1 + "\n";
                ListBoxRss.Items.Add(tbTitle);

                TextBlock tbDescription = new TextBlock();
                tbDescription.Text = rss.Description1 + "\n";
                ListBoxRss.Items.Add(tbDescription);
                */
            }
        } 



        // Beispielcode zur Erstellung einer lokalisierten ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // ApplicationBar der Seite einer neuen Instanz von ApplicationBar zuweisen
        //    ApplicationBar = new ApplicationBar();

        //    // Eine neue Schaltfläche erstellen und als Text die lokalisierte Zeichenfolge aus AppResources zuweisen.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Ein neues Menüelement mit der lokalisierten Zeichenfolge aus AppResources erstellen
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}