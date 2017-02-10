﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Mobile.Channel;
using System.Runtime.InteropServices;
using Microsoft.Azure.Mobile.Analytics.Ingestion.Models;
using Microsoft.Azure.Mobile.Ingestion.Models;
using Microsoft.Azure.Mobile.Utils;

//TODO storage helper?

namespace Microsoft.Azure.Mobile.Analytics
{
    public class Analytics : IMobileCenterService
    {
        #region static

        private const string EnabledKey = "MobileCenterAnalyticsEnabled";
        private const string ChannelName = "analytics";
        private static object _analyticsLock = new object();

        private static Analytics _instanceField;

        public static Analytics Instance
        {
            get
            {
                lock (_analyticsLock)
                {
                    if (_instanceField == null)
                    {
                        _instanceField = new Analytics();
                    }
                    return _instanceField;
                }
            }
            set
            {
                lock (_analyticsLock)
                {
                    _instanceField = value; //for testing
                }
            }
        }

        /// <summary>
        ///     Enable or disable Analytics module.
        /// </summary>
        public static bool Enabled
        {
            get
            {
                lock (_analyticsLock)
                {
                    return Instance.InstanceEnabled;
                }
            }
            set
            {
                lock (_analyticsLock)
                {
                    Instance.InstanceEnabled = value;
                }
            }
        }

        /// <summary>
        ///     Track a custom event.
        /// </summary>
        /// <param name="name">An event name.</param>
        /// <param name="properties">Optional properties.</param>
        public static void TrackEvent(string name, IDictionary<string, string> properties = null)
        {
            lock (_analyticsLock)
            {
                Instance.InstanceTrackEvent(name, properties);
            }
        }
        #endregion

        #region instance

        private ChannelGroup _channelGroup = null;
        private bool _enabled = true;
        private IApplicationSettings _applicationSettings = new ApplicationSettings();
        internal Analytics()
        {
            LogSerializer.AddFactory(PageLog.JsonIdentifier, new LogFactory<PageLog>());
            LogSerializer.AddFactory(EventLog.JsonIdentifier, new LogFactory<EventLog>());
            LogSerializer.AddFactory(StartSessionLog.JsonIdentifier, new LogFactory<StartSessionLog>());
        }

        public bool InstanceEnabled
        {
            get
            {
                return _applicationSettings.GetValue(EnabledKey, defaultValue: true);
            }
            set
            {
                _applicationSettings[EnabledKey] = value;
            }
        }

        private void InstanceTrackEvent(string name, IDictionary<string, string> properties = null)
        {
            if (_enabled && _channelGroup != null)
            {
                var log = new EventLog(0, null, Guid.NewGuid(), name, null, properties);
                _channelGroup.GetChannel(ChannelName).Enqueue(log);
            }
        }

        public void OnChannelGroupReady(ChannelGroup channelGroup)
        {
            _channelGroup = channelGroup;
            _channelGroup.AddChannel(ChannelName, 3, TimeSpan.FromSeconds(3), 3);//TODO these values are just made up
        }
        #endregion
    }
}