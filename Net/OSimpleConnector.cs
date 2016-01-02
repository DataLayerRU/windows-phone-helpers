using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Windows;
using WindowsPhonePostClient;
using System.Net;

namespace WPDevelopment.Net
{
    public delegate void DownloadListener(object _sender, string e);
    public class OSimpleConnector
    {
        public List<KeyValuePair<string, string>> ExtraParams;
        protected Dictionary<string, string> Params;

        public DownloadListener OnRequestComplete;

        protected string ServerName;
        protected Uri UriObject;

        public string Method = "POST";

        public OSimpleConnector()
        {
            this.Params = new Dictionary<string, string>();
            this.ExtraParams = new List<KeyValuePair<string, string>>();
        }

        public void SetServerName(string _server_name)
        {
            this.ServerName = _server_name;
        }

        public string GetServerName()
        {
            return this.ServerName;
        }

        public void SendGet()
        {
            WebClient client = new WebClient();
            client.AllowReadStreamBuffering = true;
            client.DownloadStringCompleted += client_DownloadStringCompleted;
            client.DownloadStringAsync(new Uri(this.GetServerName() + "?" + this.PrepareRequest()));
        }

        private void client_DownloadStringCompleted(object sender, System.Net.DownloadStringCompletedEventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                try
                {
                    this.OnRequestComplete.Invoke(this, e.Result);
                }
                catch (Exception ex)
                {
                    this.OnRequestComplete.Invoke(this, null);
                }
            });
        }

        public void SendRequest()
        {
            PostClient proxy = new PostClient(this.PrepareParams());
            proxy.Method = this.Method;
            proxy.DownloadStringCompleted += (sender, e) =>
            {
                if (e.Error == null)
                {
                    string data = e.Result;
                    this.OnRequestComplete.Invoke(this, data);
                }
                else
                {
                    this.OnRequestComplete.Invoke(this, null);
                }
            };
            proxy.DownloadStringAsync(new Uri(this.GetServerName(), UriKind.Absolute));
        }

        protected List<KeyValuePair<string, string>> PrepareParams()
        {
            for (int i = 0; i < this.Params.Keys.Count; i++)
            {
                var key = this.Params.Keys.ElementAt(i);
                var value = this.Params[key].ToString();

                this.ExtraParams.Add(new KeyValuePair<string, string>(key, value));
            }

            return this.ExtraParams;
        }

        protected string PrepareRequest()
        {
            string address = "";
            for (int i = 0; i < this.Params.Keys.Count; i++)
            {
                var key = this.Params.Keys.ElementAt(i);
                var value = this.Params[key];

                if (address != "")
                {
                    address += "&";
                }

                address += key + "=" + value;
            }


            return address;
        }

        public void SetParams(Dictionary<string, string> _params)
        {
            this.Params = _params;
        }

        public Dictionary<string, string> GetParams()
        {
            return this.Params;
        }

        public void AddParam(string _index, string _value)
        {
            this.Params.Add(_index, _value);
        }

        public void RemoveParam(string _index)
        {
            this.Params.Remove(_index);
        }
    }
}