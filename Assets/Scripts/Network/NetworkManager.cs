using UnityEngine;

namespace Modules.Network
{
    public class NetworkManager
    {
        public bool HasInternetConnection => Application.internetReachability != NetworkReachability.NotReachable;
    }
}
