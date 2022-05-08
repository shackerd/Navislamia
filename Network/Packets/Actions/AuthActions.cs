﻿using Configuration;
using Navislamia.Network.Enums;
using Network;
using Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Navislamia.Network.Packets.Auth;
using Navislamia.Network.Packets;
using Navislamia.Network.Entities;
using Navislamia.Network.Packets.Actions.Interfaces;

namespace Navislamia.Network.Packets.Actions
{
    public class AuthActions : IAuthActionService
    {
        IConfigurationService configSVC;
        INotificationService notificationSVC;

        Dictionary<ushort, Func<Client, ISerializablePacket, int>> actions = new Dictionary<ushort, Func<Client, ISerializablePacket, int>>();

        public AuthActions(IConfigurationService configService, INotificationService notificationService)
        {
            configSVC = configService;
            notificationSVC = notificationService;

            actions[(ushort)AuthPackets.TS_AG_LOGIN_RESULT] = OnLoginResult; // TODO: example, remove me!
        }

        public int Execute(Client client, ISerializablePacket msg)
        {
            if (!actions.ContainsKey(msg.ID))
                return 1;

            return actions[msg.ID]?.Invoke(client, msg) ?? 2;
        }

        public int OnLoginResult(Client client, ISerializablePacket msg)
        {
            var _msg = msg as TS_AG_LOGIN_RESULT;

            if (_msg.Result > 0)
            {
                notificationSVC.WriteError("Failed to register to the Auth Server!");

                return 1;
            }

            client.Ready = true;

            notificationSVC.WriteSuccess("Successfully registered to the Auth Server!");

            return 0;
        }
    }
}
