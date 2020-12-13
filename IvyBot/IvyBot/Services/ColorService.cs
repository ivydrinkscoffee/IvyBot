using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using IvyBot.Modules;

namespace IvyBot.Services
{
    public class ColorService
    {
        public class ColorDef
        {
            public string Id;
            public string Name;
            public Color Color;
            
            public ColorDef(string name, Color color)
            {
                Name = name;
                Id = name.ToLowerInvariant();
                Color = color;
            }
        }
        
        public static async Task<string> SetColorAsync(SocketGuildUser user, string colorName)
        {
            ColorDef color;
            
            if (!ColorModule.colorMap.TryGetValue(colorName.ToLowerInvariant(), out color))
            {
                return "<:empty:314349398723264512> Color not found";
            }

            SocketRole role = user.Guild.Roles.Where(r => r.Name == color.Name).FirstOrDefault();

            if (role == null)
            {
                var newRole = await user.Guild.CreateRoleAsync(color.Name, permissions: GuildPermissions.None, color: color.Color, false, null);
                await user.AddRoleAsync(newRole);

                return $"<:check:314349398811475968> Set **{user.ToString()}**'s color role to **{color.Name}**";
            }

            await user.AddRoleAsync(role);
            
            return $"<:check:314349398811475968> Set **{user.ToString()}**'s color role to **{color.Name}**";
        }
    }
}
