//  
//  RouteContract.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using System.Collections.Generic;

namespace IdentityServer4.OpenAdmin.UI.Contracts
{
    public class RouteContract
    {
        public RouteType Type { get; set; }
        public string Name { get; set; }
        public bool ShowInMenu { get; set; }
        public IEnumerable<RouteContract> ChildRoutes { get; set; }
        public string Path { get; set; }
        public string MenuGroup { get; set; }
        public string Icon { get; set; }
        public Dictionary<string, object> Properties { get; set; }
    }

    public enum RouteType
    {
        Dashboard = 0,
        List = 1,
        Editor = 2,
        Group = 3
    }
}