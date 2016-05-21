/// <reference path="./typings/jquery.d.ts" />
/// <reference path="./typings/signalr.d.ts" />
/// <reference path="./JudoMap.ts" />
var Mapping;
(function (Mapping) {
    var MapHub = (function () {
        function MapHub(map) {
            this.map = map;
        }
        MapHub.prototype.start = function () {
            var _this = this;
            var mapHub = $.connection.mapHub;
            mapHub.client.displayLocation = function (label, lat, long) {
                _this.map.drawOnMap(label, lat, long);
            };
            $.connection.hub.start().done();
        };
        return MapHub;
    }());
    Mapping.MapHub = MapHub;
})(Mapping || (Mapping = {}));
