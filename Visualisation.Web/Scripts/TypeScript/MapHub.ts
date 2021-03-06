﻿/// <reference path="./typings/jquery.d.ts" />
/// <reference path="./typings/signalr.d.ts" />
/// <reference path="./JudoMap.ts" />

interface SignalR {
    mapHub: MapHubProxy;
}
interface MapHubProxy {
    client: MapClient;
}
interface MapClient {
    displayLocation: (title: string, lat: number, long: number) => void;
}

module Mapping {
    export class MapHub {
        private map: JudoMap;

        constructor(map: JudoMap) {
            this.map = map;
        }

        start() {
            const mapHub = $.connection.mapHub;
            mapHub.client.displayLocation = (label, lat, long) => {
                this.map.drawOnMap(label, lat, long);
            };
            $.connection.hub.start().done();
        }
    }
}

