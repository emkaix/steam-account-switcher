using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace AccountSwitcherV3
{
    public static class SteamWebAPIHelper
    {
        private const string _apikey = "6F1C5159690300332B9B6B27197273CA";

        public static async Task<IEnumerable<KeyValuePair<long, BitmapImage>>> FetchPlayerDataAsync(IEnumerable<Account> _accs)
        {
            var requesturl = $@"http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={_apikey}&steamids=";

            foreach (var acc in _accs)
            {
                requesturl += acc.SteamID + ",";
            }

            var wc = new WebClient();
            var jsonstring = await wc.DownloadStringTaskAsync(requesturl);
            var playerdata = JsonConvert.DeserializeObject<PlayerSummaryRootObject>(jsonstring);

            var lsResult = new List<KeyValuePair<long, BitmapImage>>();
            foreach (var player in playerdata.response.players)
            {
                var imgbytes = await wc.DownloadDataTaskAsync(player.avatar);
                var img = ToImage(imgbytes);

                lsResult.Add(new KeyValuePair<long, BitmapImage>(Convert.ToInt64(player.steamid), img));

            }

            return lsResult;
        }

        private static BitmapImage ToImage(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }

    }


    public class PlayerSummaryInfo
    {
        public string steamid { get; set; }
        public int communityvisibilitystate { get; set; }
        public int profilestate { get; set; }
        public string personaname { get; set; }
        public int lastlogoff { get; set; }
        public int commentpermission { get; set; }
        public string profileurl { get; set; }
        public string avatar { get; set; }
        public string avatarmedium { get; set; }
        public string avatarfull { get; set; }
        public int personastate { get; set; }
        public string realname { get; set; }
        public string primaryclanid { get; set; }
        public int? timecreated { get; set; }
        public int? personastateflags { get; set; }
        public string loccountrycode { get; set; }
        public string locstatecode { get; set; }
    }
    public class PlayerSummaryResponse
    {
        public List<PlayerSummaryInfo> players { get; set; }
    }
    public class PlayerSummaryRootObject
    {
        public PlayerSummaryResponse response { get; set; }
    }
}
