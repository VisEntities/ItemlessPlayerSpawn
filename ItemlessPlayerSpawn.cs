/*
 * Copyright (C) 2024 Game4Freak.io
 * This mod is provided under the Game4Freak EULA.
 * Full legal terms can be found at https://game4freak.io/eula/
 */

using System.Collections.Generic;

namespace Oxide.Plugins
{
    [Info("Itemless Player Spawn", "VisEntities", "1.0.0")]
    [Description(" ")]
    public class ItemlessPlayerSpawn : RustPlugin
    {
        #region Fields

        private static ItemlessPlayerSpawn _plugin;

        #endregion Fields

        #region Oxide Hooks

        private void Init()
        {
            _plugin = this;
            PermissionUtil.RegisterPermissions();
        }

        private void Unload()
        {
            _plugin = null;
        }

        private object OnDefaultItemsReceive(PlayerInventory inventory)
        {
            if (inventory == null)
                return null;

            BasePlayer player = inventory.baseEntity;
            if (player == null)
                return null;

            if (!PermissionUtil.HasPermission(player, PermissionUtil.USE))
                return null;

            return true;
        }

        #endregion Oxide Hooks

        #region Permissions

        private static class PermissionUtil
        {
            public const string USE = "itemlessplayerspawn.use";
            private static readonly List<string> _permissions = new List<string>
            {
                USE,
            };

            public static void RegisterPermissions()
            {
                foreach (var permission in _permissions)
                {
                    _plugin.permission.RegisterPermission(permission, _plugin);
                }
            }

            public static bool HasPermission(BasePlayer player, string permissionName)
            {
                return _plugin.permission.UserHasPermission(player.UserIDString, permissionName);
            }
        }

        #endregion Permissions
    }
}