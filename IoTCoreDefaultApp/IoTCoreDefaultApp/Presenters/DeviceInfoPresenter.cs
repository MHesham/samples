﻿// Copyright (c) Microsoft. All rights reserved.


using System;
using System.Linq;
using Windows.Networking;
using Windows.Networking.Connectivity;
using IoTCoreDefaultApp.Utils;

namespace IoTCoreDefaultApp
{
    public static class DeviceInfoPresenter
    {
        public static string GetDeviceName()
        {
            var hostname = NetworkInformation.GetHostNames()
                .FirstOrDefault(x => x.Type == HostNameType.DomainName);
            if (hostname != null)
            {
                return hostname.CanonicalName;
            }
            return "<no device name>";
        }

        public static string GetBoardName()
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            string boardName;

            switch (DeviceTypeInformation.Type)
            {
                case DeviceTypes.RPI2:
                    boardName = DeviceTypeInformation.ProductName;
                    if (string.IsNullOrEmpty(boardName))
                    {
                        boardName = loader.GetString("Rpi2Name");
                    }
                    break;

                case DeviceTypes.MBM:
                    boardName = loader.GetString("MBMName");
                    break;

                case DeviceTypes.DB410:
                    boardName = loader.GetString("DB410Name");
                    break;

                default:
                    boardName = loader.GetString("GenericBoardName");
                    break;
            }
            return boardName;
        }

        public static Uri GetBoardImageUri()
        {
            switch (DeviceTypeInformation.Type)
            {
                case DeviceTypes.RPI2:
                    return new Uri("ms-appx:///Assets/RaspberryPiBoard.png");

                case DeviceTypes.MBM:
                    return new Uri("ms-appx:///Assets/MBMBoard.png");

                case DeviceTypes.DB410:
                    return new Uri("ms-appx:///Assets/DB410Board.png");

                default:
                    return new Uri("ms-appx:///Assets/GenericBoard.png");
            }
        }
    }
}
